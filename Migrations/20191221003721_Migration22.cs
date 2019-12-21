using Microsoft.EntityFrameworkCore.Migrations;

namespace App.API.Migrations
{
    public partial class Migration22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Change",
                table: "Wallet",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Change",
                table: "Wallet");
        }
    }
}
