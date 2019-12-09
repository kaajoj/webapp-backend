using Microsoft.EntityFrameworkCore.Migrations;

namespace Advantage.API.Migrations
{
    public partial class Migration13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlertDown",
                table: "Cryptos");

            migrationBuilder.DropColumn(
                name: "AlertUp",
                table: "Cryptos");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Cryptos");

            migrationBuilder.DropColumn(
                name: "Sum",
                table: "Cryptos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlertDown",
                table: "Cryptos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlertUp",
                table: "Cryptos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quantity",
                table: "Cryptos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sum",
                table: "Cryptos",
                nullable: true);
        }
    }
}
