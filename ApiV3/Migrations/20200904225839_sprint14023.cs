using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14023 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoReporta_Cargo_CargoJefeId",
                table: "CargoReporta");

            migrationBuilder.DropIndex(
                name: "IX_CargoReporta_CargoJefeId",
                table: "CargoReporta");

            migrationBuilder.DropColumn(
                name: "CargoJefeId",
                table: "CargoReporta");

            migrationBuilder.AddColumn<int>(
                name: "CargoDependenciaReportaId",
                table: "CargoReporta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CargoReporta_CargoDependenciaReportaId",
                table: "CargoReporta",
                column: "CargoDependenciaReportaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoReporta_CargoDependencia_CargoDependenciaReportaId",
                table: "CargoReporta",
                column: "CargoDependenciaReportaId",
                principalTable: "CargoDependencia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoReporta_CargoDependencia_CargoDependenciaReportaId",
                table: "CargoReporta");

            migrationBuilder.DropIndex(
                name: "IX_CargoReporta_CargoDependenciaReportaId",
                table: "CargoReporta");

            migrationBuilder.DropColumn(
                name: "CargoDependenciaReportaId",
                table: "CargoReporta");

            migrationBuilder.AddColumn<int>(
                name: "CargoJefeId",
                table: "CargoReporta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CargoReporta_CargoJefeId",
                table: "CargoReporta",
                column: "CargoJefeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoReporta_Cargo_CargoJefeId",
                table: "CargoReporta",
                column: "CargoJefeId",
                principalTable: "Cargo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
