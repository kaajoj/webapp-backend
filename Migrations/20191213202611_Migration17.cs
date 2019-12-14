using Microsoft.EntityFrameworkCore.Migrations;

namespace App.API.Migrations
{
    public partial class Migration17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OldPrice",
                table: "Wallet",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OldPrice",
                table: "Wallet");
        }
    }
}
