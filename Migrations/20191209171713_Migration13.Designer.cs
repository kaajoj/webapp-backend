﻿// <auto-generated />
using App.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace App.API.Migrations
{
    [DbContext(typeof(ApiContext))]
    [Migration("20191209171713_Migration13")]
    partial class Migration13
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "3.0.0-preview3.19153.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Advantage.API.Models.Crypto", b =>
                {
                    b.Property<int>("idCrypto")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Change24h");

                    b.Property<string>("Change7d");

                    b.Property<string>("Name");

                    b.Property<string>("Price");

                    b.Property<int>("Rank");

                    b.Property<string>("Symbol");

                    b.Property<int>("ownFlag");

                    b.HasKey("idCrypto");

                    b.ToTable("Cryptos");
                });

            modelBuilder.Entity("Advantage.API.Models.Wallet", b =>
                {
                    b.Property<int>("idCrypto")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlertDown");

                    b.Property<string>("AlertUp");

                    b.Property<string>("Change24h");

                    b.Property<string>("Change7d");

                    b.Property<string>("Name");

                    b.Property<string>("Price");

                    b.Property<string>("Quantity");

                    b.Property<int>("Rank");

                    b.Property<string>("Sum");

                    b.Property<string>("Symbol");

                    b.Property<int>("ownFlag");

                    b.HasKey("idCrypto");

                    b.ToTable("Wallet");
                });
#pragma warning restore 612, 618
        }
    }
}
