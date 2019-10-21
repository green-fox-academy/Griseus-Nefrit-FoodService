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
                keyValue: "5b35a1bc-394d-4e4d-a243-0a31baf7162d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b962968-6d19-4a83-92bf-6d2bc688e44c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6185c20-c284-48fa-bc99-8dd223721504");

            migrationBuilder.AddColumn<string>(
                name: "Timezone",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b2b6334a-6625-4000-8426-dfad7bcb8cef", "30a2d2fa-0683-498a-8eb2-d6bc57d84801", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5a2666ca-2b3f-475f-b133-9747a4f66e84", "c2d05abf-1f13-45de-b074-8a9a0bf0b007", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b7924f4e-c034-4954-9937-03d71cac5efa", "4ac1bdc0-794c-4563-93d2-317f9436654a", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a2666ca-2b3f-475f-b133-9747a4f66e84");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2b6334a-6625-4000-8426-dfad7bcb8cef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7924f4e-c034-4954-9937-03d71cac5efa");

            migrationBuilder.DropColumn(
                name: "Timezone",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c6185c20-c284-48fa-bc99-8dd223721504", "8c19d4bd-3fdc-4ab5-9049-4af5761c4a61", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5b962968-6d19-4a83-92bf-6d2bc688e44c", "0c49f363-5174-419b-8353-62edfb7cd309", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5b35a1bc-394d-4e4d-a243-0a31baf7162d", "7f881a53-cca8-48a8-a04f-4a7178801b26", "Customer", "CUSTOMER" });
        }
    }
}
