using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fudbalski_turnir.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeletedTipUcescaUKlubuOdAndDo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipUcesca",
                table: "Utakmica");

            migrationBuilder.DropColumn(
                name: "UKlubuDo",
                table: "Osoba");

            migrationBuilder.DropColumn(
                name: "UKlubuOd",
                table: "Osoba");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipUcesca",
                table: "Utakmica",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UKlubuDo",
                table: "Osoba",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UKlubuOd",
                table: "Osoba",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
