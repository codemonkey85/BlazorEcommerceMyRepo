using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorECommerce.Server.Data.Migrations.Sqlite
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Product 1 Description", "https://img.freepik.com/free-psd/cosmetic-product-packaging-mockup_1150-40284.jpg?w=2000", 9.99m, "Product 1 Title" },
                    { 2, "Product 2 Description", "https://img.freepik.com/free-psd/cosmetic-product-packaging-mockup_1150-40282.jpg?w=2000", 9.99m, "Product 2 Title" },
                    { 3, "Product 3 Description", "https://img.freepik.com/free-photo/pedestal-display-blank-podium-product_1048-16154.jpg?w=996", 9.99m, "Product 3 Title" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
