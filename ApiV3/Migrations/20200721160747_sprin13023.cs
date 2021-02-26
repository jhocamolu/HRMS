using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprin13023 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Novedad",
                newName: "FechaAplicacion");

            migrationBuilder.CreateIndex(
                name: "IX_Novedad_TerceroId",
                table: "Novedad",
                column: "TerceroId");

            migrationBuilder.CreateIndex(
                name: "IX_Novedad_TipoPeriodoId",
                table: "Novedad",
                column: "TipoPeriodoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Novedad_Tercero_TerceroId",
                table: "Novedad",
                column: "TerceroId",
                principalTable: "Tercero",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Novedad_TipoPeriodo_TipoPeriodoId",
                table: "Novedad",
                column: "TipoPeriodoId",
                principalTable: "TipoPeriodo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Novedad_Tercero_TerceroId",
                table: "Novedad");

            migrationBuilder.DropForeignKey(
                name: "FK_Novedad_TipoPeriodo_TipoPeriodoId",
                table: "Novedad");

            migrationBuilder.DropIndex(
                name: "IX_Novedad_TerceroId",
                table: "Novedad");

            migrationBuilder.DropIndex(
                name: "IX_Novedad_TipoPeriodoId",
                table: "Novedad");

            migrationBuilder.RenameColumn(
                name: "FechaAplicacion",
                table: "Novedad",
                newName: "Fecha");
        }
    }
}
