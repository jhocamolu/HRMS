using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint14018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TarifaAporteEmpresa",
                table: "TipoAdministradora",
                type: "decimal(19,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TarifaAporteFuncionario",
                table: "TipoAdministradora",
                type: "decimal(19,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TarifaAporteTotal",
                table: "TipoAdministradora",
                type: "decimal(19,3)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContratoCentroTrabajo",
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
                    ContratoId = table.Column<int>(nullable: false),
                    CentroTrabajoId = table.Column<int>(nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratoCentroTrabajo", x => x.Id);
                    table.CheckConstraint("CK_ContratoCentroTrabajo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_ContratoCentroTrabajo_CentroTrabajo_CentroTrabajoId",
                        column: x => x.CentroTrabajoId,
                        principalTable: "CentroTrabajo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContratoCentroTrabajo_Contrato_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContratoCentroTrabajo_CentroTrabajoId",
                table: "ContratoCentroTrabajo",
                column: "CentroTrabajoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoCentroTrabajo_ContratoId",
                table: "ContratoCentroTrabajo",
                column: "ContratoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContratoCentroTrabajo");

            migrationBuilder.DropColumn(
                name: "TarifaAporteEmpresa",
                table: "TipoAdministradora");

            migrationBuilder.DropColumn(
                name: "TarifaAporteFuncionario",
                table: "TipoAdministradora");

            migrationBuilder.DropColumn(
                name: "TarifaAporteTotal",
                table: "TipoAdministradora");
        }
    }
}
