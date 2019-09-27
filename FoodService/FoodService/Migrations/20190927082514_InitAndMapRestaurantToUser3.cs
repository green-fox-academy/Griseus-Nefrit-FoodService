using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodService.Migrations
{
    public partial class InitAndMapRestaurantToUser3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_ManagerId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_ManagerId",
                table: "Restaurants");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a3385738-ef48-4b3d-8769-2a0dd5916666", "5dcbfac0-8cd8-43d0-b976-9afefb066cd7" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "e08fb599-8d4c-4451-8e9d-8935ccf75eb5", "66a3a3ed-8ce8-4be2-aafd-386ec04c5e62" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "ff475a7c-9795-42fa-b1b0-7b39f831ef55", "50e80e92-6222-42d8-869c-24f4612bc989" });

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Restaurants");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Restaurants",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "524910cd-9a53-43ac-b3c6-0340c10384de", "b7e9c44b-63d2-49b0-815a-5092642f1e45", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "38de4664-f62d-410e-b918-cec751aa4a1d", "adc0ea8d-39e9-43a2-a362-c460e57497d3", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b207baae-0696-4fcf-9807-1214981e6b64", "76cca271-ea20-4f17-af6c-25706d7b2566", "Customer", "CUSTOMER" });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_AppUserId",
                table: "Restaurants",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_AppUserId",
                table: "Restaurants",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_AppUserId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_AppUserId",
                table: "Restaurants");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "38de4664-f62d-410e-b918-cec751aa4a1d", "adc0ea8d-39e9-43a2-a362-c460e57497d3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "524910cd-9a53-43ac-b3c6-0340c10384de", "b7e9c44b-63d2-49b0-815a-5092642f1e45" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "b207baae-0696-4fcf-9807-1214981e6b64", "76cca271-ea20-4f17-af6c-25706d7b2566" });

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Restaurants");

            migrationBuilder.AddColumn<string>(
                name: "ManagerId",
                table: "Restaurants",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a3385738-ef48-4b3d-8769-2a0dd5916666", "5dcbfac0-8cd8-43d0-b976-9afefb066cd7", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e08fb599-8d4c-4451-8e9d-8935ccf75eb5", "66a3a3ed-8ce8-4be2-aafd-386ec04c5e62", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ff475a7c-9795-42fa-b1b0-7b39f831ef55", "50e80e92-6222-42d8-869c-24f4612bc989", "Customer", "CUSTOMER" });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_ManagerId",
                table: "Restaurants",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_ManagerId",
                table: "Restaurants",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
