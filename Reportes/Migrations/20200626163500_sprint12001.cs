using Microsoft.EntityFrameworkCore.Migrations;

namespace Reportes.Migrations
{
    public partial class sprint12001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReporteParametro_Parametro_ParametroId",
                table: "ReporteParametro");

            migrationBuilder.DropForeignKey(
                name: "FK_ReporteParametro_Reporte_ReporteId",
                table: "ReporteParametro");

            migrationBuilder.AddForeignKey(
                name: "FK_ReporteParametro_Parametro_ParametroId",
                table: "ReporteParametro",
                column: "ParametroId",
                principalTable: "Parametro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReporteParametro_Reporte_ReporteId",
                table: "ReporteParametro",
                column: "ReporteId",
                principalTable: "Reporte",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReporteParametro_Parametro_ParametroId",
                table: "ReporteParametro");

            migrationBuilder.DropForeignKey(
                name: "FK_ReporteParametro_Reporte_ReporteId",
                table: "ReporteParametro");

            migrationBuilder.AddForeignKey(
                name: "FK_ReporteParametro_Parametro_ParametroId",
                table: "ReporteParametro",
                column: "ParametroId",
                principalTable: "Parametro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReporteParametro_Reporte_ReporteId",
                table: "ReporteParametro",
                column: "ReporteId",
                principalTable: "Reporte",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
