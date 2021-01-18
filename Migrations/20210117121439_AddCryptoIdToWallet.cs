using Microsoft.EntityFrameworkCore.Migrations;

namespace VSApi.Migrations
{
    public partial class AddCryptoIdToWallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_Cryptos_CryptoId",
                table: "Wallet");

            migrationBuilder.AlterColumn<int>(
                name: "CryptoId",
                table: "Wallet",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_Cryptos_CryptoId",
                table: "Wallet",
                column: "CryptoId",
                principalTable: "Cryptos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_Cryptos_CryptoId",
                table: "Wallet");

            migrationBuilder.AlterColumn<int>(
                name: "CryptoId",
                table: "Wallet",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_Cryptos_CryptoId",
                table: "Wallet",
                column: "CryptoId",
                principalTable: "Cryptos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
