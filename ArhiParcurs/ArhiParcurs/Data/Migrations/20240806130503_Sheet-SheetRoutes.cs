using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArhiParcurs.Migrations
{
    /// <inheritdoc />
    public partial class SheetSheetRoutes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Vehicles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "Vehicles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsDeleted",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Sheets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "Sheets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsDeleted",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Sheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SheetRoutes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "SheetRoutes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "SheetRoutes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsDeleted",
                table: "SheetRoutes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SheetId",
                table: "SheetRoutes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SheetRoutes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Partners",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Partners",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "Partners",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsDeleted",
                table: "Partners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Partners",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Fuels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Fuels",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "Fuels",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsDeleted",
                table: "Fuels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Fuels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ConsumptionIncreases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "ConsumptionIncreases",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "ConsumptionIncreases",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsDeleted",
                table: "ConsumptionIncreases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ConsumptionIncreases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SheetRoutes_SheetId",
                table: "SheetRoutes",
                column: "SheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_SheetRoutes_Sheets_SheetId",
                table: "SheetRoutes",
                column: "SheetId",
                principalTable: "Sheets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SheetRoutes_Sheets_SheetId",
                table: "SheetRoutes");

            migrationBuilder.DropIndex(
                name: "IX_SheetRoutes_SheetId",
                table: "SheetRoutes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SheetRoutes");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "SheetRoutes");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "SheetRoutes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SheetRoutes");

            migrationBuilder.DropColumn(
                name: "SheetId",
                table: "SheetRoutes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SheetRoutes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Fuels");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Fuels");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Fuels");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Fuels");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Fuels");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ConsumptionIncreases");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "ConsumptionIncreases");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "ConsumptionIncreases");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ConsumptionIncreases");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ConsumptionIncreases");
        }
    }
}
