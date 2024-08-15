using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArhiParcurs.Migrations
{
    /// <inheritdoc />
    public partial class Vehicle_Branch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Vehicles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_BranchId",
                table: "Vehicles",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Branches_BranchId",
                table: "Vehicles",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Branches_BranchId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_BranchId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Vehicles");
        }
    }
}
