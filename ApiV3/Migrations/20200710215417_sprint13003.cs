using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint13003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoReporta_Cargo_CargoFuncionarioId",
                table: "CargoReporta");

            migrationBuilder.DropIndex(
                name: "IX_CargoReporta_CargoFuncionarioId",
                table: "CargoReporta");

            migrationBuilder.DropColumn(
                name: "CargoFuncionarioId",
                table: "CargoReporta");

            migrationBuilder.AddColumn<int>(
                name: "CargoDependenciaId",
                table: "CargoReporta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CargoPresupuesto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstadoRegistro = table.Column<string>(type: "char(10)", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    EliminadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CargoId = table.Column<int>(nullable: false),
                    Anno = table.Column<int>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoPresupuesto", x => x.Id);
                    table.CheckConstraint("CK_HoraExtra_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_CargoPresupuesto_Cargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CargoReporta_CargoDependenciaId",
                table: "CargoReporta",
                column: "CargoDependenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoPresupuesto_CargoId",
                table: "CargoPresupuesto",
                column: "CargoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoReporta_CargoDependencia_CargoDependenciaId",
                table: "CargoReporta",
                column: "CargoDependenciaId",
                principalTable: "CargoDependencia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoReporta_CargoDependencia_CargoDependenciaId",
                table: "CargoReporta");

            migrationBuilder.DropTable(
                name: "CargoPresupuesto");

            migrationBuilder.DropIndex(
                name: "IX_CargoReporta_CargoDependenciaId",
                table: "CargoReporta");

            migrationBuilder.DropColumn(
                name: "CargoDependenciaId",
                table: "CargoReporta");

            migrationBuilder.AddColumn<int>(
                name: "CargoFuncionarioId",
                table: "CargoReporta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CargoReporta_CargoFuncionarioId",
                table: "CargoReporta",
                column: "CargoFuncionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoReporta_Cargo_CargoFuncionarioId",
                table: "CargoReporta",
                column: "CargoFuncionarioId",
                principalTable: "Cargo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
