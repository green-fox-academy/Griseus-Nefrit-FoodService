using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodService.Migrations
{
    public partial class InitAndMapRestaurantToUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "110d96be-7146-4d9d-85d7-4a1a9a7bec72", "ff5897ac-f9ad-4d79-b00e-5e0f53d24cf5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "227e064a-36dd-41c5-92f0-4320cd1bc13b", "081546cb-f1fc-476d-a341-e0d2da19e788" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "fcd6cf29-56fb-4546-b0b3-3bc677353d44", "4226118d-de62-4399-9bfd-3a64a852290a" });

            migrationBuilder.AddColumn<string>(
                name: "Teszt",
                table: "AspNetUsers",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "Teszt",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "227e064a-36dd-41c5-92f0-4320cd1bc13b", "081546cb-f1fc-476d-a341-e0d2da19e788", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fcd6cf29-56fb-4546-b0b3-3bc677353d44", "4226118d-de62-4399-9bfd-3a64a852290a", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "110d96be-7146-4d9d-85d7-4a1a9a7bec72", "ff5897ac-f9ad-4d79-b00e-5e0f53d24cf5", "Customer", "CUSTOMER" });
        }
    }
}
