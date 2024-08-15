using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArhiParcurs.Migrations
{
    /// <inheritdoc />
    public partial class SheetRoutes_namedelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "SheetRoutes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SheetRoutes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
