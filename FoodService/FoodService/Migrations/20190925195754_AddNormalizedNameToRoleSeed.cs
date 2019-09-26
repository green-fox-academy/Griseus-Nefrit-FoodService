using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodService.Migrations
{
    public partial class AddNormalizedNameToRoleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "b39d07cd-4d68-419c-a142-6abf616c708c", "9390c869-59f0-4afc-b35a-1d295e8b9656" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "d115cee6-57ef-4647-8e9e-e82043a80240", "2ca65f4d-f25d-4433-9658-80867552e528" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "ecb80185-1e75-4058-bd8a-2b11342234d5", "f9e5c0f0-5cbc-4d63-8338-a9657facd22f" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5a880449-8a1e-4e27-bcc6-6761ef51e5e8", "63fb047f-6974-47d8-ab91-36fe309d14ea", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d944b903-e132-4a60-b70d-a03860221b94", "f587aed2-a9af-466c-ae09-ed190c61b80e", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7c55c4cf-e0be-452f-90b5-d2070bf7b526", "5f58771e-6d5a-4684-aff7-9abcf53f2830", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "5a880449-8a1e-4e27-bcc6-6761ef51e5e8", "63fb047f-6974-47d8-ab91-36fe309d14ea" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "7c55c4cf-e0be-452f-90b5-d2070bf7b526", "5f58771e-6d5a-4684-aff7-9abcf53f2830" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "d944b903-e132-4a60-b70d-a03860221b94", "f587aed2-a9af-466c-ae09-ed190c61b80e" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d115cee6-57ef-4647-8e9e-e82043a80240", "2ca65f4d-f25d-4433-9658-80867552e528", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b39d07cd-4d68-419c-a142-6abf616c708c", "9390c869-59f0-4afc-b35a-1d295e8b9656", "Manager", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ecb80185-1e75-4058-bd8a-2b11342234d5", "f9e5c0f0-5cbc-4d63-8338-a9657facd22f", "Customer", null });
        }
    }
}
