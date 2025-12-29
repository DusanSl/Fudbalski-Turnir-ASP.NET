using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fudbalski_turnir.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CleanedUpMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Klub",
                columns: table => new
                {
                    KlubID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeKluba = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GodinaOsnivanja = table.Column<int>(type: "int", nullable: false),
                    RankingTima = table.Column<int>(type: "int", nullable: false),
                    BrojIgraca = table.Column<int>(type: "int", nullable: false),
                    Stadion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nacionalnost = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
                    ImeSponzora = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KontaktSponzora = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
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
                    NazivTurnira = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lokacija = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DatumPocetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumZavrsetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipTurnira = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnir", x => x.TurnirID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Igraci",
                columns: table => new
                {
                    OsobaID = table.Column<int>(type: "int", nullable: false),
                    KlubID = table.Column<int>(type: "int", nullable: false),
                    Pozicija = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BrojDresa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igraci", x => x.OsobaID);
                    table.ForeignKey(
                        name: "FK_Igraci_Klub_KlubID",
                        column: x => x.KlubID,
                        principalTable: "Klub",
                        principalColumn: "KlubID",
                        onDelete: ReferentialAction.Cascade);
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
                    KlubID = table.Column<int>(type: "int", nullable: false),
                    GodineIskustva = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menadzeri", x => x.OsobaID);
                    table.ForeignKey(
                        name: "FK_Menadzeri_Klub_KlubID",
                        column: x => x.KlubID,
                        principalTable: "Klub",
                        principalColumn: "KlubID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Menadzeri_Osoba_OsobaID",
                        column: x => x.OsobaID,
                        principalTable: "Osoba",
                        principalColumn: "OsobaID",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Utakmica",
                columns: table => new
                {
                    UtakmicaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurnirID = table.Column<int>(type: "int", nullable: true),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mesto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrviKlubNaziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DrugiKlubNaziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Kolo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PrviKlubGolovi = table.Column<int>(type: "int", nullable: false),
                    DrugiKlubGolovi = table.Column<int>(type: "int", nullable: false),
                    Produzeci = table.Column<bool>(type: "bit", nullable: false),
                    Penali = table.Column<bool>(type: "bit", nullable: false),
                    PrviKlubPenali = table.Column<int>(type: "int", nullable: true),
                    DrugiKlubPenali = table.Column<int>(type: "int", nullable: true),
                    Grupa = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utakmica", x => x.UtakmicaID);
                    table.ForeignKey(
                        name: "FK_Utakmica_Turnir_TurnirID",
                        column: x => x.TurnirID,
                        principalTable: "Turnir",
                        principalColumn: "TurnirID");
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Igra_UtakmiceUtakmicaID",
                table: "Igra",
                column: "UtakmiceUtakmicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Igraci_KlubID",
                table: "Igraci",
                column: "KlubID");

            migrationBuilder.CreateIndex(
                name: "IX_Menadzeri_KlubID",
                table: "Menadzeri",
                column: "KlubID");

            migrationBuilder.CreateIndex(
                name: "IX_Sponzorise_TurnirID",
                table: "Sponzorise",
                column: "TurnirID");

            migrationBuilder.CreateIndex(
                name: "IX_Ucestvuje_TurnirID",
                table: "Ucestvuje",
                column: "TurnirID");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmica_TurnirID",
                table: "Utakmica",
                column: "TurnirID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Igra");

            migrationBuilder.DropTable(
                name: "Igraci");

            migrationBuilder.DropTable(
                name: "Menadzeri");

            migrationBuilder.DropTable(
                name: "Sponzorise");

            migrationBuilder.DropTable(
                name: "Ucestvuje");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Utakmica");

            migrationBuilder.DropTable(
                name: "Osoba");

            migrationBuilder.DropTable(
                name: "Sponzor");

            migrationBuilder.DropTable(
                name: "Klub");

            migrationBuilder.DropTable(
                name: "Turnir");
        }
    }
}
