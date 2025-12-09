using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fudbalski_turnir.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUtakmicaToTurnir : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TurnirID",
                table: "Utakmica",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utakmica_TurnirID",
                table: "Utakmica",
                column: "TurnirID");

            migrationBuilder.AddForeignKey(
                name: "FK_Utakmica_Turnir_TurnirID",
                table: "Utakmica",
                column: "TurnirID",
                principalTable: "Turnir",
                principalColumn: "TurnirID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utakmica_Turnir_TurnirID",
                table: "Utakmica");

            migrationBuilder.DropIndex(
                name: "IX_Utakmica_TurnirID",
                table: "Utakmica");

            migrationBuilder.DropColumn(
                name: "TurnirID",
                table: "Utakmica");
        }
    }
}
