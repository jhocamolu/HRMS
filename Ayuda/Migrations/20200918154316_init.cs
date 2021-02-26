using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ayuda.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EstadoRegistro = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    FechaEliminacion = table.Column<DateTime>(nullable: true),
                    Nombre = table.Column<string>(maxLength: 255, nullable: false),
                    Orden = table.Column<int>(nullable: false),
                    CategoriaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categoria_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clave",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Palabra = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clave", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articulo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EstadoRegistro = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    FechaEliminacion = table.Column<DateTime>(nullable: true),
                    CategoriaId = table.Column<int>(nullable: false),
                    Orden = table.Column<int>(nullable: false),
                    Titulo = table.Column<string>(maxLength: 255, nullable: false),
                    Descripcion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articulo_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticuloClave",
                columns: table => new
                {
                    ArticuloId = table.Column<int>(nullable: false),
                    ClaveId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticuloClave", x => new { x.ClaveId, x.ArticuloId });
                    table.ForeignKey(
                        name: "FK_ArticuloClave_Articulo_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticuloClave_Clave_ClaveId",
                        column: x => x.ClaveId,
                        principalTable: "Clave",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articulo_CategoriaId",
                table: "Articulo",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticuloClave_ArticuloId",
                table: "ArticuloClave",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_CategoriaId",
                table: "Categoria",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticuloClave");

            migrationBuilder.DropTable(
                name: "Articulo");

            migrationBuilder.DropTable(
                name: "Clave");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
