import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { RegisterDto } from '../components/register/register.component';
import { LoginDto } from '../components/login/login.component';
import { catchError, map, Observable, of } from 'rxjs';
import { LoginResponseDTO } from '../interfaces/login-response-dto';
import { tokenGetter, tokenSetter } from '../../../app.config';
import { DecodedUserToken } from '../interfaces/decoded-user-token';
import { jwtDecode } from 'jwt-decode';
import { User } from '../interfaces/user';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  api: string;
  http: HttpClient;
  router: Router;
  currentUser: User | null = null;
  decodedUserToken: DecodedUserToken | null = null;

  constructor(http: HttpClient, router: Router) {
    this.api = environment.apiUrl;
    this.http = http;
    this.router = router;
  }

  get accessToken() {
    return tokenGetter();
  }

  get currentUserId() {
    if (!this.accessToken || this.accessToken === '') {
      return '';
    }
    this.decodedUserToken = jwtDecode<DecodedUserToken>(this.accessToken);
    return this.decodedUserToken ? this.decodedUserToken.id : '';
  }

  hasRole(role: string): boolean {
    if (!this.accessToken || this.accessToken === '') {
      return false;
    }
    this.decodedUserToken = jwtDecode<DecodedUserToken>(this.accessToken);

    let tokenRole = this.decodedUserToken ? this.decodedUserToken.role : null;

    if (Array.isArray(tokenRole)) {
      return tokenRole.includes(role);
    }

    if (tokenRole === role) {
      return true;
    }

    return false;
  }

  register(registerDto: RegisterDto): Observable<string> {
    return this.http.post(`${this.api}/Users/register`, registerDto, { responseType: 'text' }).pipe(
      map((res: string) => {
        return res;
      }),
      catchError((error: HttpErrorResponse) => {
        console.error('Registration failed', error);
        throw error;
      })
    );
  }

  login(loginDto: LoginDto): Observable<LoginResponseDTO> {
    return this.http.post<LoginResponseDTO>(`${this.api}/Users/login`, loginDto)
      .pipe(map((res => {
        tokenSetter(res.token);
        this.setRefreshToken(res.refreshToken);
        this.decodedUserToken = jwtDecode<DecodedUserToken>(res.token);

        return res;
      }
    )));
  }

  refreshToken(): Observable<LoginResponseDTO> {
    const refreshToken = this.getRefreshToken();
    return this.http.post<LoginResponseDTO>(`${this.api}/Users/refresh-token`, { token: this.accessToken, refreshToken })
      .pipe(map((res => {
        tokenSetter(res.token);
        this.setRefreshToken(res.refreshToken);
        return res;
      }
    )));
  }

  isAuthenticated(): Observable<boolean> {
    if (!this.accessToken || this.accessToken === '') {
      return of(false);
    }

    return of(true);
  }

  logout() {
    tokenSetter('');
    this.setRefreshToken('');
    this.currentUser = null;
    this.decodedUserToken = null;
    this.router.navigate(['/home']);
  }

  forgotPassword(email: string) {
    return this.http.post(`${this.api}/forgot-password`, { email });
  }

  private setRefreshToken(refreshToken: string | null) {
    if (refreshToken) {
      document.cookie = `refreshToken=${refreshToken}; path=/;`;
    } else {
      document.cookie = `refreshToken=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
    }
  }

  private getRefreshToken(): string | null {
    const cookie = document.cookie;
    const cookieArray = cookie.split('; ');
    for (const cookie of cookieArray) {
      const [key, value] = cookie.split('=');
      if (key === 'refreshToken') {
        return value;
      }
    }
    return null;
  }
}
