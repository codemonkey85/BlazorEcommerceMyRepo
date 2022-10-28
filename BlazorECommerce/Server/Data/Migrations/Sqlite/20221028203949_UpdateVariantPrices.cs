using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorECommerce.Server.Data.Migrations.Sqlite
{
    /// <inheritdoc />
    public partial class UpdateVariantPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductId", "ProductTypeId" },
                keyValues: new object[] { 1, 3 },
                columns: new[] { "OriginalPrice", "Price" },
                values: new object[] { 29.99m, 19.99m });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductId", "ProductTypeId" },
                keyValues: new object[] { 2, 6 },
                columns: new[] { "OriginalPrice", "Price" },
                values: new object[] { 29.99m, 19.99m });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductId", "ProductTypeId" },
                keyValues: new object[] { 3, 10 },
                columns: new[] { "OriginalPrice", "Price" },
                values: new object[] { 29.99m, 19.99m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductId", "ProductTypeId" },
                keyValues: new object[] { 1, 3 },
                columns: new[] { "OriginalPrice", "Price" },
                values: new object[] { 19.99m, 9.99m });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductId", "ProductTypeId" },
                keyValues: new object[] { 2, 6 },
                columns: new[] { "OriginalPrice", "Price" },
                values: new object[] { 19.99m, 9.99m });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumns: new[] { "ProductId", "ProductTypeId" },
                keyValues: new object[] { 3, 10 },
                columns: new[] { "OriginalPrice", "Price" },
                values: new object[] { 19.99m, 9.99m });
        }
    }
}
