using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodService.Migrations
{
    public partial class PriceRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Prices_PriceId",
                table: "Meals");

            migrationBuilder.AlterColumn<long>(
                name: "PriceId",
                table: "Meals",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Prices_PriceId",
                table: "Meals",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "PriceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Prices_PriceId",
                table: "Meals");

            migrationBuilder.AlterColumn<long>(
                name: "PriceId",
                table: "Meals",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Prices_PriceId",
                table: "Meals",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "PriceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
