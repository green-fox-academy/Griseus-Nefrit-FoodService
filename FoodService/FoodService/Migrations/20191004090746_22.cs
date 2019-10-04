using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodService.Migrations
{
    public partial class _22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "25547674-8578-43f2-bd78-7e306ddbe079");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "997e9ad1-1a97-46a1-bf27-b9fd48230fe8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a097ab58-5d04-4841-86cf-fbae5dd8f177");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "997e9ad1-1a97-46a1-bf27-b9fd48230fe8", "99eb860e-4e80-4eb4-8f14-1b1e74e452fe", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a097ab58-5d04-4841-86cf-fbae5dd8f177", "93d91d3d-7efa-4f56-9090-81c6a6217f73", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "25547674-8578-43f2-bd78-7e306ddbe079", "1e16a514-3554-4346-9762-6a9e9840339a", "Customer", "CUSTOMER" });
        }
    }
}
