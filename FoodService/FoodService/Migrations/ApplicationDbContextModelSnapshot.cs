﻿// <auto-generated />
using System;
using FoodService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodService.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FoodService.Models.CartItem", b =>
                {
                    b.Property<long>("CartItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("MealId");

                    b.Property<int>("Quantity");

                    b.Property<long?>("ShoppingCartId");

                    b.HasKey("CartItemId");

                    b.HasIndex("MealId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("FoodService.Models.Identity.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("FoodService.Models.Meal", b =>
                {
                    b.Property<long>("MealId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("ImageUri");

                    b.Property<string>("Name");

                    b.Property<long?>("PriceId");

                    b.Property<long?>("RestaurantId");

                    b.HasKey("MealId");

                    b.HasIndex("PriceId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("FoodService.Models.Price", b =>
                {
                    b.Property<long>("PriceId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<string>("Currency")
                        .IsRequired();

                    b.HasKey("PriceId");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("FoodService.Models.Restaurant", b =>
                {
                    b.Property<long>("RestaurantId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("FoodType")
                        .IsRequired();

                    b.Property<string>("ManagerId")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("PriceCategory");

                    b.HasKey("RestaurantId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("FoodService.Models.ShoppingCart", b =>
                {
                    b.Property<long>("ShoppingCartId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<int>("OrderStatus");

                    b.Property<string>("UserId");

                    b.HasKey("ShoppingCartId");

                    b.HasIndex("UserId");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "39b57eb7-ba79-49ad-a702-4daa5293ecc3",
                            ConcurrencyStamp = "2680c856-6e3e-4640-9285-ce7128b62601",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "e3ae4675-48a5-4184-b749-52068c9ac5e7",
                            ConcurrencyStamp = "155f0256-0ae3-401d-8958-a54121e51411",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = "094ffe12-179a-4151-8f87-943a4eb3b681",
                            ConcurrencyStamp = "628c8b5d-8e09-4eea-bd07-23f15bccfd2c",
                            Name = "Customer",
                            NormalizedName = "CUSTOMER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FoodService.Models.CartItem", b =>
                {
                    b.HasOne("FoodService.Models.Meal", "meal")
                        .WithMany("CartItems")
                        .HasForeignKey("MealId");

                    b.HasOne("FoodService.Models.ShoppingCart", "ShoppingCart")
                        .WithMany("CartItems")
                        .HasForeignKey("ShoppingCartId");
                });

            modelBuilder.Entity("FoodService.Models.Meal", b =>
                {
                    b.HasOne("FoodService.Models.Price", "Price")
                        .WithMany()
                        .HasForeignKey("PriceId");

                    b.HasOne("FoodService.Models.Restaurant", "Restaurant")
                        .WithMany("Meals")
                        .HasForeignKey("RestaurantId");
                });

            modelBuilder.Entity("FoodService.Models.Restaurant", b =>
                {
                    b.HasOne("FoodService.Models.Identity.AppUser", "Manager")
                        .WithMany("OwnedRestaurants")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FoodService.Models.ShoppingCart", b =>
                {
                    b.HasOne("FoodService.Models.Identity.AppUser", "User")
                        .WithMany("ShoppingCarts")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FoodService.Models.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FoodService.Models.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FoodService.Models.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FoodService.Models.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
