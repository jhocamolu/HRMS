using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint13008 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConceptoNominaElementoFormula",
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
                    ConceptoNominaId = table.Column<int>(nullable: false),
                    ElementoFormulaId = table.Column<int>(nullable: false),
                    Tipo = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptoNominaElementoFormula", x => x.Id);
                    table.CheckConstraint("CK_ConceptoNominaElementoFormula_Tipo", "([Tipo]='Concepto' OR [Tipo]='Funcion')");
                    table.ForeignKey(
                        name: "FK_ConceptoNominaElementoFormula_ConceptoNomina_ConceptoNominaId",
                        column: x => x.ConceptoNominaId,
                        principalTable: "ConceptoNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConceptoNominaElementoFormula_ConceptoNominaId",
                table: "ConceptoNominaElementoFormula",
                column: "ConceptoNominaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConceptoNominaElementoFormula");
        }
    }
}
