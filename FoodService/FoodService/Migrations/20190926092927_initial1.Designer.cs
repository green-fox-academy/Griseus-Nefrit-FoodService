﻿// <auto-generated />
using System;
using FoodService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodService.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20190926092927_initial1")]
    partial class initial1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FoodService.Models.Meal", b =>
                {
                    b.Property<long>("MealId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

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

                    b.Property<string>("Currency");

                    b.HasKey("PriceId");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("FoodService.Models.Restaurant", b =>
                {
                    b.Property<long>("RestaurantId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("RestaurantId");

                    b.ToTable("Restaurants");
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
#pragma warning restore 612, 618
        }
    }
}
