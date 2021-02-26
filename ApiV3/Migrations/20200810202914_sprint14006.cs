using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint14006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidato",
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
                    RequisicionPersonalId = table.Column<int>(nullable: false),
                    HojaDeVidaId = table.Column<int>(nullable: false),
                    Estado = table.Column<string>(nullable: false),
                    Justificacion = table.Column<string>(type: "text", nullable: true),
                    AdjuntoPruebas = table.Column<string>(nullable: true),
                    AdjuntoExamen = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidato", x => x.Id);
                    table.CheckConstraint("CK_Candidato_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_Candidato_Estado", "([Estado]='Postulado' OR [Estado]='Descartado' OR [Estado]='Competente' OR [Estado]='Elegible' OR [Estado]='NoApto' OR [Estado]='Seleccionado')");
                    table.ForeignKey(
                        name: "FK_Candidato_HojaDeVida_HojaDeVidaId",
                        column: x => x.HojaDeVidaId,
                        principalTable: "HojaDeVida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidato_RequisicionPersonal_RequisicionPersonalId",
                        column: x => x.RequisicionPersonalId,
                        principalTable: "RequisicionPersonal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidato_HojaDeVidaId",
                table: "Candidato",
                column: "HojaDeVidaId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidato_RequisicionPersonalId",
                table: "Candidato",
                column: "RequisicionPersonalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidato");
        }
    }
}
