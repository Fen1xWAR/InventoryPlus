using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryPlus.Infrastructure.Migrations
{
    public partial class UpdateConsumitionNode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumables_Buildings_BuildingId",
                table: "Consumables");

            migrationBuilder.RenameColumn(
                name: "BuildingId",
                table: "Consumables",
                newName: "CabinetId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumables_BuildingId",
                table: "Consumables",
                newName: "IX_Consumables_CabinetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumables_Cabinets_CabinetId",
                table: "Consumables",
                column: "CabinetId",
                principalTable: "Cabinets",
                principalColumn: "CabinetId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumables_Cabinets_CabinetId",
                table: "Consumables");

            migrationBuilder.RenameColumn(
                name: "CabinetId",
                table: "Consumables",
                newName: "BuildingId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumables_CabinetId",
                table: "Consumables",
                newName: "IX_Consumables_BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumables_Buildings_BuildingId",
                table: "Consumables",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "BuildingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
