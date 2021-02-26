using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint12009 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaNovedad",
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
                    Modulo = table.Column<string>(type: "varchar(255)", nullable: false),
                    Clase = table.Column<string>(type: "varchar(255)", nullable: false),
                    UsaParametrizacion = table.Column<bool>(type: "bit", nullable: false),
                    RequiereTercero = table.Column<bool>(type: "bit", nullable: false),
                    UbicacionTercero = table.Column<string>(type: "varchar(255)", nullable: true),
                    ValorEditable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaNovedad", x => x.Id);
                    table.CheckConstraint("CK_CategoriaNovedad_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_CategoriaNovedad_Modulo", "([Modulo]='Libranzas' OR [Modulo]='Embargos' OR [Modulo]='Ausentismos' OR [Modulo]='Beneficios' OR [Modulo]='HorasExtra'  OR [Modulo]='GastosViaje' OR [Modulo]='OtrasNovedades' )");
                    table.CheckConstraint("CK_CategoriaNovedad_Clase", "([Clase]='Fija' OR [Clase]='Eventual' )");
                    table.ForeignKey(
                        name: "FK_CategoriaNovedad_ConceptoNomina_ConceptoNominaId",
                        column: x => x.ConceptoNominaId,
                        principalTable: "ConceptoNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaNovedad_ConceptoNominaId",
                table: "CategoriaNovedad",
                column: "ConceptoNominaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriaNovedad");
        }
    }
}
