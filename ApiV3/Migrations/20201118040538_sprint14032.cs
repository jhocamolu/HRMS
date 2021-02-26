using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14032 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_CentroTrabajo_CentroTrabajoId",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_ContratoCentroTrabajo_CentroTrabajo_CentroTrabajoId",
                table: "ContratoCentroTrabajo");

            migrationBuilder.DropForeignKey(
                name: "FK_ContratoCentroTrabajo_Contrato_ContratoId",
                table: "ContratoCentroTrabajo");

            migrationBuilder.DropIndex(
                name: "IX_Contrato_CentroTrabajoId",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "CentroTrabajoId",
                table: "Contrato");

            migrationBuilder.AddForeignKey(
                name: "FK_ContratoCentroTrabajo_CentroTrabajo_CentroTrabajoId",
                table: "ContratoCentroTrabajo",
                column: "CentroTrabajoId",
                principalTable: "CentroTrabajo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContratoCentroTrabajo_Contrato_ContratoId",
                table: "ContratoCentroTrabajo",
                column: "ContratoId",
                principalTable: "Contrato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContratoCentroTrabajo_CentroTrabajo_CentroTrabajoId",
                table: "ContratoCentroTrabajo");

            migrationBuilder.DropForeignKey(
                name: "FK_ContratoCentroTrabajo_Contrato_ContratoId",
                table: "ContratoCentroTrabajo");

            migrationBuilder.AddColumn<int>(
                name: "CentroTrabajoId",
                table: "Contrato",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_CentroTrabajoId",
                table: "Contrato",
                column: "CentroTrabajoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_CentroTrabajo_CentroTrabajoId",
                table: "Contrato",
                column: "CentroTrabajoId",
                principalTable: "CentroTrabajo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContratoCentroTrabajo_CentroTrabajo_CentroTrabajoId",
                table: "ContratoCentroTrabajo",
                column: "CentroTrabajoId",
                principalTable: "CentroTrabajo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContratoCentroTrabajo_Contrato_ContratoId",
                table: "ContratoCentroTrabajo",
                column: "ContratoId",
                principalTable: "Contrato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
