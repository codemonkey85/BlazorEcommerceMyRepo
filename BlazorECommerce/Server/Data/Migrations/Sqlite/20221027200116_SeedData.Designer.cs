﻿// <auto-generated />
using BlazorECommerce.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorECommerce.Server.Data.Migrations.Sqlite
{
    [DbContext(typeof(SqliteDatabaseContext))]
    [Migration("20221027200116_SeedData")]
    partial class SeedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0-rc.2.22472.11");

            modelBuilder.Entity("BlazorECommerce.Shared.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Product 1 Description",
                            ImageUrl = "https://img.freepik.com/free-psd/cosmetic-product-packaging-mockup_1150-40284.jpg?w=2000",
                            Price = 9.99m,
                            Title = "Product 1 Title"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Product 2 Description",
                            ImageUrl = "https://img.freepik.com/free-psd/cosmetic-product-packaging-mockup_1150-40282.jpg?w=2000",
                            Price = 9.99m,
                            Title = "Product 2 Title"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Product 3 Description",
                            ImageUrl = "https://img.freepik.com/free-photo/pedestal-display-blank-podium-product_1048-16154.jpg?w=996",
                            Price = 9.99m,
                            Title = "Product 3 Title"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
