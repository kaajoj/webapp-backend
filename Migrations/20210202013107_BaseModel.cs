using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VSApi.Migrations
{
    public partial class BaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_Cryptos_CryptoId",
                table: "Wallet");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_AspNetUsers_UserId",
                table: "Wallet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallet",
                table: "Wallet");

            migrationBuilder.RenameTable(
                name: "Wallet",
                newName: "Wallets");

            migrationBuilder.RenameIndex(
                name: "IX_Wallet_UserId",
                table: "Wallets",
                newName: "IX_Wallets_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Wallet_CryptoId",
                table: "Wallets",
                newName: "IX_Wallets_CryptoId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Cryptos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Cryptos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Wallets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Wallets",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Cryptos_CryptoId",
                table: "Wallets",
                column: "CryptoId",
                principalTable: "Cryptos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_AspNetUsers_UserId",
                table: "Wallets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Cryptos_CryptoId",
                table: "Wallets");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_AspNetUsers_UserId",
                table: "Wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Cryptos");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Cryptos");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Wallets");

            migrationBuilder.RenameTable(
                name: "Wallets",
                newName: "Wallet");

            migrationBuilder.RenameIndex(
                name: "IX_Wallets_UserId",
                table: "Wallet",
                newName: "IX_Wallet_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Wallets_CryptoId",
                table: "Wallet",
                newName: "IX_Wallet_CryptoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallet",
                table: "Wallet",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_Cryptos_CryptoId",
                table: "Wallet",
                column: "CryptoId",
                principalTable: "Cryptos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_AspNetUsers_UserId",
                table: "Wallet",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
