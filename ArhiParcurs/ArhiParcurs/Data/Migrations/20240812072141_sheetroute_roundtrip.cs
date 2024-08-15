using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArhiParcurs.Migrations
{
    /// <inheritdoc />
    public partial class sheetroute_roundtrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RouteType",
                table: "SheetRoutes");

            migrationBuilder.AddColumn<bool>(
                name: "RoundTrip",
                table: "SheetRoutes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoundTrip",
                table: "SheetRoutes");

            migrationBuilder.AddColumn<int>(
                name: "RouteType",
                table: "SheetRoutes",
                type: "int",
                nullable: true);
        }
    }
}
