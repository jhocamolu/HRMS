using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14040 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFinalReal",
                table: "AusentismoFuncionario",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaIniciaReal",
                table: "AusentismoFuncionario",
                type: "date",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConceptoNominaTipoAdministradora",
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
                    TipoAdministradoraId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptoNominaTipoAdministradora", x => x.Id);
                    table.CheckConstraint("CK_ConceptoNominaTipoAdministradora_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_ConceptoNominaTipoAdministradora_ConceptoNomina_ConceptoNominaId",
                        column: x => x.ConceptoNominaId,
                        principalTable: "ConceptoNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConceptoNominaTipoAdministradora_TipoAdministradora_TipoAdministradoraId",
                        column: x => x.TipoAdministradoraId,
                        principalTable: "TipoAdministradora",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConceptoNominaTipoAdministradora_ConceptoNominaId",
                table: "ConceptoNominaTipoAdministradora",
                column: "ConceptoNominaId");

            migrationBuilder.CreateIndex(
                name: "IX_ConceptoNominaTipoAdministradora_TipoAdministradoraId",
                table: "ConceptoNominaTipoAdministradora",
                column: "TipoAdministradoraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConceptoNominaTipoAdministradora");

            migrationBuilder.DropColumn(
                name: "FechaFinalReal",
                table: "AusentismoFuncionario");

            migrationBuilder.DropColumn(
                name: "FechaIniciaReal",
                table: "AusentismoFuncionario");
        }
    }
}
