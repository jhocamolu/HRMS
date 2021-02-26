using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint13019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AplicacionExternaCargo_Cargo_CargoIndependienteId",
                table: "AplicacionExternaCargo");

            migrationBuilder.DropForeignKey(
                name: "FK_AplicacionExternaCargoDependiente_Cargo_CargoDependienteId",
                table: "AplicacionExternaCargoDependiente");

            migrationBuilder.DropIndex(
                name: "IX_AplicacionExternaCargoDependiente_CargoDependienteId",
                table: "AplicacionExternaCargoDependiente");

            migrationBuilder.DropIndex(
                name: "IX_AplicacionExternaCargo_CargoIndependienteId",
                table: "AplicacionExternaCargo");

            migrationBuilder.DropCheckConstraint(
                name: "CK_AplicacionExternaCargo_Tipo",
                table: "AplicacionExternaCargo");

            migrationBuilder.DropColumn(
                name: "CargoDependienteId",
                table: "AplicacionExternaCargoDependiente");

            migrationBuilder.DropColumn(
                name: "CargoIndependienteId",
                table: "AplicacionExternaCargo");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_AplicacionExternaCargo_Tipo",
                table: "AplicacionExternaCargo",
                sql: "([Tipo]='Aprobacion' OR [Tipo]='Autorizacion' OR [Tipo]='Revision')");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_TipoAplicacionExterna_Revisa",
                table: "AplicacionExterna",
                sql: "([Revisa]='JefeInmediato' OR [Revisa]='Otro' OR [Revisa]='NoAplica')");

            migrationBuilder.AddColumn<int>(
                name: "CargoDependenciaId",
                table: "AplicacionExternaCargoDependiente",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CargoDependenciaIndependienteId",
                table: "AplicacionExternaCargo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Revisa",
                table: "AplicacionExterna",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacionExternaCargoDependiente_CargoDependenciaId",
                table: "AplicacionExternaCargoDependiente",
                column: "CargoDependenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacionExternaCargo_CargoDependenciaIndependienteId",
                table: "AplicacionExternaCargo",
                column: "CargoDependenciaIndependienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_AplicacionExternaCargo_CargoDependencia_CargoDependenciaIndependienteId",
                table: "AplicacionExternaCargo",
                column: "CargoDependenciaIndependienteId",
                principalTable: "CargoDependencia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AplicacionExternaCargoDependiente_CargoDependencia_CargoDependenciaId",
                table: "AplicacionExternaCargoDependiente",
                column: "CargoDependenciaId",
                principalTable: "CargoDependencia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AplicacionExternaCargo_CargoDependencia_CargoDependenciaIndependienteId",
                table: "AplicacionExternaCargo");

            migrationBuilder.DropForeignKey(
                name: "FK_AplicacionExternaCargoDependiente_CargoDependencia_CargoDependenciaId",
                table: "AplicacionExternaCargoDependiente");

            migrationBuilder.DropIndex(
                name: "IX_AplicacionExternaCargoDependiente_CargoDependenciaId",
                table: "AplicacionExternaCargoDependiente");

            migrationBuilder.DropIndex(
                name: "IX_AplicacionExternaCargo_CargoDependenciaIndependienteId",
                table: "AplicacionExternaCargo");

            migrationBuilder.DropCheckConstraint(
                name: "CK_AplicacionExternaCargo_Tipo",
                table: "AplicacionExternaCargo");

            migrationBuilder.DropCheckConstraint(
                name: "CK_TipoAplicacionExterna_Revisa",
                table: "AplicacionExterna");

            migrationBuilder.DropColumn(
                name: "CargoDependenciaId",
                table: "AplicacionExternaCargoDependiente");

            migrationBuilder.DropColumn(
                name: "CargoDependenciaIndependienteId",
                table: "AplicacionExternaCargo");

            migrationBuilder.DropColumn(
                name: "Revisa",
                table: "AplicacionExterna");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_AplicacionExternaCargo_Tipo",
                table: "AplicacionExternaCargo",
                sql: "([Tipo]='Aprobacion' OR [Tipo]='Autorizacion')");

            migrationBuilder.AddColumn<int>(
                name: "CargoDependienteId",
                table: "AplicacionExternaCargoDependiente",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CargoIndependienteId",
                table: "AplicacionExternaCargo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AplicacionExternaCargoDependiente_CargoDependienteId",
                table: "AplicacionExternaCargoDependiente",
                column: "CargoDependienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacionExternaCargo_CargoIndependienteId",
                table: "AplicacionExternaCargo",
                column: "CargoIndependienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_AplicacionExternaCargo_Cargo_CargoIndependienteId",
                table: "AplicacionExternaCargo",
                column: "CargoIndependienteId",
                principalTable: "Cargo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AplicacionExternaCargoDependiente_Cargo_CargoDependienteId",
                table: "AplicacionExternaCargoDependiente",
                column: "CargoDependienteId",
                principalTable: "Cargo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
