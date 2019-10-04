using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodService.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "345a962a-8075-4b68-bdf4-a4a0c5d166c7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "379e9e04-70f8-4b9d-bf6a-13624d7dbd0f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc13999e-58c4-487f-9fca-df3c3ce1eda5");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "cc13999e-58c4-487f-9fca-df3c3ce1eda5", "ea7ee3a0-2f94-48be-ac0e-059544133e01", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "345a962a-8075-4b68-bdf4-a4a0c5d166c7", "6e4843ee-f5e7-4227-828c-b26fe4f2c687", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "379e9e04-70f8-4b9d-bf6a-13624d7dbd0f", "3154665c-4ee0-4d8d-ab6f-96a9759e3f9e", "Customer", "CUSTOMER" });
        }
    }
}
