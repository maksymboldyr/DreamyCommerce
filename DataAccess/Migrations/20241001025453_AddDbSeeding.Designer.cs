﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241001025453_AddDbSeeding")]
    partial class AddDbSeeding
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("DataAccess.Entities.Category", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1d",
                            Name = "Electronics"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1e",
                            Name = "Clothing"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1f",
                            Name = "Books"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1g",
                            Name = "Furniture"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1h",
                            Name = "Toys"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1i",
                            Name = "Tools"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1j",
                            Name = "Sports"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1k",
                            Name = "Music"
                        });
                });

            modelBuilder.Entity("DataAccess.Entities.Order", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DataAccess.Entities.OrderDetail", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("DataAccess.Entities.Product", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CategoryId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Discount")
                        .HasColumnType("REAL");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("Stock")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SubcategoryId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SubcategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DataAccess.Entities.Subcategory", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Subcategories");

                    b.HasData(
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1l",
                            CategoryId = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1d",
                            Name = "Smartphones"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1m",
                            CategoryId = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1d",
                            Name = "Laptops"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1n",
                            CategoryId = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1e",
                            Name = "T-Shirts"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1o",
                            CategoryId = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1e",
                            Name = "Jeans"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1p",
                            CategoryId = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1f",
                            Name = "Fantasy"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1q",
                            CategoryId = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1f",
                            Name = "Science Fiction"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1r",
                            CategoryId = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1g",
                            Name = "Sofas"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1s",
                            CategoryId = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1g",
                            Name = "Beds"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1t",
                            CategoryId = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1h",
                            Name = "Cars"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1u",
                            CategoryId = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1h",
                            Name = "Dinosaurs"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1v",
                            CategoryId = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1i",
                            Name = "Drills"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1w",
                            CategoryId = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1i",
                            Name = "Screwdrivers"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1x",
                            CategoryId = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1j",
                            Name = "Football"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1y",
                            CategoryId = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1j",
                            Name = "Basketball"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1z",
                            CategoryId = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1k",
                            Name = "Guitars"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e10",
                            CategoryId = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1k",
                            Name = "Drums"
                        });
                });

            modelBuilder.Entity("DataAccess.Entities.Users.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1b",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1c",
                            Name = "Shop",
                            NormalizedName = "SHOP"
                        });
                });

            modelBuilder.Entity("DataAccess.Entities.Users.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1a",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "e790b5ec-33f1-4577-a749-dee4bbfc6ad6",
                            Email = "admin@mail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@MAIL.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAIAAYagAAAAEAVUjtRAi74ijyIwjlcEGUSi5W7gBEcSdxYfuc13m+AxRHggcl1dcECQsyjGPgo/hg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "9d6d1de9-481a-4063-99f2-5a0541c50bd3",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1b",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "acab0947-6d71-4d39-aa62-9d22a9addc6e",
                            Email = "shop@mail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "SHOP@MAIL.COM",
                            NormalizedUserName = "SHOP",
                            PasswordHash = "AQAAAAIAAYagAAAAELe/K/tkf3WXY+UnQDx66ExLAAmgfuO6eGE6exbgax7oRWQ6UJw/R0ZrlmAiiybS9Q==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "0fdceace-1997-43a2-9876-679e9143f789",
                            TwoFactorEnabled = false,
                            UserName = "shop"
                        },
                        new
                        {
                            Id = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1c",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "da760f73-3cdd-4d53-96c0-bfeedaa60fc5",
                            Email = "user@mail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "USER@MAIL.COM",
                            NormalizedUserName = "USER",
                            PasswordHash = "AQAAAAIAAYagAAAAEFAYkqypxmduCnqb6+QjnPGFp6O8rd7nERIgCY8OFWOSWKxrOIKrh7jjXEn1L/vMhQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "ac25d2ab-4bae-40dc-b21a-f2cb6febad92",
                            TwoFactorEnabled = false,
                            UserName = "user"
                        });
                });

            modelBuilder.Entity("DataAccess.Entities.Users.UserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1a",
                            RoleId = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1b"
                        },
                        new
                        {
                            UserId = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1b",
                            RoleId = "f1b0b3f4-3b1b-4b7e-8f1d-3e0b6e1d6e1c"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DataAccess.Entities.Order", b =>
                {
                    b.HasOne("DataAccess.Entities.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccess.Entities.OrderDetail", b =>
                {
                    b.HasOne("DataAccess.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DataAccess.Entities.Product", b =>
                {
                    b.HasOne("DataAccess.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.HasOne("DataAccess.Entities.Subcategory", "Subcategory")
                        .WithMany("Products")
                        .HasForeignKey("SubcategoryId");

                    b.Navigation("Category");

                    b.Navigation("Subcategory");
                });

            modelBuilder.Entity("DataAccess.Entities.Subcategory", b =>
                {
                    b.HasOne("DataAccess.Entities.Category", "Category")
                        .WithMany("Subcategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DataAccess.Entities.Users.UserRole", b =>
                {
                    b.HasOne("DataAccess.Entities.Users.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.Users.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("DataAccess.Entities.Users.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DataAccess.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DataAccess.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("DataAccess.Entities.Users.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DataAccess.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Entities.Category", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("DataAccess.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("DataAccess.Entities.Subcategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("DataAccess.Entities.Users.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("DataAccess.Entities.Users.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}