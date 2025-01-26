using BusinessLogic.DTO;
using BusinessLogic.DTO.Auth;
using BusinessLogic.Interfaces;
using DataAccess.Entities.Users;
using DataAccess.Interfaces;
using DataAccess.Repository;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLogic.Services.Domain;

/// <summary>
/// Service for managing users.
/// </summary>
public class UserService(
    IUserRepository userRepository,
    FilterBuilderService filterBuilder,
    SortingService<UserDto> sortingService,
    UserManager<User> userManager,
    IConfiguration configuration) : IUserService
{
    /// <summary>
    /// Gets user data by id.
    /// </summary>
    /// <param name="id">The user identifier.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="UserDataDto"/> object.</returns>
    public async Task<UserDataDto> GetUserDataByIdAsync(string id)
    {
        var user = await userRepository.GetUserByIdAsync(id);
        return user.Adapt<UserDataDto>();
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="registrationDto">The user registration data transfer object.</param>
    /// <returns><see langword="true"/> if the user was created successfully, <see langword="false"/> otherwise.</returns>
    public async Task<bool> CreateUserAsync(RegistrationDto registrationDto)
    {
        var user = registrationDto.Adapt<User>();
        user.PasswordHash = userManager.PasswordHasher.HashPassword(user, registrationDto.Password);
        return await userRepository.CreateUserAsync(user);
    }

    /// <summary>
    /// Check if the entered password is correct.
    /// </summary>
    /// <param name="loginDto">The user login data transfer object.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains <see langword="true"/> if the password is correct, <see langword="false"/> otherwise.</returns>
    public async Task<bool> CheckUserPasswordAsync(LoginDto loginDto)
    {
        var user = await userRepository.GetUserByEmailAsync(loginDto.Email);
        return await userManager.CheckPasswordAsync(user, loginDto.Password);
    }

    /// <summary>
    /// Gets an access token by email.
    /// </summary>
    /// <param name="email">The user's email address.</param>
    /// <returns>JWT token.</returns>
    public async Task<string> GetTokenByEmailAddressAsync(string email)
    {
        var user = await userRepository.GetUserByEmailAsync(email);
        var userRoles = await userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
            new Claim("id", user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        string token = GenerateToken(authClaims);

        user.RefreshToken = token;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await userRepository.UpdateUserAsync(user);

        return token;
    }

    public async Task<string?> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        var principal = GetPrincipalFromExpiredToken(refreshTokenDto.Token);
        if (principal == null)
        {
            return null;
        }

        var userId = principal.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        if (userId == null)
        {
            return null;
        }

        var user = await userRepository.GetUserByIdAsync(userId);
        if (user == null || user.RefreshToken != refreshTokenDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return null;
        }

        var newToken = GenerateToken(principal.Claims);
        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // Set new refresh token expiry time
        await userRepository.UpdateUserAsync(user);

        return newToken;
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    /// <summary>
    /// Generates a JWT token based on the provided claims.
    /// </summary>
    /// <param name="claims">The claims to include in the token.</param>
    /// <returns>The generated JWT token.</returns>
    private string GenerateToken(IEnumerable<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = configuration["JWT:ValidIssuer"],
            Audience = configuration["JWT:ValidAudience"],
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    /// <summary>
    /// Gets filtered, ordered and paginated collection of <see cref="UserDto"/> objects and total count of users.
    /// </summary>
    /// <param name="filter">The filter string.</param>
    /// <param name="page">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <param name="sortField">The field to sort by.</param>
    /// <param name="sortOrder">The sort order (asc/desc).</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="UserDto"/> objects and the total count of users.</returns>
    public async Task<(IEnumerable<UserDto>, int)> GetUsersAsync(string filter, int page, int pageSize, string sortField, string sortOrder)
    {
        var filterExpression = filterBuilder.BuildFilter<UserDto>(filter);
        var sortExpression = sortingService.GetSortExpression(sortField, sortOrder);

        var users = await userRepository.GetUsersAsync();

        var userDTOs = new List<UserDto>();

        foreach (var user in users)
        {
            var userDTO = user.Adapt<UserDto>();
            userDTO.Roles = await GetUserRolesByIdAsync(user.Id);
            userDTOs.Add(userDTO);
        }

        var filteredDTOs = userDTOs
            .Where(u => filterExpression.Compile().Invoke(u));

        var sortedDtos = sortExpression(filteredDTOs.AsQueryable());

        var totalUsers = filteredDTOs.Count();

        var pagedUserDTOs = sortedDtos
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        return (pagedUserDTOs, totalUsers);
    }


    /// <summary>
    /// Gets user by id.
    /// </summary>
    /// <param name="id">The user identifier.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="UserDto"/> object.</returns>
    public async Task<UserDto> GetUserByIdAsync(string id)
    {
        var user = await userRepository.GetUserByIdAsync(id);
        return user.Adapt<UserDto>();
    }

    /// <summary>
    /// Gets user by email.
    /// </summary>
    /// <param name="email">The user's email address.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="UserDto"/> object.</returns>
    public async Task<UserDto> GetUserByEmailAsync(string email)
    {
        var user = await userRepository.GetUserByEmailAsync(email);
        return user.Adapt<UserDto>();
    }

    /// <summary>
    /// Updates user.
    /// </summary>
    /// <param name="userDto">The user data transfer object.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains <see langword="true"/> if the user was updated successfully, <see langword="false"/> otherwise.</returns>
    public async Task<bool> UpdateUserAsync(UserDto userDto)
    {
        var user = userDto.Adapt<User>();

        var existingRoles = await GetUserRolesByIdAsync(userDto.Id);
        var rolesToRemove = existingRoles.Except(userDto.Roles);
        var rolesToAdd = userDto.Roles.Except(existingRoles);

        foreach (var role in rolesToRemove)
        {
            await RemoveRole(new EditUserRolesDto { UserId = userDto.Id, RoleName = role });
        }

        foreach (var role in rolesToAdd)
        {
            await SetUserRole(new EditUserRolesDto { UserId = userDto.Id, RoleName = role });
        }

        return await userRepository.UpdateUserAsync(user);
    }

    /// <summary>
    /// Deletes user by id.
    /// </summary>
    /// <param name="id">The user identifier.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains <see langword="true"/> if the user was deleted successfully, <see langword="false"/> otherwise.</returns>
    public async Task<bool> DeleteUserAsync(string id)
    {
        return await userRepository.DeleteUserAsync(id);
    }

    /// <summary>
    /// Gets all roles.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="RoleDto"/> objects.</returns>
    public async Task<IEnumerable<RoleDto>> GetRolesAsync()
    {
        var roles = await userRepository.GetRolesAsync();
        return roles.Adapt<IEnumerable<RoleDto>>();
    }

    /// <summary>
    /// Gets roles by given filter with pagination and sorting.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of role names.</returns>
    public async Task<IEnumerable<string>> GetUserRolesByIdAsync(string userId)
    {
        return await userRepository.GetUserRolesByIdAsync(userId);
    }

    /// <summary>
    /// Gets all role names.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of role names.</returns>
    public async Task<IEnumerable<string>> GetRolesNamesAsync()
    {
        var roles = await userRepository.GetRolesAsync();
        return roles.Select(r => r.Name!).Where(name => name != null);
    }

    /// <summary>
    /// Sets user role.
    /// </summary>
    /// <param name="editUserRolesDto">The data transfer object containing user id and role name.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains <see langword="true"/> if the role was set successfully, <see langword="false"/> otherwise.</returns>
    public async Task<bool> SetUserRole(EditUserRolesDto editUserRolesDto)
    {
        return await userRepository.SetRole(editUserRolesDto.UserId, editUserRolesDto.RoleName);
    }

    /// <summary>
    /// Removes user role.
    /// </summary>
    /// <param name="editUserRolesDto">The data transfer object containing user id and role name.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains <see langword="true"/> if the role was removed successfully, <see langword="false"/> otherwise.</returns>
    public async Task<bool> RemoveRole(EditUserRolesDto editUserRolesDto)
    {
        return await userRepository.RemoveRole(editUserRolesDto.UserId, editUserRolesDto.RoleName);
    }


}
