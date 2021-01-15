using Microsoft.EntityFrameworkCore.Migrations;

namespace VSApi.Migrations
{
    public partial class RemoveOwnFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnFlag",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "OwnFlag",
                table: "Cryptos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnFlag",
                table: "Wallet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnFlag",
                table: "Cryptos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
