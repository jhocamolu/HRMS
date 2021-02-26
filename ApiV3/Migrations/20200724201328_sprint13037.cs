using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint13037 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateCheckConstraint(
                name: "CK_HojaDeVidaEstudio_EstadoEstudio",
                table: "HojaDeVidaEstudio",
                sql: "([EstadoEstudio]='EnCurso' OR [EstadoEstudio]='Aplazado' OR [EstadoEstudio]='Abandonado' OR [EstadoEstudio]='Culminado')");

            migrationBuilder.CreateTable(
                name: "HojaDeVidaExperienciaLaboral",
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
                    HojaDeVidaId = table.Column<int>(nullable: false),
                    NombreCargo = table.Column<string>(type: "varchar(255)", nullable: false),
                    NombreEmpresa = table.Column<string>(type: "varchar(255)", nullable: false),
                    Telefono = table.Column<string>(type: "varchar(255)", nullable: false),
                    Salario = table.Column<string>(type: "varchar(255)", nullable: true),
                    NombreJefeInmediato = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true),
                    FuncionesCargo = table.Column<string>(type: "text", nullable: true),
                    TrabajaActualmente = table.Column<bool>(nullable: true),
                    MotivoRetiro = table.Column<string>(type: "text", nullable: true),
                    Observaciones = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HojaDeVidaExperienciaLaboral", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HojaDeVidaExperienciaLaboral_HojaDeVida_HojaDeVidaId",
                        column: x => x.HojaDeVidaId,
                        principalTable: "HojaDeVida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HojaDeVidaExperienciaLaboral_HojaDeVidaId",
                table: "HojaDeVidaExperienciaLaboral",
                column: "HojaDeVidaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HojaDeVidaExperienciaLaboral");

            migrationBuilder.DropCheckConstraint(
                name: "CK_HojaDeVidaEstudio_EstadoEstudio",
                table: "HojaDeVidaEstudio");
        }
    }
}
