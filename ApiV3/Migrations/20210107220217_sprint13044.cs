using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint13044 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NominaCentroCosto",
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
                    NitTercero = table.Column<string>(type: "varchar(255)", nullable: false),
                    Valor = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NominaCentroCosto", x => x.Id);
                    table.CheckConstraint("CK_NominaCentroCosto_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_NominaCentroCosto_CentroCosto_CentroCostoId",
                        column: x => x.CentroCostoId,
                        principalTable: "CentroCosto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NominaCentroCosto_ConceptoNomina_ConceptoNominaId",
                        column: x => x.ConceptoNominaId,
                        principalTable: "ConceptoNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NominaCentroCosto_NominaFuncionario_NominaFuncionarioId",
                        column: x => x.NominaFuncionarioId,
                        principalTable: "NominaFuncionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NominaCentroCosto_CentroCostoId",
                table: "NominaCentroCosto",
                column: "CentroCostoId");

            migrationBuilder.CreateIndex(
                name: "IX_NominaCentroCosto_ConceptoNominaId",
                table: "NominaCentroCosto",
                column: "ConceptoNominaId");

            migrationBuilder.CreateIndex(
                name: "IX_NominaCentroCosto_NominaFuncionarioId",
                table: "NominaCentroCosto",
                column: "NominaFuncionarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NominaCentroCosto");
        }
    }
}
