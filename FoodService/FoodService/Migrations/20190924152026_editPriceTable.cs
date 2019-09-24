using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodService.Migrations
{
    public partial class editPriceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceAmount",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "PriceCurrency",
                table: "Meals");

            migrationBuilder.AddColumn<long>(
                name: "PriceId",
                table: "Meals",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    PriceId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: false),
                    Currency = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.PriceId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meals_PriceId",
                table: "Meals",
                column: "PriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Prices_PriceId",
                table: "Meals",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "PriceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Prices_PriceId",
                table: "Meals");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Meals_PriceId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "Meals");

            migrationBuilder.AddColumn<int>(
                name: "PriceAmount",
                table: "Meals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PriceCurrency",
                table: "Meals",
                nullable: true);
        }
    }
}
