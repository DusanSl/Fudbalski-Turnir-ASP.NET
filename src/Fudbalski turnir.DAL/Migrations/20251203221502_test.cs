using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fudbalski_turnir.Data.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klub",
                columns: table => new
                {
                    KlubID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeKluba = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GodinaOsnivanja = table.Column<int>(type: "int", nullable: false),
                    RankingTima = table.Column<int>(type: "int", nullable: false),
                    BrojIgraca = table.Column<int>(type: "int", nullable: false),
                    Stadion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojOsvojenihTitula = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klub", x => x.KlubID);
                });

            migrationBuilder.CreateTable(
                name: "Osoba",
                columns: table => new
                {
                    OsobaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nacionalnost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UKlubuOd = table.Column<DateOnly>(type: "date", nullable: false),
                    UKlubuDo = table.Column<DateOnly>(type: "date", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    IgracID = table.Column<int>(type: "int", nullable: true),
                    Igrac_KlubID = table.Column<int>(type: "int", nullable: true),
                    Pozicija = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojDresa = table.Column<int>(type: "int", nullable: true),
                    MenadzerID = table.Column<int>(type: "int", nullable: true),
                    KlubID = table.Column<int>(type: "int", nullable: true),
                    GodineIskustva = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osoba", x => x.OsobaID);
                });

            migrationBuilder.CreateTable(
                name: "Sponzor",
                columns: table => new
                {
                    SponzorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeSponzora = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KontaktSponzora = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VrednostSponzora = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponzor", x => x.SponzorID);
                });

            migrationBuilder.CreateTable(
                name: "Turnir",
                columns: table => new
                {
                    TurnirID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivTurnira = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lokacija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumPocetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumZavrsetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipTurnira = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnir", x => x.TurnirID);
                });

            migrationBuilder.CreateTable(
                name: "Utakmica",
                columns: table => new
                {
                    UtakmicaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mesto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrviKlubNaziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrugiKlubNaziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kolo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrviKlubGolovi = table.Column<int>(type: "int", nullable: false),
                    DrugiKlubGolovi = table.Column<int>(type: "int", nullable: false),
                    Produzeci = table.Column<bool>(type: "bit", nullable: false),
                    Penali = table.Column<bool>(type: "bit", nullable: false),
                    PrviKlubPenali = table.Column<int>(type: "int", nullable: false),
                    DrugiKlubPenali = table.Column<int>(type: "int", nullable: false),
                    TipUcesca = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utakmica", x => x.UtakmicaID);
                });

            migrationBuilder.CreateTable(
                name: "Sponzorise",
                columns: table => new
                {
                    SponzorID = table.Column<int>(type: "int", nullable: false),
                    TurnirID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponzorise", x => new { x.SponzorID, x.TurnirID });
                    table.ForeignKey(
                        name: "FK_Sponzorise_Sponzor_SponzorID",
                        column: x => x.SponzorID,
                        principalTable: "Sponzor",
                        principalColumn: "SponzorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sponzorise_Turnir_TurnirID",
                        column: x => x.TurnirID,
                        principalTable: "Turnir",
                        principalColumn: "TurnirID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ucestvuje",
                columns: table => new
                {
                    KluboviKlubID = table.Column<int>(type: "int", nullable: false),
                    TurnirID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucestvuje", x => new { x.KluboviKlubID, x.TurnirID });
                    table.ForeignKey(
                        name: "FK_Ucestvuje_Klub_KluboviKlubID",
                        column: x => x.KluboviKlubID,
                        principalTable: "Klub",
                        principalColumn: "KlubID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ucestvuje_Turnir_TurnirID",
                        column: x => x.TurnirID,
                        principalTable: "Turnir",
                        principalColumn: "TurnirID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Igra",
                columns: table => new
                {
                    KluboviKlubID = table.Column<int>(type: "int", nullable: false),
                    UtakmiceUtakmicaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igra", x => new { x.KluboviKlubID, x.UtakmiceUtakmicaID });
                    table.ForeignKey(
                        name: "FK_Igra_Klub_KluboviKlubID",
                        column: x => x.KluboviKlubID,
                        principalTable: "Klub",
                        principalColumn: "KlubID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Igra_Utakmica_UtakmiceUtakmicaID",
                        column: x => x.UtakmiceUtakmicaID,
                        principalTable: "Utakmica",
                        principalColumn: "UtakmicaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Igra_UtakmiceUtakmicaID",
                table: "Igra",
                column: "UtakmiceUtakmicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Sponzorise_TurnirID",
                table: "Sponzorise",
                column: "TurnirID");

            migrationBuilder.CreateIndex(
                name: "IX_Ucestvuje_TurnirID",
                table: "Ucestvuje",
                column: "TurnirID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Igra");

            migrationBuilder.DropTable(
                name: "Osoba");

            migrationBuilder.DropTable(
                name: "Sponzorise");

            migrationBuilder.DropTable(
                name: "Ucestvuje");

            migrationBuilder.DropTable(
                name: "Utakmica");

            migrationBuilder.DropTable(
                name: "Sponzor");

            migrationBuilder.DropTable(
                name: "Klub");

            migrationBuilder.DropTable(
                name: "Turnir");
        }
    }
}
