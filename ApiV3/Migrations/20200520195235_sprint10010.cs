using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint10010 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AplicacionExterna",
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
                    Descripcion = table.Column<string>(type: "varchar(255)", nullable: true),
                    Aprueba = table.Column<string>(type: "varchar(255)", nullable: false),
                    Autoriza = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AplicacionExterna", x => x.Id);
                    table.CheckConstraint("CK_AplicacionExterna_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_TipoAplicacionExterna_Aprueba", "([Aprueba]='JefeInmediato' OR [Aprueba]='Otro' OR [Aprueba]='NoAplica')");
                    table.CheckConstraint("CK_TipoAplicacionExterna_Autoriza", "([Autoriza]='JefeInmediato' OR [Autoriza]='Otro' OR [Autoriza]='NoAplica')");
                });

            migrationBuilder.CreateTable(
                name: "AplicacionExternaCargo",
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
                    AplicacionExternaId = table.Column<int>(nullable: false),
                    Tipo = table.Column<string>(type: "varchar(255)", nullable: false),
                    CargoDependienteId = table.Column<int>(nullable: false),
                    CentroOperativoDependienteId = table.Column<int>(nullable: false),
                    CargoIndependienteId = table.Column<int>(nullable: false),
                    CentroOperativoIndependienteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AplicacionExternaCargo", x => x.Id);
                    table.CheckConstraint("CK_AplicacionExternaCargo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_AplicacionExternaCargo_Tipo", "([Tipo]='Aprobacion' OR [Tipo]='Autorizacion')");
                    table.ForeignKey(
                        name: "FK_AplicacionExternaCargo_AplicacionExterna_AplicacionExternaId",
                        column: x => x.AplicacionExternaId,
                        principalTable: "AplicacionExterna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AplicacionExternaCargo_Cargo_CargoDependienteId",
                        column: x => x.CargoDependienteId,
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AplicacionExternaCargo_Cargo_CargoIndependienteId",
                        column: x => x.CargoIndependienteId,
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AplicacionExternaCargo_CentroOperativo_CentroOperativoDependienteId",
                        column: x => x.CentroOperativoDependienteId,
                        principalTable: "CentroOperativo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AplicacionExternaCargo_CentroOperativo_CentroOperativoIndependienteId",
                        column: x => x.CentroOperativoIndependienteId,
                        principalTable: "CentroOperativo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AplicacionExternaCargo_AplicacionExternaId",
                table: "AplicacionExternaCargo",
                column: "AplicacionExternaId");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacionExternaCargo_CargoDependienteId",
                table: "AplicacionExternaCargo",
                column: "CargoDependienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacionExternaCargo_CargoIndependienteId",
                table: "AplicacionExternaCargo",
                column: "CargoIndependienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacionExternaCargo_CentroOperativoDependienteId",
                table: "AplicacionExternaCargo",
                column: "CentroOperativoDependienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacionExternaCargo_CentroOperativoIndependienteId",
                table: "AplicacionExternaCargo",
                column: "CentroOperativoIndependienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AplicacionExternaCargo");

            migrationBuilder.DropTable(
                name: "AplicacionExterna");
        }
    }
}
