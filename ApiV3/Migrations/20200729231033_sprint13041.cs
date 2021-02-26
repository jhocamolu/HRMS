using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint13041 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateCheckConstraint(
                name: "CK_HojaDeVidaExperienciaLaboral_EstadoRegistro",
                table: "HojaDeVidaExperienciaLaboral",
                sql: "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            migrationBuilder.CreateTable(
                name: "HojaDeVidaDocumento",
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
                    TipoSoporteId = table.Column<int>(nullable: false),
                    Comentario = table.Column<string>(type: "text", nullable: false),
                    Adjunto = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HojaDeVidaDocumento", x => x.Id);
                    table.CheckConstraint("CK_HojaDeVidaDocumento_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_HojaDeVidaDocumento_HojaDeVida_HojaDeVidaId",
                        column: x => x.HojaDeVidaId,
                        principalTable: "HojaDeVida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HojaDeVidaDocumento_TipoSoporte_TipoSoporteId",
                        column: x => x.TipoSoporteId,
                        principalTable: "TipoSoporte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HojaDeVidaDocumento_HojaDeVidaId",
                table: "HojaDeVidaDocumento",
                column: "HojaDeVidaId");

            migrationBuilder.CreateIndex(
                name: "IX_HojaDeVidaDocumento_TipoSoporteId",
                table: "HojaDeVidaDocumento",
                column: "TipoSoporteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HojaDeVidaDocumento");

            migrationBuilder.DropCheckConstraint(
                name: "CK_HojaDeVidaExperienciaLaboral_EstadoRegistro",
                table: "HojaDeVidaExperienciaLaboral");
        }
    }
}
