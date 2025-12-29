using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fudbalski_turnir.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeperateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrojDresa",
                table: "Osoba");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Osoba");

            migrationBuilder.DropColumn(
                name: "GodineIskustva",
                table: "Osoba");

            migrationBuilder.DropColumn(
                name: "IgracID",
                table: "Osoba");

            migrationBuilder.DropColumn(
                name: "Igrac_KlubID",
                table: "Osoba");

            migrationBuilder.DropColumn(
                name: "KlubID",
                table: "Osoba");

            migrationBuilder.DropColumn(
                name: "MenadzerID",
                table: "Osoba");

            migrationBuilder.DropColumn(
                name: "Pozicija",
                table: "Osoba");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UKlubuOd",
                table: "Osoba",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UKlubuDo",
                table: "Osoba",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.CreateTable(
                name: "Igraci",
                columns: table => new
                {
                    OsobaID = table.Column<int>(type: "int", nullable: false),
                    IgracID = table.Column<int>(type: "int", nullable: false),
                    KlubID = table.Column<int>(type: "int", nullable: false),
                    Pozicija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojDresa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igraci", x => x.OsobaID);
                    table.ForeignKey(
                        name: "FK_Igraci_Osoba_OsobaID",
                        column: x => x.OsobaID,
                        principalTable: "Osoba",
                        principalColumn: "OsobaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menadzeri",
                columns: table => new
                {
                    OsobaID = table.Column<int>(type: "int", nullable: false),
                    MenadzerID = table.Column<int>(type: "int", nullable: false),
                    KlubID = table.Column<int>(type: "int", nullable: false),
                    GodineIskustva = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menadzeri", x => x.OsobaID);
                    table.ForeignKey(
                        name: "FK_Menadzeri_Osoba_OsobaID",
                        column: x => x.OsobaID,
                        principalTable: "Osoba",
                        principalColumn: "OsobaID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Igraci");

            migrationBuilder.DropTable(
                name: "Menadzeri");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "UKlubuOd",
                table: "Osoba",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "UKlubuDo",
                table: "Osoba",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "BrojDresa",
                table: "Osoba",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Osoba",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GodineIskustva",
                table: "Osoba",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IgracID",
                table: "Osoba",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Igrac_KlubID",
                table: "Osoba",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KlubID",
                table: "Osoba",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MenadzerID",
                table: "Osoba",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pozicija",
                table: "Osoba",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
