using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint13030 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_TipoCotizanteSubtipoCotizante_TipoCotizanteSubtipoCotizanteID",
                table: "Contrato");

            migrationBuilder.AlterColumn<int>(
                name: "TipoCotizanteSubtipoCotizanteID",
                table: "Contrato",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_TipoCotizanteSubtipoCotizante_TipoCotizanteSubtipoCotizanteID",
                table: "Contrato",
                column: "TipoCotizanteSubtipoCotizanteID",
                principalTable: "TipoCotizanteSubtipoCotizante",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_TipoCotizanteSubtipoCotizante_TipoCotizanteSubtipoCotizanteID",
                table: "Contrato");

            migrationBuilder.AlterColumn<int>(
                name: "TipoCotizanteSubtipoCotizanteID",
                table: "Contrato",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_TipoCotizanteSubtipoCotizante_TipoCotizanteSubtipoCotizanteID",
                table: "Contrato",
                column: "TipoCotizanteSubtipoCotizanteID",
                principalTable: "TipoCotizanteSubtipoCotizante",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
