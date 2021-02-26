using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint11005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_CargoGrupo_CargoGrupoId",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_TipoPeriodo_TipoPeriodoId",
                table: "Contrato");

            migrationBuilder.AlterColumn<int>(
                name: "TipoPeriodoId",
                table: "Contrato",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CargoGrupoId",
                table: "Contrato",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_CargoGrupo_CargoGrupoId",
                table: "Contrato",
                column: "CargoGrupoId",
                principalTable: "CargoGrupo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_TipoPeriodo_TipoPeriodoId",
                table: "Contrato",
                column: "TipoPeriodoId",
                principalTable: "TipoPeriodo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_CargoGrupo_CargoGrupoId",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_TipoPeriodo_TipoPeriodoId",
                table: "Contrato");

            migrationBuilder.AlterColumn<int>(
                name: "TipoPeriodoId",
                table: "Contrato",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CargoGrupoId",
                table: "Contrato",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

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
    }
}
