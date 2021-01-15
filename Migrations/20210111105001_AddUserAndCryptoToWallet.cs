using Microsoft.EntityFrameworkCore.Migrations;

namespace VSApi.Migrations
{
    public partial class AddUserAndCryptoToWallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CryptoId",
                table: "Wallet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Wallet",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_CryptoId",
                table: "Wallet",
                column: "CryptoId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_UserId",
                table: "Wallet",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_Cryptos_CryptoId",
                table: "Wallet",
                column: "CryptoId",
                principalTable: "Cryptos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_AspNetUsers_UserId",
                table: "Wallet",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_Cryptos_CryptoId",
                table: "Wallet");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_AspNetUsers_UserId",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_CryptoId",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_UserId",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "CryptoId",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Wallet");
        }
    }
}
