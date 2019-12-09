using Microsoft.EntityFrameworkCore.Migrations;

namespace Advantage.API.Migrations
{
    public partial class Migration9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Walletcontext",
                table: "Walletcontext");

            migrationBuilder.RenameTable(
                name: "Walletcontext",
                newName: "Wallet");

            migrationBuilder.RenameColumn(
                name: "sum",
                table: "Wallet",
                newName: "Sum");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "Wallet",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "alertUp",
                table: "Wallet",
                newName: "AlertUp");

            migrationBuilder.RenameColumn(
                name: "alertDown",
                table: "Wallet",
                newName: "AlertDown");

            migrationBuilder.RenameColumn(
                name: "startPrice",
                table: "Wallet",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "changePrice",
                table: "Wallet",
                newName: "Change7d");

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

            migrationBuilder.AddColumn<string>(
                name: "Change24h",
                table: "Wallet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Wallet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ownFlag",
                table: "Wallet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallet",
                table: "Wallet",
                column: "idCrypto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallet",
                table: "Wallet");

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

            migrationBuilder.DropColumn(
                name: "Change24h",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "ownFlag",
                table: "Wallet");

            migrationBuilder.RenameTable(
                name: "Wallet",
                newName: "Walletcontext");

            migrationBuilder.RenameColumn(
                name: "Sum",
                table: "Walletcontext",
                newName: "sum");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Walletcontext",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "AlertUp",
                table: "Walletcontext",
                newName: "alertUp");

            migrationBuilder.RenameColumn(
                name: "AlertDown",
                table: "Walletcontext",
                newName: "alertDown");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Walletcontext",
                newName: "startPrice");

            migrationBuilder.RenameColumn(
                name: "Change7d",
                table: "Walletcontext",
                newName: "changePrice");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Walletcontext",
                table: "Walletcontext",
                column: "idCrypto");
        }
    }
}
