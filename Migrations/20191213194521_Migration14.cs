using Microsoft.EntityFrameworkCore.Migrations;

namespace App.API.Migrations
{
    public partial class Migration14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "idCrypto",
                table: "Wallet",
                newName: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Wallet",
                newName: "idCrypto");
        }
    }
}
