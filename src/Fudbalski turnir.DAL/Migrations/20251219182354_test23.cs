using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fudbalski_turnir.Data.Migrations
{
    /// <inheritdoc />
    public partial class test23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Menadzeri_KlubID",
                table: "Menadzeri",
                column: "KlubID");

            migrationBuilder.CreateIndex(
                name: "IX_Igraci_KlubID",
                table: "Igraci",
                column: "KlubID");

            migrationBuilder.AddForeignKey(
                name: "FK_Igraci_Klub_KlubID",
                table: "Igraci",
                column: "KlubID",
                principalTable: "Klub",
                principalColumn: "KlubID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Menadzeri_Klub_KlubID",
                table: "Menadzeri",
                column: "KlubID",
                principalTable: "Klub",
                principalColumn: "KlubID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Igraci_Klub_KlubID",
                table: "Igraci");

            migrationBuilder.DropForeignKey(
                name: "FK_Menadzeri_Klub_KlubID",
                table: "Menadzeri");

            migrationBuilder.DropIndex(
                name: "IX_Menadzeri_KlubID",
                table: "Menadzeri");

            migrationBuilder.DropIndex(
                name: "IX_Igraci_KlubID",
                table: "Igraci");
        }
    }
}
