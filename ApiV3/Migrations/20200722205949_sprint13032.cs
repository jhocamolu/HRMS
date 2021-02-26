using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint13032 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InformacionBasicaHojaDeVida",
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
                    PrimerNombre = table.Column<string>(type: "varchar(255)", nullable: false),
                    SegundoNombre = table.Column<string>(type: "varchar(255)", nullable: true),
                    PrimerApellido = table.Column<string>(type: "varchar(255)", nullable: false),
                    SegundoApellido = table.Column<string>(type: "varchar(255)", nullable: true),
                    SexoId = table.Column<int>(nullable: false),
                    EstadoCivilId = table.Column<int>(nullable: false),
                    OcupacionId = table.Column<int>(nullable: false),
                    Pensionado = table.Column<bool>(nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "date", nullable: false),
                    DivisionPoliticaNivel2OrigenId = table.Column<int>(nullable: false),
                    TipoDocumentoId = table.Column<int>(nullable: false),
                    NumeroDocumento = table.Column<string>(type: "varchar(255)", nullable: false),
                    FechaExpedicionDocumento = table.Column<DateTime>(type: "date", nullable: false),
                    DivisionPoliticaNivel2ExpedicionDocumentoId = table.Column<int>(nullable: false),
                    Nit = table.Column<string>(type: "varchar(255)", nullable: false),
                    DigitoVerificacion = table.Column<int>(nullable: false),
                    DivisionPoliticaNivel2ResidenciaId = table.Column<int>(nullable: false),
                    Celular = table.Column<string>(type: "varchar(255)", nullable: false),
                    TelefonoFijo = table.Column<string>(type: "varchar(255)", nullable: true),
                    TipoViviendaId = table.Column<int>(nullable: false),
                    Direccion = table.Column<string>(type: "varchar(255)", nullable: true),
                    ClaseLibretaMilitarId = table.Column<int>(nullable: true),
                    NumeroLibreta = table.Column<string>(type: "varchar(255)", nullable: true),
                    Distrito = table.Column<int>(nullable: true),
                    LicenciaConduccionAId = table.Column<int>(nullable: true),
                    LicenciaConduccionAFechaVencimiento = table.Column<DateTime>(type: "date", nullable: true),
                    LicenciaConduccionBId = table.Column<int>(nullable: true),
                    LicenciaConduccionBFechaVencimiento = table.Column<DateTime>(type: "date", nullable: true),
                    LicenciaConduccionCId = table.Column<int>(nullable: true),
                    LicenciaConduccionCFechaVencimiento = table.Column<DateTime>(type: "date", nullable: true),
                    TallaCamisa = table.Column<string>(type: "varchar(255)", nullable: true),
                    TallaPantalon = table.Column<string>(type: "varchar(255)", nullable: true),
                    NumeroCalzado = table.Column<double>(nullable: true),
                    UsaLentes = table.Column<bool>(nullable: false),
                    TipoSangreId = table.Column<int>(nullable: false),
                    CorreoElectronicoPersonal = table.Column<string>(type: "varchar(255)", nullable: true),
                    Adjunto = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformacionBasicaHojaDeVida", x => x.Id);
                    table.CheckConstraint("CK_InformacionBasicaHojaDeVida_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_InformacionBasicaHojaDeVida_ClaseLibretaMilitar_ClaseLibretaMilitarId",
                        column: x => x.ClaseLibretaMilitarId,
                        principalTable: "ClaseLibretaMilitar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InformacionBasicaHojaDeVida_DivisionPoliticaNivel2_DivisionPoliticaNivel2ExpedicionDocumentoId",
                        column: x => x.DivisionPoliticaNivel2ExpedicionDocumentoId,
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InformacionBasicaHojaDeVida_DivisionPoliticaNivel2_DivisionPoliticaNivel2OrigenId",
                        column: x => x.DivisionPoliticaNivel2OrigenId,
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InformacionBasicaHojaDeVida_DivisionPoliticaNivel2_DivisionPoliticaNivel2ResidenciaId",
                        column: x => x.DivisionPoliticaNivel2ResidenciaId,
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InformacionBasicaHojaDeVida_EstadoCivil_EstadoCivilId",
                        column: x => x.EstadoCivilId,
                        principalTable: "EstadoCivil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacionBasicaHojaDeVida_LicenciaConduccion_LicenciaConduccionAId",
                        column: x => x.LicenciaConduccionAId,
                        principalTable: "LicenciaConduccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InformacionBasicaHojaDeVida_LicenciaConduccion_LicenciaConduccionBId",
                        column: x => x.LicenciaConduccionBId,
                        principalTable: "LicenciaConduccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InformacionBasicaHojaDeVida_LicenciaConduccion_LicenciaConduccionCId",
                        column: x => x.LicenciaConduccionCId,
                        principalTable: "LicenciaConduccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InformacionBasicaHojaDeVida_Ocupacion_OcupacionId",
                        column: x => x.OcupacionId,
                        principalTable: "Ocupacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacionBasicaHojaDeVida_Sexo_SexoId",
                        column: x => x.SexoId,
                        principalTable: "Sexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacionBasicaHojaDeVida_TipoDocumento_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TipoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacionBasicaHojaDeVida_TipoSangre_TipoSangreId",
                        column: x => x.TipoSangreId,
                        principalTable: "TipoSangre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacionBasicaHojaDeVida_TipoVivienda_TipoViviendaId",
                        column: x => x.TipoViviendaId,
                        principalTable: "TipoVivienda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasicaHojaDeVida_ClaseLibretaMilitarId",
                table: "InformacionBasicaHojaDeVida",
                column: "ClaseLibretaMilitarId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasicaHojaDeVida_DivisionPoliticaNivel2ExpedicionDocumentoId",
                table: "InformacionBasicaHojaDeVida",
                column: "DivisionPoliticaNivel2ExpedicionDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasicaHojaDeVida_DivisionPoliticaNivel2OrigenId",
                table: "InformacionBasicaHojaDeVida",
                column: "DivisionPoliticaNivel2OrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasicaHojaDeVida_DivisionPoliticaNivel2ResidenciaId",
                table: "InformacionBasicaHojaDeVida",
                column: "DivisionPoliticaNivel2ResidenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasicaHojaDeVida_EstadoCivilId",
                table: "InformacionBasicaHojaDeVida",
                column: "EstadoCivilId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasicaHojaDeVida_LicenciaConduccionAId",
                table: "InformacionBasicaHojaDeVida",
                column: "LicenciaConduccionAId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasicaHojaDeVida_LicenciaConduccionBId",
                table: "InformacionBasicaHojaDeVida",
                column: "LicenciaConduccionBId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasicaHojaDeVida_LicenciaConduccionCId",
                table: "InformacionBasicaHojaDeVida",
                column: "LicenciaConduccionCId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasicaHojaDeVida_OcupacionId",
                table: "InformacionBasicaHojaDeVida",
                column: "OcupacionId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasicaHojaDeVida_SexoId",
                table: "InformacionBasicaHojaDeVida",
                column: "SexoId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasicaHojaDeVida_TipoDocumentoId",
                table: "InformacionBasicaHojaDeVida",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasicaHojaDeVida_TipoSangreId",
                table: "InformacionBasicaHojaDeVida",
                column: "TipoSangreId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasicaHojaDeVida_TipoViviendaId",
                table: "InformacionBasicaHojaDeVida",
                column: "TipoViviendaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InformacionBasicaHojaDeVida");
        }
    }
}
