using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint13029 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ColombianoEnElExterior",
                table: "Contrato",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ExtranjeroNoObligadoACotizarAPension",
                table: "Contrato",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TipoCotizanteSubtipoCotizanteID",
                table: "Contrato",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_TipoCotizanteSubtipoCotizanteID",
                table: "Contrato",
                column: "TipoCotizanteSubtipoCotizanteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_TipoCotizanteSubtipoCotizante_TipoCotizanteSubtipoCotizanteID",
                table: "Contrato",
                column: "TipoCotizanteSubtipoCotizanteID",
                principalTable: "TipoCotizanteSubtipoCotizante",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_TipoCotizanteSubtipoCotizante_TipoCotizanteSubtipoCotizanteID",
                table: "Contrato");

            migrationBuilder.DropIndex(
                name: "IX_Contrato_TipoCotizanteSubtipoCotizanteID",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "ColombianoEnElExterior",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "ExtranjeroNoObligadoACotizarAPension",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "TipoCotizanteSubtipoCotizanteID",
                table: "Contrato");
        }
    }
}
