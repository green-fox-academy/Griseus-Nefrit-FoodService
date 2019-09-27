using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodService.Migrations
{
    public partial class check0926 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "7379e162-480a-4220-85b4-776c421a6946", "ef451a84-a658-4df7-b299-50cd45fb0b03" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "7bb9c4e7-254e-4a9e-9ad6-095a12a80870", "63624cc7-6d08-4e44-ae03-d3e01aa910b0" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "92e49094-a928-4cb4-87e2-8876e8f0a445", "296f6cb4-1e34-4e06-99fe-01d512355dab" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4eb15690-990f-4253-8c07-5aa8d6fcdcb6", "a1b01dc8-a1f8-4508-88be-e8e92b7d1267", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "23f223b1-8f4b-428b-a55b-a69af477b6c9", "7462f32e-d1ac-4f64-9833-869322d578da", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "80ae65d0-6bed-481f-acbb-5076a218f8cb", "c20e61cb-0cf9-42bf-ac9d-f96044b6d04e", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "23f223b1-8f4b-428b-a55b-a69af477b6c9", "7462f32e-d1ac-4f64-9833-869322d578da" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "4eb15690-990f-4253-8c07-5aa8d6fcdcb6", "a1b01dc8-a1f8-4508-88be-e8e92b7d1267" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "80ae65d0-6bed-481f-acbb-5076a218f8cb", "c20e61cb-0cf9-42bf-ac9d-f96044b6d04e" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7379e162-480a-4220-85b4-776c421a6946", "ef451a84-a658-4df7-b299-50cd45fb0b03", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "92e49094-a928-4cb4-87e2-8876e8f0a445", "296f6cb4-1e34-4e06-99fe-01d512355dab", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7bb9c4e7-254e-4a9e-9ad6-095a12a80870", "63624cc7-6d08-4e44-ae03-d3e01aa910b0", "Customer", "CUSTOMER" });
        }
    }
}
