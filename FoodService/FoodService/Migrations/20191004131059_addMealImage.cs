using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodService.Migrations
{
    public partial class addMealImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c39efca-8290-4ba0-9bad-f3dddbf0d5cb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4bf3abf-05d5-4e81-b075-7d53d11cafa9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d42e6b1c-d98b-49aa-aa2d-9d2a6fc6d45c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "52d63534-3935-4222-8471-29eb178d99a9", "2dafcbeb-a275-4bb5-a931-a7853e809e72", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f148537d-09a5-4beb-ba14-93a033073eea", "90fcd3b7-fecd-47cb-b747-349740a73f56", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "34132f54-7c7a-4bf3-9207-1346b09cc6fc", "c606b957-1530-445e-af53-2185b84dd3cc", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34132f54-7c7a-4bf3-9207-1346b09cc6fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52d63534-3935-4222-8471-29eb178d99a9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f148537d-09a5-4beb-ba14-93a033073eea");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d42e6b1c-d98b-49aa-aa2d-9d2a6fc6d45c", "5a2314c5-ac2b-4d28-8daa-44b6630d39ec", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b4bf3abf-05d5-4e81-b075-7d53d11cafa9", "55ae77fd-3cff-4845-aa03-bf1fa9ec8d7a", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2c39efca-8290-4ba0-9bad-f3dddbf0d5cb", "39c7e8c1-2efe-4cf0-804f-875c93d13e7a", "Customer", "CUSTOMER" });
        }
    }
}
