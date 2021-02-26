using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14043 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NominaContabilidad",
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
                    NominaFuncionarioId = table.Column<int>(nullable: false),
                    ConceptoNominaId = table.Column<int>(nullable: false),
                    CentroCostoId = table.Column<int>(nullable: false),
                    CuentaContableId = table.Column<int>(nullable: false),
                    TipoComprobante = table.Column<string>(type: "varchar(255)", nullable: false),
                    Nit = table.Column<string>(type: "varchar(255)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Debito = table.Column<decimal>(type: "money", nullable: true),
                    Credito = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NominaContabilidad", x => x.Id);
                    table.CheckConstraint("CK_NominaContabilidad_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_NominaContabilidad_CentroCosto_CentroCostoId",
                        column: x => x.CentroCostoId,
                        principalTable: "CentroCosto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NominaContabilidad_ConceptoNomina_ConceptoNominaId",
                        column: x => x.ConceptoNominaId,
                        principalTable: "ConceptoNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NominaContabilidad_CuentaContable_CuentaContableId",
                        column: x => x.CuentaContableId,
                        principalTable: "CuentaContable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NominaContabilidad_NominaFuncionario_NominaFuncionarioId",
                        column: x => x.NominaFuncionarioId,
                        principalTable: "NominaFuncionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NominaContabilidad_CentroCostoId",
                table: "NominaContabilidad",
                column: "CentroCostoId");

            migrationBuilder.CreateIndex(
                name: "IX_NominaContabilidad_ConceptoNominaId",
                table: "NominaContabilidad",
                column: "ConceptoNominaId");

            migrationBuilder.CreateIndex(
                name: "IX_NominaContabilidad_CuentaContableId",
                table: "NominaContabilidad",
                column: "CuentaContableId");

            migrationBuilder.CreateIndex(
                name: "IX_NominaContabilidad_NominaFuncionarioId",
                table: "NominaContabilidad",
                column: "NominaFuncionarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NominaContabilidad");
        }
    }
}
