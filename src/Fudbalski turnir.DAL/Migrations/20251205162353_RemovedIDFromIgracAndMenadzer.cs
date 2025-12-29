using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fudbalski_turnir.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedIDFromIgracAndMenadzer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenadzerID",
                table: "Menadzeri");

            migrationBuilder.DropColumn(
                name: "IgracID",
                table: "Igraci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenadzerID",
                table: "Menadzeri",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IgracID",
                table: "Igraci",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
