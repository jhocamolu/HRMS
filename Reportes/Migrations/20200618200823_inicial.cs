using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reportes.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
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
                    Alias = table.Column<string>(type: "varchar(255)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parametro",
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
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false),
                    TipoDato = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subcategoria",
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
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false),
                    CategoriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcategoria_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reporte",
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
                    Alias = table.Column<string>(type: "varchar(255)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(500)", nullable: false),
                    SubcategoriaId = table.Column<int>(nullable: false),
                    Link = table.Column<string>(type: "varchar(max)", nullable: false),
                    VistaGeneracion = table.Column<string>(type: "varchar(255)", nullable: true),
                    Path = table.Column<string>(type: "varchar(255)", nullable: false),
                    Formato = table.Column<string>(type: "varchar(255)", nullable: false),
                    Extension = table.Column<string>(type: "varchar(255)", nullable: false),
                    Alto = table.Column<string>(type: "varchar(255)", nullable: true),
                    Ancho = table.Column<string>(type: "varchar(255)", nullable: true),
                    EsModal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reporte", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reporte_Subcategoria_SubcategoriaId",
                        column: x => x.SubcategoriaId,
                        principalTable: "Subcategoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReporteParametro",
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
                    ReporteId = table.Column<int>(nullable: false),
                    ParametroId = table.Column<int>(nullable: false),
                    EsRequerido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReporteParametro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReporteParametro_Parametro_ParametroId",
                        column: x => x.ParametroId,
                        principalTable: "Parametro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReporteParametro_Reporte_ReporteId",
                        column: x => x.ReporteId,
                        principalTable: "Reporte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reporte_Alias",
                table: "Reporte",
                column: "Alias",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reporte_SubcategoriaId",
                table: "Reporte",
                column: "SubcategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ReporteParametro_ParametroId",
                table: "ReporteParametro",
                column: "ParametroId");

            migrationBuilder.CreateIndex(
                name: "IX_ReporteParametro_ReporteId",
                table: "ReporteParametro",
                column: "ReporteId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategoria_CategoriaId",
                table: "Subcategoria",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReporteParametro");

            migrationBuilder.DropTable(
                name: "Parametro");

            migrationBuilder.DropTable(
                name: "Reporte");

            migrationBuilder.DropTable(
                name: "Subcategoria");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
