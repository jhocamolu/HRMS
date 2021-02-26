using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint10009 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateCheckConstraint(
                name: "CK_Funcionario_Estado",
                table: "Funcionario",
                sql: "([Estado]='Activo' OR [Estado]='EnVacaciones' OR [Estado]='Retirado' OR [Estado]='Seleccionado' OR [Estado]='Incapacitado')");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_Contrato_Estado",
                table: "Contrato",
                sql: "([Estado]='Vigente' OR [Estado]='SinIniciar' OR [Estado]='Terminado' OR [Estado]='Cancelado' OR [Estado]='PendientePorLiquidar')");

            migrationBuilder.AddColumn<int>(
                name: "CargoGrupoId",
                table: "Contrato",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Justificacion",
                table: "Contrato",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoPeriodoId",
                table: "Contrato",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_CargoGrupoId",
                table: "Contrato",
                column: "CargoGrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_TipoPeriodoId",
                table: "Contrato",
                column: "TipoPeriodoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_CargoGrupo_CargoGrupoId",
                table: "Contrato",
                column: "CargoGrupoId",
                principalTable: "CargoGrupo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_TipoPeriodo_TipoPeriodoId",
                table: "Contrato",
                column: "TipoPeriodoId",
                principalTable: "TipoPeriodo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_CargoGrupo_CargoGrupoId",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_TipoPeriodo_TipoPeriodoId",
                table: "Contrato");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Funcionario_Estado",
                table: "Funcionario");

            migrationBuilder.DropIndex(
                name: "IX_Contrato_CargoGrupoId",
                table: "Contrato");

            migrationBuilder.DropIndex(
                name: "IX_Contrato_TipoPeriodoId",
                table: "Contrato");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Contrato_Estado",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "CargoGrupoId",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "Justificacion",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "TipoPeriodoId",
                table: "Contrato");
        }
    }
}
