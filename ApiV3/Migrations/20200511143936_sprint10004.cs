using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint10004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AusentismoFuncionario_Funcionario_FuncionarioId",
                table: "AusentismoFuncionario");

            migrationBuilder.DropForeignKey(
                name: "FK_AusentismoFuncionario_TipoAusentismo_TipoAusentismoId",
                table: "AusentismoFuncionario");

            migrationBuilder.DropForeignKey(
                name: "FK_NominaDetalle_ConceptoNomina_ConceptoNominaId",
                table: "NominaDetalle");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_Cargo_Clase",
                table: "Cargo",
                sql: "([Clase]='CentroOperativo' OR [Clase]='Nacional')");

            migrationBuilder.AlterColumn<string>(
                name: "Tamanio",
                table: "VariableNomina",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Inconsistencia",
                table: "NominaDetalle",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "NombreParametro",
                table: "FuncionVariable",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "JefeInmediato",
                table: "CargoReporta",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CostoSicom",
                table: "Cargo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_AusentismoFuncionario_Funcionario_FuncionarioId",
                table: "AusentismoFuncionario",
                column: "FuncionarioId",
                principalTable: "Funcionario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AusentismoFuncionario_TipoAusentismo_TipoAusentismoId",
                table: "AusentismoFuncionario",
                column: "TipoAusentismoId",
                principalTable: "TipoAusentismo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NominaDetalle_ConceptoNomina_ConceptoNominaId",
                table: "NominaDetalle",
                column: "ConceptoNominaId",
                principalTable: "ConceptoNomina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AusentismoFuncionario_Funcionario_FuncionarioId",
                table: "AusentismoFuncionario");

            migrationBuilder.DropForeignKey(
                name: "FK_AusentismoFuncionario_TipoAusentismo_TipoAusentismoId",
                table: "AusentismoFuncionario");

            migrationBuilder.DropForeignKey(
                name: "FK_NominaDetalle_ConceptoNomina_ConceptoNominaId",
                table: "NominaDetalle");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Cargo_Clase",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "NombreParametro",
                table: "FuncionVariable");

            migrationBuilder.DropColumn(
                name: "JefeInmediato",
                table: "CargoReporta");

            migrationBuilder.DropColumn(
                name: "CostoSicom",
                table: "Cargo");

            migrationBuilder.AlterColumn<string>(
                name: "Tamanio",
                table: "VariableNomina",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Inconsistencia",
                table: "NominaDetalle",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AusentismoFuncionario_Funcionario_FuncionarioId",
                table: "AusentismoFuncionario",
                column: "FuncionarioId",
                principalTable: "Funcionario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AusentismoFuncionario_TipoAusentismo_TipoAusentismoId",
                table: "AusentismoFuncionario",
                column: "TipoAusentismoId",
                principalTable: "TipoAusentismo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NominaDetalle_ConceptoNomina_ConceptoNominaId",
                table: "NominaDetalle",
                column: "ConceptoNominaId",
                principalTable: "ConceptoNomina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
