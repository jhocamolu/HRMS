using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reclutamiento.Migrations
{
    public partial class reclutamientosprint14001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_DivisionPoliticaNivel2_DivisionPoliticaNivel2ExpedicionDocumentoId",
                schema: "dbo",
                table: "HojaDeVida");

            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_DivisionPoliticaNivel2_DivisionPoliticaNivel2OrigenId",
                schema: "dbo",
                table: "HojaDeVida");

            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_DivisionPoliticaNivel2_DivisionPoliticaNivel2ResidenciaId",
                schema: "dbo",
                table: "HojaDeVida");

            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_EstadoCivil_EstadoCivilId",
                schema: "dbo",
                table: "HojaDeVida");

            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_Ocupacion_OcupacionId",
                schema: "dbo",
                table: "HojaDeVida");

            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_TipoSangre_TipoSangreId",
                schema: "dbo",
                table: "HojaDeVida");

            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_TipoVivienda_TipoViviendaId",
                schema: "dbo",
                table: "HojaDeVida");
                */
            migrationBuilder.AddColumn<string>(
                name: "TokenGhestic",
                schema: "reclutamiento",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");
            /*
            migrationBuilder.AlterColumn<string>(
                name: "CodigoPila",
                schema: "dbo",
                table: "TipoDocumento",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.AlterColumn<string>(
                name: "CodigoDian",
                schema: "dbo",
                table: "TipoDocumento",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.AddColumn<string>(
                name: "EquivalenteBancario",
                schema: "dbo",
                table: "TipoDocumento",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "UsaLentes",
                schema: "dbo",
                table: "HojaDeVida",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "TipoViviendaId",
                schema: "dbo",
                table: "HojaDeVida",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TipoSangreId",
                schema: "dbo",
                table: "HojaDeVida",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "Pensionado",
                schema: "dbo",
                table: "HojaDeVida",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "OcupacionId",
                schema: "dbo",
                table: "HojaDeVida",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Nit",
                schema: "dbo",
                table: "HojaDeVida",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaNacimiento",
                schema: "dbo",
                table: "HojaDeVida",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaExpedicionDocumento",
                schema: "dbo",
                table: "HojaDeVida",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "EstadoCivilId",
                schema: "dbo",
                table: "HojaDeVida",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DivisionPoliticaNivel2ResidenciaId",
                schema: "dbo",
                table: "HojaDeVida",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DivisionPoliticaNivel2OrigenId",
                schema: "dbo",
                table: "HojaDeVida",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DivisionPoliticaNivel2ExpedicionDocumentoId",
                schema: "dbo",
                table: "HojaDeVida",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DigitoVerificacion",
                schema: "dbo",
                table: "HojaDeVida",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Notificacion",
                schema: "dbo",
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
                    Tipo = table.Column<string>(type: "varchar(255)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(255)", nullable: false),
                    Mensaje = table.Column<string>(type: "text", nullable: false),
                    EnEjecucion = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificacionPlantilla",
                schema: "dbo",
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
                    Descripcion = table.Column<string>(type: "Text", nullable: false),
                    Plantilla = table.Column<string>(type: "Text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificacionPlantilla", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TareaProgramada",
                schema: "dbo",
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
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: true),
                    Alias = table.Column<string>(type: "varchar(255)", nullable: true),
                    Periodicidad = table.Column<string>(type: "varchar(255)", nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Instruccion = table.Column<string>(type: "varchar(255)", nullable: true),
                    EnEjecucion = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareaProgramada", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActividadEconomica",
                schema: "reclutamiento",
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
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActividadEconomica", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CentroCosto",
                schema: "reclutamiento",
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
                    Codigo = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentroCosto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CentroOperativo",
                schema: "reclutamiento",
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
                    Codigo = table.Column<string>(type: "char(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentroOperativo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClaseAportante",
                schema: "reclutamiento",
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
                    Codigo = table.Column<string>(type: "char(1)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaseAportante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dependencia",
                schema: "reclutamiento",
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
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Funcionario",
                schema: "reclutamiento",
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
                    Estado = table.Column<string>(type: "char(30)", nullable: false),
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
                    CorreoElectronicoCorporativo = table.Column<string>(type: "varchar(255)", nullable: true),
                    Adjunto = table.Column<string>(type: "varchar(255)", nullable: true),
                    CriterioBusqueda = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionario_ClaseLibretaMilitar_ClaseLibretaMilitarId",
                        column: x => x.ClaseLibretaMilitarId,
                        principalSchema: "reclutamiento",
                        principalTable: "ClaseLibretaMilitar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_DivisionPoliticaNivel2_DivisionPoliticaNivel2ExpedicionDocumentoId",
                        column: x => x.DivisionPoliticaNivel2ExpedicionDocumentoId,
                        principalSchema: "reclutamiento",
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionario_DivisionPoliticaNivel2_DivisionPoliticaNivel2OrigenId",
                        column: x => x.DivisionPoliticaNivel2OrigenId,
                        principalSchema: "reclutamiento",
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionario_DivisionPoliticaNivel2_DivisionPoliticaNivel2ResidenciaId",
                        column: x => x.DivisionPoliticaNivel2ResidenciaId,
                        principalSchema: "reclutamiento",
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionario_EstadoCivil_EstadoCivilId",
                        column: x => x.EstadoCivilId,
                        principalSchema: "reclutamiento",
                        principalTable: "EstadoCivil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionario_LicenciaConduccion_LicenciaConduccionAId",
                        column: x => x.LicenciaConduccionAId,
                        principalSchema: "reclutamiento",
                        principalTable: "LicenciaConduccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_LicenciaConduccion_LicenciaConduccionBId",
                        column: x => x.LicenciaConduccionBId,
                        principalSchema: "reclutamiento",
                        principalTable: "LicenciaConduccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_LicenciaConduccion_LicenciaConduccionCId",
                        column: x => x.LicenciaConduccionCId,
                        principalSchema: "reclutamiento",
                        principalTable: "LicenciaConduccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_Ocupacion_OcupacionId",
                        column: x => x.OcupacionId,
                        principalSchema: "reclutamiento",
                        principalTable: "Ocupacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionario_Sexo_SexoId",
                        column: x => x.SexoId,
                        principalSchema: "dbo",
                        principalTable: "Sexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionario_TipoDocumento_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalSchema: "dbo",
                        principalTable: "TipoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionario_TipoSangre_TipoSangreId",
                        column: x => x.TipoSangreId,
                        principalSchema: "reclutamiento",
                        principalTable: "TipoSangre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionario_TipoVivienda_TipoViviendaId",
                        column: x => x.TipoViviendaId,
                        principalSchema: "reclutamiento",
                        principalTable: "TipoVivienda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotivoVacante",
                schema: "reclutamiento",
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
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false),
                    RequiereNombreAQuienReemplaza = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivoVacante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NaturalezaJuridica",
                schema: "reclutamiento",
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
                    Codigo = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaturalezaJuridica", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NivelCargo",
                schema: "reclutamiento",
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
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NivelCargo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperadorPago",
                schema: "reclutamiento",
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
                    PaginaWeb = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperadorPago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoAdministradora",
                schema: "reclutamiento",
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
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false),
                    TarifaAporteFuncionario = table.Column<decimal>(type: "decimal(19,3)", nullable: false),
                    TarifaAporteEmpresa = table.Column<decimal>(type: "decimal(19,3)", nullable: false),
                    TarifaAporteTotal = table.Column<decimal>(type: "decimal(19,3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAdministradora", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoAportante",
                schema: "reclutamiento",
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
                    Codigo = table.Column<string>(type: "char(2)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAportante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoContrato",
                schema: "reclutamiento",
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
                    CantidadProrrogas = table.Column<int>(nullable: false),
                    DuracionMaxima = table.Column<int>(nullable: false),
                    TerminoIndefinido = table.Column<bool>(nullable: false),
                    Clase = table.Column<string>(type: "varchar(255)", nullable: false),
                    DocumentoSlug = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoContrato", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoContribuyente",
                schema: "reclutamiento",
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
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false),
                    Persona = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoContribuyente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoPersona",
                schema: "reclutamiento",
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
                    Codigo = table.Column<string>(type: "char(1)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPersona", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificacionDestinatario",
                schema: "dbo",
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
                    NotificacionId = table.Column<int>(nullable: false),
                    FuncionarioId = table.Column<int>(nullable: true),
                    CorreoElectronico = table.Column<string>(type: "varchar(255)", nullable: true),
                    Estado = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificacionDestinatario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificacionDestinatario_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalSchema: "reclutamiento",
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificacionDestinatario_Notificacion_NotificacionId",
                        column: x => x.NotificacionId,
                        principalSchema: "dbo",
                        principalTable: "Notificacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cargo",
                schema: "reclutamiento",
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
                    Codigo = table.Column<string>(type: "varchar(10)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false),
                    ObjetivoCargo = table.Column<string>(type: "text", nullable: false),
                    CostoSicom = table.Column<bool>(type: "bit", nullable: false),
                    NivelCargoId = table.Column<int>(nullable: false),
                    Clase = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cargo_NivelCargo_NivelCargoId",
                        column: x => x.NivelCargoId,
                        principalSchema: "reclutamiento",
                        principalTable: "NivelCargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administradora",
                schema: "reclutamiento",
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
                    Nit = table.Column<string>(type: "varchar(255)", nullable: false),
                    Dv = table.Column<string>(type: "varchar(255)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false),
                    TipoAdministradoraId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administradora", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administradora_TipoAdministradora_TipoAdministradoraId",
                        column: x => x.TipoAdministradoraId,
                        principalSchema: "reclutamiento",
                        principalTable: "TipoAdministradora",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClaseAportanteTipoAportante",
                schema: "reclutamiento",
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
                    ClaseAportanteId = table.Column<int>(nullable: false),
                    TipoAportanteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaseAportanteTipoAportante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClaseAportanteTipoAportante_ClaseAportante_ClaseAportanteId",
                        column: x => x.ClaseAportanteId,
                        principalSchema: "reclutamiento",
                        principalTable: "ClaseAportante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClaseAportanteTipoAportante_TipoAportante_TipoAportanteId",
                        column: x => x.TipoAportanteId,
                        principalSchema: "reclutamiento",
                        principalTable: "TipoAportante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CargoDependencia",
                schema: "reclutamiento",
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
                    CargoId = table.Column<int>(nullable: false),
                    DependenciaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoDependencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargoDependencia_Cargo_CargoId",
                        column: x => x.CargoId,
                        principalSchema: "reclutamiento",
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CargoDependencia_Dependencia_DependenciaId",
                        column: x => x.DependenciaId,
                        principalSchema: "reclutamiento",
                        principalTable: "Dependencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InformacionBasica",
                schema: "dbo",
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
                    Nit = table.Column<string>(type: "varchar(255)", nullable: false),
                    DigitoVerificacion = table.Column<string>(type: "varchar(255)", nullable: false),
                    RazonSocial = table.Column<string>(type: "varchar(255)", nullable: false),
                    ActividadEconomicaId = table.Column<int>(nullable: false),
                    DivisionPoliticaNivel2Id = table.Column<int>(nullable: false),
                    Direccion = table.Column<string>(type: "varchar(255)", nullable: true),
                    Telefono = table.Column<string>(type: "varchar(255)", nullable: true),
                    Fax = table.Column<string>(type: "varchar(255)", nullable: true),
                    CorreoElectronico = table.Column<string>(type: "varchar(255)", nullable: true),
                    Web = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaConstitucion = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    TipoContribuyenteId = table.Column<int>(nullable: false),
                    OperadorPagoId = table.Column<int>(nullable: false),
                    ArlId = table.Column<int>(nullable: false),
                    TipoDocumentoId = table.Column<int>(nullable: false),
                    NaturalezaJuridicaId = table.Column<int>(nullable: false),
                    TipoPersonaId = table.Column<int>(nullable: false),
                    ClaseAportanteTipoAportanteId = table.Column<int>(nullable: false),
                    CargoId = table.Column<int>(nullable: false),
                    BeneficiarioLey1429De2010 = table.Column<bool>(type: "bit", nullable: false),
                    BeneficiarioImpuestoEquidad = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformacionBasica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InformacionBasica_ActividadEconomica_ActividadEconomicaId",
                        column: x => x.ActividadEconomicaId,
                        principalSchema: "reclutamiento",
                        principalTable: "ActividadEconomica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacionBasica_Administradora_ArlId",
                        column: x => x.ArlId,
                        principalSchema: "reclutamiento",
                        principalTable: "Administradora",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacionBasica_Cargo_CargoId",
                        column: x => x.CargoId,
                        principalSchema: "reclutamiento",
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacionBasica_ClaseAportanteTipoAportante_ClaseAportanteTipoAportanteId",
                        column: x => x.ClaseAportanteTipoAportanteId,
                        principalSchema: "reclutamiento",
                        principalTable: "ClaseAportanteTipoAportante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacionBasica_DivisionPoliticaNivel2_DivisionPoliticaNivel2Id",
                        column: x => x.DivisionPoliticaNivel2Id,
                        principalSchema: "reclutamiento",
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacionBasica_NaturalezaJuridica_NaturalezaJuridicaId",
                        column: x => x.NaturalezaJuridicaId,
                        principalSchema: "reclutamiento",
                        principalTable: "NaturalezaJuridica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacionBasica_OperadorPago_OperadorPagoId",
                        column: x => x.OperadorPagoId,
                        principalSchema: "reclutamiento",
                        principalTable: "OperadorPago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacionBasica_TipoContribuyente_TipoContribuyenteId",
                        column: x => x.TipoContribuyenteId,
                        principalSchema: "reclutamiento",
                        principalTable: "TipoContribuyente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacionBasica_TipoDocumento_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalSchema: "dbo",
                        principalTable: "TipoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacionBasica_TipoPersona_TipoPersonaId",
                        column: x => x.TipoPersonaId,
                        principalSchema: "reclutamiento",
                        principalTable: "TipoPersona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequisicionPersonal",
                schema: "dbo",
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
                    CargoDependenciaSolicitanteId = table.Column<int>(nullable: false),
                    CentroOperativoSolicitanteId = table.Column<int>(nullable: true),
                    FuncionarioSolicitanteId = table.Column<int>(nullable: false),
                    CargoDependenciaSolicitadoId = table.Column<int>(nullable: false),
                    CentroOperativoSolicitadoId = table.Column<int>(nullable: true),
                    DivisionPoliticaNivel2Id = table.Column<int>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    TipoContratoId = table.Column<int>(nullable: false),
                    CentroCostoId = table.Column<int>(nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true),
                    MotivoVacanteId = table.Column<int>(nullable: false),
                    FuncionarioAQuienReemplazaId = table.Column<int>(nullable: true),
                    PerfilCargo = table.Column<string>(type: "text", nullable: false),
                    CompetenciaCargo = table.Column<string>(type: "text", nullable: false),
                    TipoReclutamiento = table.Column<int>(nullable: true),
                    Salario = table.Column<decimal>(type: "money", nullable: true),
                    SalarioPortalReclutamiento = table.Column<bool>(nullable: true),
                    PerfilPortalReclutamiento = table.Column<bool>(nullable: true),
                    CompetenciaPortalReclutamiento = table.Column<bool>(nullable: true),
                    Observacion = table.Column<string>(type: "text", nullable: true),
                    Justificacion = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<string>(type: "varchar(255)", nullable: false),
                    FechaAutorizacion = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequisicionPersonal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequisicionPersonal_CargoDependencia_CargoDependenciaSolicitadoId",
                        column: x => x.CargoDependenciaSolicitadoId,
                        principalSchema: "reclutamiento",
                        principalTable: "CargoDependencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequisicionPersonal_CargoDependencia_CargoDependenciaSolicitanteId",
                        column: x => x.CargoDependenciaSolicitanteId,
                        principalSchema: "reclutamiento",
                        principalTable: "CargoDependencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequisicionPersonal_CentroCosto_CentroCostoId",
                        column: x => x.CentroCostoId,
                        principalSchema: "reclutamiento",
                        principalTable: "CentroCosto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequisicionPersonal_CentroOperativo_CentroOperativoSolicitadoId",
                        column: x => x.CentroOperativoSolicitadoId,
                        principalSchema: "reclutamiento",
                        principalTable: "CentroOperativo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequisicionPersonal_CentroOperativo_CentroOperativoSolicitanteId",
                        column: x => x.CentroOperativoSolicitanteId,
                        principalSchema: "reclutamiento",
                        principalTable: "CentroOperativo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequisicionPersonal_DivisionPoliticaNivel2_DivisionPoliticaNivel2Id",
                        column: x => x.DivisionPoliticaNivel2Id,
                        principalSchema: "reclutamiento",
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequisicionPersonal_Funcionario_FuncionarioAQuienReemplazaId",
                        column: x => x.FuncionarioAQuienReemplazaId,
                        principalSchema: "reclutamiento",
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequisicionPersonal_Funcionario_FuncionarioSolicitanteId",
                        column: x => x.FuncionarioSolicitanteId,
                        principalSchema: "reclutamiento",
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequisicionPersonal_MotivoVacante_MotivoVacanteId",
                        column: x => x.MotivoVacanteId,
                        principalSchema: "reclutamiento",
                        principalTable: "MotivoVacante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequisicionPersonal_TipoContrato_TipoContratoId",
                        column: x => x.TipoContratoId,
                        principalSchema: "reclutamiento",
                        principalTable: "TipoContrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CargoReporta",
                schema: "reclutamiento",
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
                    CargoDependenciaId = table.Column<int>(nullable: false),
                    CargoJefeId = table.Column<int>(nullable: false),
                    JefeInmediato = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoReporta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargoReporta_CargoDependencia_CargoDependenciaId",
                        column: x => x.CargoDependenciaId,
                        principalSchema: "reclutamiento",
                        principalTable: "CargoDependencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CargoReporta_Cargo_CargoJefeId",
                        column: x => x.CargoJefeId,
                        principalSchema: "reclutamiento",
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasica_ActividadEconomicaId",
                schema: "dbo",
                table: "InformacionBasica",
                column: "ActividadEconomicaId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasica_ArlId",
                schema: "dbo",
                table: "InformacionBasica",
                column: "ArlId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasica_CargoId",
                schema: "dbo",
                table: "InformacionBasica",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasica_ClaseAportanteTipoAportanteId",
                schema: "dbo",
                table: "InformacionBasica",
                column: "ClaseAportanteTipoAportanteId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasica_DivisionPoliticaNivel2Id",
                schema: "dbo",
                table: "InformacionBasica",
                column: "DivisionPoliticaNivel2Id");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasica_NaturalezaJuridicaId",
                schema: "dbo",
                table: "InformacionBasica",
                column: "NaturalezaJuridicaId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasica_OperadorPagoId",
                schema: "dbo",
                table: "InformacionBasica",
                column: "OperadorPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasica_TipoContribuyenteId",
                schema: "dbo",
                table: "InformacionBasica",
                column: "TipoContribuyenteId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasica_TipoDocumentoId",
                schema: "dbo",
                table: "InformacionBasica",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasica_TipoPersonaId",
                schema: "dbo",
                table: "InformacionBasica",
                column: "TipoPersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificacionDestinatario_FuncionarioId",
                schema: "dbo",
                table: "NotificacionDestinatario",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificacionDestinatario_NotificacionId",
                schema: "dbo",
                table: "NotificacionDestinatario",
                column: "NotificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicionPersonal_CargoDependenciaSolicitadoId",
                schema: "dbo",
                table: "RequisicionPersonal",
                column: "CargoDependenciaSolicitadoId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicionPersonal_CargoDependenciaSolicitanteId",
                schema: "dbo",
                table: "RequisicionPersonal",
                column: "CargoDependenciaSolicitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicionPersonal_CentroCostoId",
                schema: "dbo",
                table: "RequisicionPersonal",
                column: "CentroCostoId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicionPersonal_CentroOperativoSolicitadoId",
                schema: "dbo",
                table: "RequisicionPersonal",
                column: "CentroOperativoSolicitadoId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicionPersonal_CentroOperativoSolicitanteId",
                schema: "dbo",
                table: "RequisicionPersonal",
                column: "CentroOperativoSolicitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicionPersonal_DivisionPoliticaNivel2Id",
                schema: "dbo",
                table: "RequisicionPersonal",
                column: "DivisionPoliticaNivel2Id");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicionPersonal_FuncionarioAQuienReemplazaId",
                schema: "dbo",
                table: "RequisicionPersonal",
                column: "FuncionarioAQuienReemplazaId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicionPersonal_FuncionarioSolicitanteId",
                schema: "dbo",
                table: "RequisicionPersonal",
                column: "FuncionarioSolicitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicionPersonal_MotivoVacanteId",
                schema: "dbo",
                table: "RequisicionPersonal",
                column: "MotivoVacanteId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicionPersonal_TipoContratoId",
                schema: "dbo",
                table: "RequisicionPersonal",
                column: "TipoContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Administradora_TipoAdministradoraId",
                schema: "reclutamiento",
                table: "Administradora",
                column: "TipoAdministradoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_NivelCargoId",
                schema: "reclutamiento",
                table: "Cargo",
                column: "NivelCargoId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoDependencia_CargoId",
                schema: "reclutamiento",
                table: "CargoDependencia",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoDependencia_DependenciaId",
                schema: "reclutamiento",
                table: "CargoDependencia",
                column: "DependenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoReporta_CargoDependenciaId",
                schema: "reclutamiento",
                table: "CargoReporta",
                column: "CargoDependenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoReporta_CargoJefeId",
                schema: "reclutamiento",
                table: "CargoReporta",
                column: "CargoJefeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaseAportanteTipoAportante_ClaseAportanteId",
                schema: "reclutamiento",
                table: "ClaseAportanteTipoAportante",
                column: "ClaseAportanteId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaseAportanteTipoAportante_TipoAportanteId",
                schema: "reclutamiento",
                table: "ClaseAportanteTipoAportante",
                column: "TipoAportanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_ClaseLibretaMilitarId",
                schema: "reclutamiento",
                table: "Funcionario",
                column: "ClaseLibretaMilitarId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_DivisionPoliticaNivel2ExpedicionDocumentoId",
                schema: "reclutamiento",
                table: "Funcionario",
                column: "DivisionPoliticaNivel2ExpedicionDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_DivisionPoliticaNivel2OrigenId",
                schema: "reclutamiento",
                table: "Funcionario",
                column: "DivisionPoliticaNivel2OrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_DivisionPoliticaNivel2ResidenciaId",
                schema: "reclutamiento",
                table: "Funcionario",
                column: "DivisionPoliticaNivel2ResidenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_EstadoCivilId",
                schema: "reclutamiento",
                table: "Funcionario",
                column: "EstadoCivilId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_LicenciaConduccionAId",
                schema: "reclutamiento",
                table: "Funcionario",
                column: "LicenciaConduccionAId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_LicenciaConduccionBId",
                schema: "reclutamiento",
                table: "Funcionario",
                column: "LicenciaConduccionBId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_LicenciaConduccionCId",
                schema: "reclutamiento",
                table: "Funcionario",
                column: "LicenciaConduccionCId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_OcupacionId",
                schema: "reclutamiento",
                table: "Funcionario",
                column: "OcupacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_SexoId",
                schema: "reclutamiento",
                table: "Funcionario",
                column: "SexoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_TipoDocumentoId",
                schema: "reclutamiento",
                table: "Funcionario",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_TipoSangreId",
                schema: "reclutamiento",
                table: "Funcionario",
                column: "TipoSangreId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_TipoViviendaId",
                schema: "reclutamiento",
                table: "Funcionario",
                column: "TipoViviendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_DivisionPoliticaNivel2_DivisionPoliticaNivel2ExpedicionDocumentoId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "DivisionPoliticaNivel2ExpedicionDocumentoId",
                principalSchema: "reclutamiento",
                principalTable: "DivisionPoliticaNivel2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_DivisionPoliticaNivel2_DivisionPoliticaNivel2OrigenId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "DivisionPoliticaNivel2OrigenId",
                principalSchema: "reclutamiento",
                principalTable: "DivisionPoliticaNivel2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_DivisionPoliticaNivel2_DivisionPoliticaNivel2ResidenciaId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "DivisionPoliticaNivel2ResidenciaId",
                principalSchema: "reclutamiento",
                principalTable: "DivisionPoliticaNivel2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_EstadoCivil_EstadoCivilId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "EstadoCivilId",
                principalSchema: "reclutamiento",
                principalTable: "EstadoCivil",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_Ocupacion_OcupacionId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "OcupacionId",
                principalSchema: "reclutamiento",
                principalTable: "Ocupacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_TipoSangre_TipoSangreId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "TipoSangreId",
                principalSchema: "reclutamiento",
                principalTable: "TipoSangre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_TipoVivienda_TipoViviendaId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "TipoViviendaId",
                principalSchema: "reclutamiento",
                principalTable: "TipoVivienda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_DivisionPoliticaNivel2_DivisionPoliticaNivel2ExpedicionDocumentoId",
                schema: "dbo",
                table: "HojaDeVida");

            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_DivisionPoliticaNivel2_DivisionPoliticaNivel2OrigenId",
                schema: "dbo",
                table: "HojaDeVida");

            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_DivisionPoliticaNivel2_DivisionPoliticaNivel2ResidenciaId",
                schema: "dbo",
                table: "HojaDeVida");

            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_EstadoCivil_EstadoCivilId",
                schema: "dbo",
                table: "HojaDeVida");

            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_Ocupacion_OcupacionId",
                schema: "dbo",
                table: "HojaDeVida");

            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_TipoSangre_TipoSangreId",
                schema: "dbo",
                table: "HojaDeVida");

            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_TipoVivienda_TipoViviendaId",
                schema: "dbo",
                table: "HojaDeVida");

            migrationBuilder.DropTable(
                name: "InformacionBasica",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "NotificacionDestinatario",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "NotificacionPlantilla",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RequisicionPersonal",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TareaProgramada",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CargoReporta",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "ActividadEconomica",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "Administradora",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "ClaseAportanteTipoAportante",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "NaturalezaJuridica",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "OperadorPago",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "TipoContribuyente",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "TipoPersona",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "Notificacion",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CentroCosto",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "CentroOperativo",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "Funcionario",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "MotivoVacante",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "TipoContrato",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "CargoDependencia",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "TipoAdministradora",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "ClaseAportante",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "TipoAportante",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "Cargo",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "Dependencia",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "NivelCargo",
                schema: "reclutamiento");
                */
            migrationBuilder.DropColumn(
                name: "TokenGhestic",
                schema: "reclutamiento",
                table: "AspNetUsers");
            /*
            migrationBuilder.DropColumn(
                name: "EquivalenteBancario",
                schema: "dbo",
                table: "TipoDocumento");

            migrationBuilder.AlterColumn<string>(
                name: "CodigoPila",
                schema: "dbo",
                table: "TipoDocumento",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodigoDian",
                schema: "dbo",
                table: "TipoDocumento",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "UsaLentes",
                schema: "dbo",
                table: "HojaDeVida",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TipoViviendaId",
                schema: "dbo",
                table: "HojaDeVida",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TipoSangreId",
                schema: "dbo",
                table: "HojaDeVida",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Pensionado",
                schema: "dbo",
                table: "HojaDeVida",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OcupacionId",
                schema: "dbo",
                table: "HojaDeVida",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nit",
                schema: "dbo",
                table: "HojaDeVida",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaNacimiento",
                schema: "dbo",
                table: "HojaDeVida",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaExpedicionDocumento",
                schema: "dbo",
                table: "HojaDeVida",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EstadoCivilId",
                schema: "dbo",
                table: "HojaDeVida",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DivisionPoliticaNivel2ResidenciaId",
                schema: "dbo",
                table: "HojaDeVida",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DivisionPoliticaNivel2OrigenId",
                schema: "dbo",
                table: "HojaDeVida",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DivisionPoliticaNivel2ExpedicionDocumentoId",
                schema: "dbo",
                table: "HojaDeVida",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DigitoVerificacion",
                schema: "dbo",
                table: "HojaDeVida",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_DivisionPoliticaNivel2_DivisionPoliticaNivel2ExpedicionDocumentoId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "DivisionPoliticaNivel2ExpedicionDocumentoId",
                principalSchema: "reclutamiento",
                principalTable: "DivisionPoliticaNivel2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_DivisionPoliticaNivel2_DivisionPoliticaNivel2OrigenId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "DivisionPoliticaNivel2OrigenId",
                principalSchema: "reclutamiento",
                principalTable: "DivisionPoliticaNivel2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_DivisionPoliticaNivel2_DivisionPoliticaNivel2ResidenciaId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "DivisionPoliticaNivel2ResidenciaId",
                principalSchema: "reclutamiento",
                principalTable: "DivisionPoliticaNivel2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_EstadoCivil_EstadoCivilId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "EstadoCivilId",
                principalSchema: "reclutamiento",
                principalTable: "EstadoCivil",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_Ocupacion_OcupacionId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "OcupacionId",
                principalSchema: "reclutamiento",
                principalTable: "Ocupacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_TipoSangre_TipoSangreId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "TipoSangreId",
                principalSchema: "reclutamiento",
                principalTable: "TipoSangre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_TipoVivienda_TipoViviendaId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "TipoViviendaId",
                principalSchema: "reclutamiento",
                principalTable: "TipoVivienda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);*/
        }
    }
}
