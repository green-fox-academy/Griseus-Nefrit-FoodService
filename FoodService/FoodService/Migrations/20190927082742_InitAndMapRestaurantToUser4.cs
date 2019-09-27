using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodService.Migrations
{
    public partial class InitAndMapRestaurantToUser4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Teszt",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ManagerId",
                table: "Restaurants",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3792ceca-cb2a-4d35-bfda-14267c1615a5", "18708b7f-c1ba-406e-a800-0813c4fa1643", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7b17f047-e5d1-467c-a2aa-57e4f803a566", "e9ac0dbd-35ff-47eb-9f91-6efcf0d61349", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2d6def54-9942-4971-bb1c-710062751f01", "b93576b6-e891-4900-b8e5-645d6b1f4f17", "Customer", "CUSTOMER" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                keyValues: new object[] { "2d6def54-9942-4971-bb1c-710062751f01", "b93576b6-e891-4900-b8e5-645d6b1f4f17" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "3792ceca-cb2a-4d35-bfda-14267c1615a5", "18708b7f-c1ba-406e-a800-0813c4fa1643" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "7b17f047-e5d1-467c-a2aa-57e4f803a566", "e9ac0dbd-35ff-47eb-9f91-6efcf0d61349" });

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Restaurants");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Restaurants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Teszt",
                table: "AspNetUsers",
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
    }
}
