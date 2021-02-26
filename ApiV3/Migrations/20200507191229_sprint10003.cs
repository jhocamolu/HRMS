using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint10003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudVacaciones_Nomina_NominaId",
                table: "SolicitudVacaciones");

            migrationBuilder.DropIndex(
                name: "IX_SolicitudVacaciones_NominaId",
                table: "SolicitudVacaciones");

            migrationBuilder.DropColumn(
                name: "NominaId",
                table: "SolicitudVacaciones");

            migrationBuilder.AlterColumn<decimal>(
                name: "Remuneracion",
                table: "SolicitudVacaciones",
                type: "money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AddColumn<int>(
                name: "NominaFuncionarioId",
                table: "SolicitudVacaciones",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudVacaciones_NominaFuncionarioId",
                table: "SolicitudVacaciones",
                column: "NominaFuncionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudVacaciones_NominaFuncionario_NominaFuncionarioId",
                table: "SolicitudVacaciones",
                column: "NominaFuncionarioId",
                principalTable: "NominaFuncionario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudVacaciones_NominaFuncionario_NominaFuncionarioId",
                table: "SolicitudVacaciones");

            migrationBuilder.DropIndex(
                name: "IX_SolicitudVacaciones_NominaFuncionarioId",
                table: "SolicitudVacaciones");

            migrationBuilder.DropColumn(
                name: "NominaFuncionarioId",
                table: "SolicitudVacaciones");

            migrationBuilder.AlterColumn<decimal>(
                name: "Remuneracion",
                table: "SolicitudVacaciones",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NominaId",
                table: "SolicitudVacaciones",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudVacaciones_NominaId",
                table: "SolicitudVacaciones",
                column: "NominaId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudVacaciones_Nomina_NominaId",
                table: "SolicitudVacaciones",
                column: "NominaId",
                principalTable: "Nomina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
