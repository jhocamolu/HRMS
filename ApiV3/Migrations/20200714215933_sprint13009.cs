using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint13009 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MotivoVacante",
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
                    Codigo = table.Column<string>(type: "varchar(255)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false),
                    RequiereNombreAQuienReemplaza = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivoVacante", x => x.Id);
                    table.CheckConstraint("CK_MotivoVacante_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "RequisicionPersonal",
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
                    CargoDependenciaSolicitanteId = table.Column<int>(nullable: false),
                    CentroOperativoSolicitanteId = table.Column<int>(nullable: false),
                    FuncionarioSolicitanteId = table.Column<int>(nullable: false),
                    CargoDependenciaSolicitadoId = table.Column<int>(nullable: false),
                    CentroOperativoSolicitadoID = table.Column<int>(nullable: false),
                    DivisionPoliticaNivel2Id = table.Column<int>(nullable: false),
                    Cantidad = table.Column<byte>(type: "tinyint", nullable: false),
                    TipoContratoID = table.Column<int>(nullable: false),
                    CentroCostoId = table.Column<int>(nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: false),
                    MotivoVacanteId = table.Column<int>(nullable: false),
                    FuncionarioAQuienReemplazaId = table.Column<int>(nullable: false),
                    PerfilCargo = table.Column<string>(type: "text", nullable: false),
                    CompetenciaCargo = table.Column<string>(type: "text", nullable: false),
                    TipoReclutamiento = table.Column<string>(nullable: false),
                    Salario = table.Column<decimal>(type: "money", nullable: false),
                    SalarioPortalReclutamiento = table.Column<bool>(nullable: false),
                    PerfilPortalReclutamiento = table.Column<bool>(nullable: false),
                    CompetenciaPortalReclutamiento = table.Column<bool>(nullable: false),
                    Observacion = table.Column<string>(type: "text", nullable: true),
                    Justificacion = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<string>(type: "char(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequisicionPersonal", x => x.Id);
                    table.CheckConstraint("CK_RequisicionPersonal_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_RequisicionPersonal_Estado", "([estado]='Anulada' OR [estado]='Aprobada' OR [estado]='Autorizacion' OR [estado]='Cancelada' OR [estado]='Cubierta' OR [estado]='Rechazada' OR [estado]='Revisada' OR [estado]='Solicitada')");
                    table.CheckConstraint("CK_RequisicionPersonal_TipoReclutamiento", "([TipoReclutamiento]='Externa' OR [TipoReclutamiento]='Interna' OR [TipoReclutamiento]='Mixta')");
                    table.ForeignKey(
                        name: "FK_RequisicionPersonal_CargoDependencia_CargoDependenciaSolicitadoId",
                        column: x => x.CargoDependenciaSolicitadoId,
                        principalTable: "CargoDependencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequisicionPersonal_CargoDependencia_CargoDependenciaSolicitanteId",
                        column: x => x.CargoDependenciaSolicitanteId,
                        principalTable: "CargoDependencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequisicionPersonal_CentroCosto_CentroCostoId",
                        column: x => x.CentroCostoId,
                        principalTable: "CentroCosto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequisicionPersonal_CentroOperativo_CentroOperativoSolicitadoID",
                        column: x => x.CentroOperativoSolicitadoID,
                        principalTable: "CentroOperativo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequisicionPersonal_CentroOperativo_CentroOperativoSolicitanteId",
                        column: x => x.CentroOperativoSolicitanteId,
                        principalTable: "CentroOperativo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequisicionPersonal_DivisionPoliticaNivel2_DivisionPoliticaNivel2Id",
                        column: x => x.DivisionPoliticaNivel2Id,
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequisicionPersonal_Funcionario_FuncionarioAQuienReemplazaId",
                        column: x => x.FuncionarioAQuienReemplazaId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequisicionPersonal_Funcionario_FuncionarioSolicitanteId",
                        column: x => x.FuncionarioSolicitanteId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequisicionPersonal_MotivoVacante_MotivoVacanteId",
                        column: x => x.MotivoVacanteId,
                        principalTable: "MotivoVacante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequisicionPersonal_TipoContrato_TipoContratoID",
                        column: x => x.TipoContratoID,
                        principalTable: "TipoContrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequisicionPersonal_CargoDependenciaSolicitadoId",
                table: "RequisicionPersonal",
                column: "CargoDependenciaSolicitadoId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicionPersonal_CargoDependenciaSolicitanteId",
                table: "RequisicionPersonal",
                column: "CargoDependenciaSolicitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicionPersonal_CentroCostoId",
                table: "RequisicionPersonal",
                column: "CentroCostoId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicionPersonal_CentroOperativoSolicitadoID",
                table: "RequisicionPersonal",
                column: "CentroOperativoSolicitadoID");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicionPersonal_CentroOperativoSolicitanteId",
                table: "RequisicionPersonal",
                column: "CentroOperativoSolicitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicionPersonal_DivisionPoliticaNivel2Id",
                table: "RequisicionPersonal",
                column: "DivisionPoliticaNivel2Id");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicionPersonal_FuncionarioAQuienReemplazaId",
                table: "RequisicionPersonal",
                column: "FuncionarioAQuienReemplazaId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicionPersonal_FuncionarioSolicitanteId",
                table: "RequisicionPersonal",
                column: "FuncionarioSolicitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicionPersonal_MotivoVacanteId",
                table: "RequisicionPersonal",
                column: "MotivoVacanteId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicionPersonal_TipoContratoID",
                table: "RequisicionPersonal",
                column: "TipoContratoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequisicionPersonal");

            migrationBuilder.DropTable(
                name: "MotivoVacante");
        }
    }
}
