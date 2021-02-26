using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint13031 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_TipoCotizanteSubtipoCotizante_TipoCotizanteSubtipoCotizanteID",
                table: "Contrato");

            migrationBuilder.RenameColumn(
                name: "TipoCotizanteSubtipoCotizanteID",
                table: "Contrato",
                newName: "TipoCotizanteSubtipoCotizanteId");

            migrationBuilder.RenameIndex(
                name: "IX_Contrato_TipoCotizanteSubtipoCotizanteID",
                table: "Contrato",
                newName: "IX_Contrato_TipoCotizanteSubtipoCotizanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_TipoCotizanteSubtipoCotizante_TipoCotizanteSubtipoCotizanteId",
                table: "Contrato",
                column: "TipoCotizanteSubtipoCotizanteId",
                principalTable: "TipoCotizanteSubtipoCotizante",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_TipoCotizanteSubtipoCotizante_TipoCotizanteSubtipoCotizanteId",
                table: "Contrato");

            migrationBuilder.RenameColumn(
                name: "TipoCotizanteSubtipoCotizanteId",
                table: "Contrato",
                newName: "TipoCotizanteSubtipoCotizanteID");

            migrationBuilder.RenameIndex(
                name: "IX_Contrato_TipoCotizanteSubtipoCotizanteId",
                table: "Contrato",
                newName: "IX_Contrato_TipoCotizanteSubtipoCotizanteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_TipoCotizanteSubtipoCotizante_TipoCotizanteSubtipoCotizanteID",
                table: "Contrato",
                column: "TipoCotizanteSubtipoCotizanteID",
                principalTable: "TipoCotizanteSubtipoCotizante",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
