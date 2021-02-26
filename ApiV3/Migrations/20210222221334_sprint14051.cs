using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14051 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActividadCentroCostoId",
                table: "CargoCentroCosto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CargoCentroCosto_ActividadCentroCostoId",
                table: "CargoCentroCosto",
                column: "ActividadCentroCostoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoCentroCosto_ActividadCentroCosto_ActividadCentroCostoId",
                table: "CargoCentroCosto",
                column: "ActividadCentroCostoId",
                principalTable: "ActividadCentroCosto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoCentroCosto_ActividadCentroCosto_ActividadCentroCostoId",
                table: "CargoCentroCosto");

            migrationBuilder.DropIndex(
                name: "IX_CargoCentroCosto_ActividadCentroCostoId",
                table: "CargoCentroCosto");

            migrationBuilder.DropColumn(
                name: "ActividadCentroCostoId",
                table: "CargoCentroCosto");
        }
    }
}
