﻿// <auto-generated />
using FoodService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodService.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20190925111435_initial2")]
    partial class initial2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FoodService.Models.Restaurant", b =>
                {
                    b.Property<long>("RestaurantId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Description");

                    b.Property<string>("FoodType");

                    b.Property<string>("Name");

                    b.Property<int>("PriceCategory");

                    b.HasKey("RestaurantId");

                    b.ToTable("Restaurants");
                });
#pragma warning restore 612, 618
        }
    }
}