using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Plantillas.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Etiqueta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EstadoRegistro = table.Column<string>(type: "varchar(10)", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    EliminadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(nullable: true),
                    Nombre = table.Column<string>(maxLength: 255, nullable: false),
                    Slug = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etiqueta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrupoDocumento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EstadoRegistro = table.Column<string>(type: "varchar(10)", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    EliminadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(nullable: true),
                    Nombre = table.Column<string>(maxLength: 255, nullable: false),
                    Slug = table.Column<string>(maxLength: 255, nullable: false),
                    Servicio = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoDocumento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServicioFijo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EstadoRegistro = table.Column<string>(type: "varchar(10)", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    EliminadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(nullable: true),
                    Nombre = table.Column<string>(maxLength: 255, nullable: false),
                    Servicio = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicioFijo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComplementoPlantilla",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EstadoRegistro = table.Column<string>(type: "varchar(10)", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    EliminadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(nullable: true),
                    Nombre = table.Column<string>(maxLength: 255, nullable: false),
                    Alto = table.Column<string>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    GrupoDocumentoId = table.Column<int>(nullable: false),
                    CuerpoDocumento = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplementoPlantilla", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplementoPlantilla_GrupoDocumento_GrupoDocumentoId",
                        column: x => x.GrupoDocumentoId,
                        principalTable: "GrupoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EstadoRegistro = table.Column<string>(type: "varchar(10)", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    EliminadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(nullable: true),
                    Nombre = table.Column<string>(maxLength: 255, nullable: false),
                    Slug = table.Column<string>(nullable: true),
                    GrupoDocumentoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documento_GrupoDocumento_GrupoDocumentoId",
                        column: x => x.GrupoDocumentoId,
                        principalTable: "GrupoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrupoDocumentoEtiqueta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EstadoRegistro = table.Column<string>(type: "varchar(10)", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    EliminadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(nullable: true),
                    GrupoDocumentoId = table.Column<int>(nullable: false),
                    EtiquetaId = table.Column<int>(nullable: false),
                    ServicioFijoId = table.Column<int>(nullable: true),
                    Campo = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoDocumentoEtiqueta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrupoDocumentoEtiqueta_Etiqueta_EtiquetaId",
                        column: x => x.EtiquetaId,
                        principalTable: "Etiqueta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrupoDocumentoEtiqueta_GrupoDocumento_GrupoDocumentoId",
                        column: x => x.GrupoDocumentoId,
                        principalTable: "GrupoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrupoDocumentoEtiqueta_ServicioFijo_ServicioFijoId",
                        column: x => x.ServicioFijoId,
                        principalTable: "ServicioFijo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plantilla",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EstadoRegistro = table.Column<string>(type: "varchar(10)", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    EliminadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(nullable: true),
                    Nombre = table.Column<string>(maxLength: 255, nullable: false),
                    Version = table.Column<string>(maxLength: 255, nullable: true),
                    FechaVigencia = table.Column<DateTime>(type: "date", nullable: false),
                    GrupoDocumentoId = table.Column<int>(nullable: false),
                    EncabezadoId = table.Column<int>(nullable: true),
                    PiePaginaId = table.Column<int>(nullable: true),
                    DocumentoId = table.Column<int>(nullable: false),
                    CuerpoDocumento = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plantilla", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plantilla_Documento_DocumentoId",
                        column: x => x.DocumentoId,
                        principalTable: "Documento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plantilla_ComplementoPlantilla_EncabezadoId",
                        column: x => x.EncabezadoId,
                        principalTable: "ComplementoPlantilla",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plantilla_GrupoDocumento_GrupoDocumentoId",
                        column: x => x.GrupoDocumentoId,
                        principalTable: "GrupoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plantilla_ComplementoPlantilla_PiePaginaId",
                        column: x => x.PiePaginaId,
                        principalTable: "ComplementoPlantilla",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplementoPlantilla_GrupoDocumentoId",
                table: "ComplementoPlantilla",
                column: "GrupoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_GrupoDocumentoId",
                table: "Documento",
                column: "GrupoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoDocumentoEtiqueta_GrupoDocumentoId",
                table: "GrupoDocumentoEtiqueta",
                column: "GrupoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoDocumentoEtiqueta_ServicioFijoId",
                table: "GrupoDocumentoEtiqueta",
                column: "ServicioFijoId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoDocumentoEtiqueta_EtiquetaId_GrupoDocumentoId",
                table: "GrupoDocumentoEtiqueta",
                columns: new[] { "EtiquetaId", "GrupoDocumentoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plantilla_DocumentoId",
                table: "Plantilla",
                column: "DocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Plantilla_EncabezadoId",
                table: "Plantilla",
                column: "EncabezadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Plantilla_GrupoDocumentoId",
                table: "Plantilla",
                column: "GrupoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Plantilla_PiePaginaId",
                table: "Plantilla",
                column: "PiePaginaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrupoDocumentoEtiqueta");

            migrationBuilder.DropTable(
                name: "Plantilla");

            migrationBuilder.DropTable(
                name: "Etiqueta");

            migrationBuilder.DropTable(
                name: "ServicioFijo");

            migrationBuilder.DropTable(
                name: "Documento");

            migrationBuilder.DropTable(
                name: "ComplementoPlantilla");

            migrationBuilder.DropTable(
                name: "GrupoDocumento");
        }
    }
}
