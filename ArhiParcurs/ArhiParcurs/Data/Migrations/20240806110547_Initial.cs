using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArhiParcurs.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsumptionIncreases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Percent = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumptionIncreases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fuels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TVA = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Function = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SSN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartnerState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SheetRoutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    End = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VIA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalDistance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalEquivalentDistance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Distance1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Distance2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Distance3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Distance4 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Distance5 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Distance6 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteType = table.Column<int>(type: "int", nullable: false),
                    ConsumptionIncreaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheetRoutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SheetRoutes_ConsumptionIncreases_ConsumptionIncreaseId",
                        column: x => x.ConsumptionIncreaseId,
                        principalTable: "ConsumptionIncreases",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuelConsumption = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kilometers = table.Column<int>(type: "int", nullable: false),
                    FuelId = table.Column<int>(type: "int", nullable: false),
                    VehicleState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Fuels_FuelId",
                        column: x => x.FuelId,
                        principalTable: "Fuels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    InitialFuel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Refuels = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Consumed = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FuelLeft = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateBegin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsumptionIncreaseId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sheets_ConsumptionIncreases_ConsumptionIncreaseId",
                        column: x => x.ConsumptionIncreaseId,
                        principalTable: "ConsumptionIncreases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sheets_Partners_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Partners",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sheets_Partners_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Partners",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sheets_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SheetRoutes_ConsumptionIncreaseId",
                table: "SheetRoutes",
                column: "ConsumptionIncreaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Sheets_ConsumptionIncreaseId",
                table: "Sheets",
                column: "ConsumptionIncreaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Sheets_CreatorId",
                table: "Sheets",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sheets_DriverId",
                table: "Sheets",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Sheets_VehicleId",
                table: "Sheets",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_FuelId",
                table: "Vehicles",
                column: "FuelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SheetRoutes");

            migrationBuilder.DropTable(
                name: "Sheets");

            migrationBuilder.DropTable(
                name: "ConsumptionIncreases");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Fuels");
        }
    }
}
