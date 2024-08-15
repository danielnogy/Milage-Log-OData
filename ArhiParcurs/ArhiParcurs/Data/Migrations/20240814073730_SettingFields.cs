using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArhiParcurs.Migrations
{
    /// <inheritdoc />
    public partial class SettingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CUI",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DenumireFirma",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NrInreg",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "CUI",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "DenumireFirma",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "NrInreg",
                table: "Settings");
        }
    }
}
