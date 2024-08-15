using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArhiParcurs.Migrations
{
    /// <inheritdoc />
    public partial class Settings_BooleanFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "InitialFuelTank",
                table: "Vehicles",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "WithoutPurpose",
                table: "Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WithoutRoundtrip",
                table: "Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WithoutRoutes",
                table: "Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropColumn(
                name: "InitialFuelTank",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "WithoutPurpose",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "WithoutRoundtrip",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "WithoutRoutes",
                table: "Settings");
        }
    }
}
