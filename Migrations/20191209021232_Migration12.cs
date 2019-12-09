using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Advantage.API.Migrations
{
    public partial class Migration12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cryptos",
                columns: table => new
                {
                    idCrypto = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Rank = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Symbol = table.Column<string>(nullable: true),
                    Price = table.Column<string>(nullable: true),
                    Change24h = table.Column<string>(nullable: true),
                    Change7d = table.Column<string>(nullable: true),
                    ownFlag = table.Column<int>(nullable: false),
                    Quantity = table.Column<string>(nullable: true),
                    Sum = table.Column<string>(nullable: true),
                    AlertUp = table.Column<string>(nullable: true),
                    AlertDown = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cryptos", x => x.idCrypto);
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    idCrypto = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Rank = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Symbol = table.Column<string>(nullable: true),
                    Price = table.Column<string>(nullable: true),
                    Change24h = table.Column<string>(nullable: true),
                    Change7d = table.Column<string>(nullable: true),
                    ownFlag = table.Column<int>(nullable: false),
                    Quantity = table.Column<string>(nullable: true),
                    Sum = table.Column<string>(nullable: true),
                    AlertUp = table.Column<string>(nullable: true),
                    AlertDown = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.idCrypto);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cryptos");

            migrationBuilder.DropTable(
                name: "Wallet");
        }
    }
}
