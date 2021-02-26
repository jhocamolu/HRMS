using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "util");

            migrationBuilder.CreateTable(
                name: "ActividadEconomica",
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
                    table.CheckConstraint("CK_ActividadEconomica_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "Calendario",
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
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendario", x => x.Id);
                    table.CheckConstraint("CK_Calendario_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "CategoriaParametro",
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
                    Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaParametro", x => x.Id);
                    table.CheckConstraint("CK_CategoriaParametro_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "CentroCosto",
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
                    table.CheckConstraint("CK_CentroCosto_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "CentroOperativo",
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
                    table.CheckConstraint("CK_CentroOperativo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "CentroTrabajo",
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
                    Nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    PorcentajeRiesgo = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentroTrabajo", x => x.Id);
                    table.CheckConstraint("CK_CentroTrabajo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "ClaseAusentismo",
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
                    Codigo = table.Column<string>(type: "varchar(5)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false),
                    AfectaDiaPagar = table.Column<bool>(type: "bit", nullable: false),
                    AfectaDiaTrabajado = table.Column<bool>(type: "bit", nullable: false),
                    RequiereHora = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaseAusentismo", x => x.Id);
                    table.CheckConstraint("CK_ClaseAusentismo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "ClaseLibretaMilitar",
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
                    table.PrimaryKey("PK_ClaseLibretaMilitar", x => x.Id);
                    table.CheckConstraint("CK_ClaseLibretaMilitar_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "CuentaContable",
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
                    Cuenta = table.Column<string>(type: "varchar(255)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false),
                    Naturaleza = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentaContable", x => x.Id);
                    table.CheckConstraint("CK_CuentaContable_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "Dependencia",
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
                    table.CheckConstraint("CK_Dependencia_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticoCie",
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
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticoCie", x => x.Id);
                    table.CheckConstraint("CK_DiagnosticoCie_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "EstadoCivil",
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
                    table.PrimaryKey("PK_EstadoCivil", x => x.Id);
                    table.CheckConstraint("CK_EstadoCivil_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "FormaPago",
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
                    HabilitaEnContrato = table.Column<bool>(nullable: false),
                    HabilitaEntidadFinanciera = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaPago", x => x.Id);
                    table.CheckConstraint("CK_FormaPago_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "FuncionNomina",
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
                    Alias = table.Column<string>(type: "varchar(255)", nullable: false),
                    Ayuda = table.Column<string>(type: "text", nullable: false),
                    Proceso = table.Column<string>(type: "text", nullable: false),
                    TipoFuncion = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionNomina", x => x.Id);
                    table.CheckConstraint("CK_FuncionNomina_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "GrupoNomina",
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
                    table.PrimaryKey("PK_GrupoNomina", x => x.Id);
                    table.CheckConstraint("CK_GrupoNomina_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "Idioma",
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
                    table.PrimaryKey("PK_Idioma", x => x.Id);
                    table.CheckConstraint("CK_Idioma_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "JornadaLaboral",
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
                    table.PrimaryKey("PK_JornadaLaboral", x => x.Id);
                    table.CheckConstraint("CK_JornadaLaboral_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "LicenciaConduccion",
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
                    Clase = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenciaConduccion", x => x.Id);
                    table.CheckConstraint("CK_LicenciaConduccion_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "NivelCargo",
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
                    table.CheckConstraint("CK_NivelCargo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "NivelEducativo",
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
                    table.PrimaryKey("PK_NivelEducativo", x => x.Id);
                    table.CheckConstraint("CK_NivelEducativo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "NomenclaturaDian",
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
                    TextoPosterior = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NomenclaturaDian", x => x.Id);
                    table.CheckConstraint("CK_NomenclaturaDian_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "NominaFuenteNovedad",
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
                    Modulo = table.Column<string>(type: "varchar(255)", nullable: false),
                    ModuloRegistroId = table.Column<int>(nullable: false),
                    DiasCausados = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NominaFuenteNovedad", x => x.Id);
                    table.CheckConstraint("CK_NominaFuenteNovedad_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "Notificacion",
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
                    table.CheckConstraint("CK_Notificacion_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_Notificacion_Tipo", "([Tipo]='MobilePush' OR [Tipo]='Email')");
                });

            migrationBuilder.CreateTable(
                name: "NotificacionPlantilla",
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
                    table.CheckConstraint("CK_NotificacionPlantilla_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "Ocupacion",
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
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocupacion", x => x.Id);
                    table.CheckConstraint("CK_Ocupacion_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "OperadorPago",
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
                    table.CheckConstraint("CK_OperadorPago_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "Pais",
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
                    Nacionalidad = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.Id);
                    table.CheckConstraint("CK_Pais_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "Parentesco",
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
                    Tipo = table.Column<string>(type: "varchar(255)", nullable: false),
                    Grado = table.Column<string>(type: "varchar(255)", nullable: false),
                    NumeroPersonasPermitidas = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parentesco", x => x.Id);
                    table.CheckConstraint("CK_Parentesco_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "PeriodoContable",
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
                    Nombre = table.Column<string>(type: "varchar(20)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Estado = table.Column<string>(type: "char(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodoContable", x => x.Id);
                    table.CheckConstraint("CK_PeriodoContable_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "Profesion",
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
                    table.PrimaryKey("PK_Profesion", x => x.Id);
                    table.CheckConstraint("CK_Profesion_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "Sexo",
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
                    table.PrimaryKey("PK_Sexo", x => x.Id);
                    table.CheckConstraint("CK_Sexo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "TareaProgramada",
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
                    Periodicidad = table.Column<string>(type: "varchar(255)", nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Instruccion = table.Column<string>(type: "varchar(255)", nullable: true),
                    EnEjecucion = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareaProgramada", x => x.Id);
                    table.CheckConstraint("CK_TareaProgramada_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "TipoAdministradora",
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
                    table.PrimaryKey("PK_TipoAdministradora", x => x.Id);
                    table.CheckConstraint("CK_TipoAdministradora_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "TipoContrato",
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
                    TerminoIndefinido = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoContrato", x => x.Id);
                    table.CheckConstraint("CK_TipoContrato_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "TipoContribuyente",
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
                    table.CheckConstraint("CK_TipoContribuyente_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "TipoCuenta",
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
                    table.PrimaryKey("PK_TipoCuenta", x => x.Id);
                    table.CheckConstraint("CK_TipoCuenta_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumento",
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
                    CodigoPila = table.Column<string>(type: "varchar(10)", nullable: false),
                    CodigoDian = table.Column<string>(type: "varchar(10)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false),
                    Formato = table.Column<string>(type: "varchar(255)", nullable: false),
                    Validacion = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumento", x => x.Id);
                    table.CheckConstraint("CK_TipoDocumento_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "TipoEmbargo",
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
                    SalarioMinimoEmbargable = table.Column<bool>(nullable: false),
                    Prioridad = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEmbargo", x => x.Id);
                    table.CheckConstraint("CK_TipoEmbargo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "TipoMoneda",
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
                    table.PrimaryKey("PK_TipoMoneda", x => x.Id);
                    table.CheckConstraint("CK_TipoMoneda_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "TipoPeriodo",
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
                    PagoPorDefecto = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPeriodo", x => x.Id);
                    table.CheckConstraint("CK_TipoPeriodo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "TipoSangre",
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
                    table.PrimaryKey("PK_TipoSangre", x => x.Id);
                    table.CheckConstraint("CK_TipoSangre_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "TipoSoporte",
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
                    table.PrimaryKey("PK_TipoSoporte", x => x.Id);
                    table.CheckConstraint("CK_TipoSoporte_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "TipoVivienda",
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
                    table.PrimaryKey("PK_TipoVivienda", x => x.Id);
                    table.CheckConstraint("CK_TipoVivienda_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "VariableNomina",
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
                    TipoDato = table.Column<string>(type: "varchar(255)", nullable: false),
                    Tamanio = table.Column<string>(type: "varchar(255)", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    TipoVariable = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariableNomina", x => x.Id);
                    table.CheckConstraint("CK_VariableNomina_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_VariableNomina_TipoDato", "([TipoDato] = 'BIGINT' OR [TipoDato] = 'NUMERIC' OR [TipoDato] = 'BIT' OR [TipoDato] = 'SMALLINT' OR [TipoDato] = 'DECIMAL' OR [TipoDato] = 'SMALLMONEY' OR [TipoDato] = 'INT' OR [TipoDato] = 'TINYINT' OR [TipoDato] = 'MONEY' OR [TipoDato] = 'FLOAT' OR [TipoDato] = 'REAL' OR [TipoDato] = 'DATE' OR [TipoDato] = 'DATETIMEOFFSET' OR [TipoDato] = 'DATETIME2' OR [TipoDato] = 'SMALLDATETIME' OR [TipoDato] = 'DATETIME' OR [TipoDato] = 'TIME' OR [TipoDato] = 'CHAR' OR [TipoDato] = 'VARCHAR' OR [TipoDato] = 'TEXT' OR [TipoDato] = 'NCHAR' OR [TipoDato] = 'NVARCHAR' OR [TipoDato] = 'NTEXT' OR [TipoDato] = 'BINARY' OR [TipoDato] = 'VARBINARY' OR [TipoDato] = 'IMAGE' OR [TipoDato] = 'CURSOR' OR [TipoDato] = 'ROWVERSION' OR [TipoDato] = 'HIERARCHYID' OR [TipoDato] = 'UNIQUEIDENTIFIER' OR [TipoDato] = 'SQL_VARIANT' OR [TipoDato] = 'XML' OR [TipoDato] = 'TABLE')");
                });

            migrationBuilder.CreateTable(
                name: "_LogConfiguracion",
                schema: "util",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "varchar(255)", nullable: true),
                    Tabla = table.Column<string>(type: "varchar(255)", nullable: true),
                    Activo = table.Column<bool>(nullable: false),
                    Fecha = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LogConfiguracion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParametroGeneral",
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
                    Tipo = table.Column<string>(type: "varchar(255)", nullable: false),
                    HtmlOpcion = table.Column<string>(type: "varchar(255)", nullable: true),
                    Etiqueta = table.Column<string>(type: "varchar(255)", nullable: false),
                    Ayuda = table.Column<string>(type: "text", nullable: false),
                    Orden = table.Column<short>(type: "smallint", nullable: false),
                    Item = table.Column<string>(type: "text", nullable: true),
                    Obligatorio = table.Column<bool>(nullable: false),
                    Valor = table.Column<string>(type: "varchar(255)", nullable: true),
                    CategoriaParametroId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametroGeneral", x => x.Id);
                    table.CheckConstraint("CK_ParametroGeneral_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_ParametroGeneral_CategoriaParametro_CategoriaParametroId",
                        column: x => x.CategoriaParametroId,
                        principalTable: "CategoriaParametro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TipoAusentismo",
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
                    Codigo = table.Column<string>(type: "varchar(5)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClaseAusentismoId = table.Column<int>(nullable: false),
                    UnidadTiempo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAusentismo", x => x.Id);
                    table.CheckConstraint("CK_TipoAusentismo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_TipoAusentismo_ClaseAusentismo_ClaseAusentismoId",
                        column: x => x.ClaseAusentismoId,
                        principalTable: "ClaseAusentismo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DependenciaJerarquia",
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
                    DependenciaHijoId = table.Column<int>(nullable: false),
                    DependenciaPadreId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DependenciaJerarquia", x => x.Id);
                    table.CheckConstraint("CK_DependenciaJerarquia_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_DependenciaJerarquia_Dependencia_DependenciaHijoId",
                        column: x => x.DependenciaHijoId,
                        principalTable: "Dependencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DependenciaJerarquia_Dependencia_DependenciaPadreId",
                        column: x => x.DependenciaPadreId,
                        principalTable: "Dependencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConceptoNomina",
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
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false),
                    TipoConceptoNomina = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClaseConceptoNomina = table.Column<string>(type: "varchar(255)", nullable: false),
                    Orden = table.Column<int>(nullable: false),
                    ConceptoAgrupador = table.Column<bool>(type: "bit", nullable: false),
                    OrigenCentroCosto = table.Column<string>(type: "varchar(255)", nullable: false),
                    OrigenTercero = table.Column<string>(type: "varchar(255)", nullable: false),
                    VisibleImpresion = table.Column<bool>(type: "bit", nullable: false),
                    UnidadMedida = table.Column<string>(type: "varchar(255)", nullable: false),
                    RequiereCantidad = table.Column<bool>(type: "bit", nullable: false),
                    FuncionNominaId = table.Column<int>(nullable: true),
                    NitTercero = table.Column<string>(type: "varchar(255)", nullable: true),
                    DigitoVerificacion = table.Column<string>(type: "varchar(255)", nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Formula = table.Column<string>(type: "text", nullable: true),
                    ProcedimientoSql = table.Column<string>(type: "text", nullable: true),
                    ProcedimientoNombre = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptoNomina", x => x.Id);
                    table.CheckConstraint("CK_ConceptoNomina_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_ConceptoNomina_FuncionNomina_FuncionNominaId",
                        column: x => x.FuncionNominaId,
                        principalTable: "FuncionNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JornadaLaboralDia",
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
                    JornadaLaboralId = table.Column<int>(nullable: false),
                    Dia = table.Column<string>(type: "varchar(255)", nullable: false),
                    HoraInicioJornada = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraInicioDescanso = table.Column<TimeSpan>(type: "time", nullable: true),
                    HoraFinDescanso = table.Column<TimeSpan>(type: "time", nullable: true),
                    HoraFinJornada = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JornadaLaboralDia", x => x.Id);
                    table.CheckConstraint("CK_JornadaLaboralDia_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_JornadaLaboralDia_JornadaLaboral_JornadaLaboralId",
                        column: x => x.JornadaLaboralId,
                        principalTable: "JornadaLaboral",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cargo",
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
                    NivelCargoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.Id);
                    table.CheckConstraint("CK_Cargo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_Cargo_NivelCargo_NivelCargoId",
                        column: x => x.NivelCargoId,
                        principalTable: "NivelCargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DivisionPoliticaNivel1",
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
                    PaisId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionPoliticaNivel1", x => x.Id);
                    table.CheckConstraint("CK_DivisionPoliticaNivel1_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_DivisionPoliticaNivel1_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TareaProgramadaLog",
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
                    TareaProgramadaId = table.Column<int>(nullable: false),
                    Estado = table.Column<string>(nullable: false),
                    Resultado = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareaProgramadaLog", x => x.Id);
                    table.CheckConstraint("CK_TareasProgramadasLogs_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_TareaProgramadaLog_TareaProgramada_TareaProgramadaId",
                        column: x => x.TareaProgramadaId,
                        principalTable: "TareaProgramada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administradora",
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
                    table.CheckConstraint("CK_Administradora_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_Administradora_TipoAdministradora_TipoAdministradoraId",
                        column: x => x.TipoAdministradoraId,
                        principalTable: "TipoAdministradora",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubPeriodo",
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
                    TipoPeriodoId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false),
                    Dias = table.Column<int>(nullable: false),
                    DiaInicial = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubPeriodo", x => x.Id);
                    table.CheckConstraint("CK_SubPeriodo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_SubPeriodo_TipoPeriodo_TipoPeriodoId",
                        column: x => x.TipoPeriodoId,
                        principalTable: "TipoPeriodo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TipoLiquidacion",
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
                    TipoPeriodoId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoLiquidacion", x => x.Id);
                    table.CheckConstraint("CK_TipoLiquidacion_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_TipoLiquidacion_TipoPeriodo_TipoPeriodoId",
                        column: x => x.TipoPeriodoId,
                        principalTable: "TipoPeriodo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FuncionVariable",
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
                    FuncionNominaId = table.Column<int>(nullable: false),
                    VariableNominaId = table.Column<int>(nullable: false),
                    Orden = table.Column<int>(nullable: false),
                    ValorDefecto = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionVariable", x => x.Id);
                    table.CheckConstraint("CK_FuncionVariable_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_FuncionVariable_FuncionNomina_FuncionNominaId",
                        column: x => x.FuncionNominaId,
                        principalTable: "FuncionNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuncionVariable_VariableNomina_VariableNominaId",
                        column: x => x.VariableNominaId,
                        principalTable: "VariableNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConceptoBase",
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
                    ConceptoNominaAgrupadorId = table.Column<int>(nullable: false),
                    ConceptoNominaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptoBase", x => x.Id);
                    table.CheckConstraint("CK_ConceptoBase_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_ConceptoBase_ConceptoNomina_ConceptoNominaAgrupadorId",
                        column: x => x.ConceptoNominaAgrupadorId,
                        principalTable: "ConceptoNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConceptoBase_ConceptoNomina_ConceptoNominaId",
                        column: x => x.ConceptoNominaId,
                        principalTable: "ConceptoNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConceptoNominaCuentaContable",
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
                    CentroCostoId = table.Column<int>(nullable: true),
                    CuentaContableId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptoNominaCuentaContable", x => x.Id);
                    table.CheckConstraint("CK_ConceptoNominaCuentaContable_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_ConceptoNominaCuentaContable_CentroCosto_CentroCostoId",
                        column: x => x.CentroCostoId,
                        principalTable: "CentroCosto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConceptoNominaCuentaContable_ConceptoNomina_ConceptoNominaId",
                        column: x => x.ConceptoNominaId,
                        principalTable: "ConceptoNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConceptoNominaCuentaContable_CuentaContable_CuentaContableId",
                        column: x => x.CuentaContableId,
                        principalTable: "CuentaContable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TipoAusentismoConceptoNomina",
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
                    TipoAusentismoId = table.Column<int>(nullable: false),
                    ConceptoNominaId = table.Column<int>(nullable: false),
                    CoberturaDesde = table.Column<int>(nullable: false),
                    CoberturaHasta = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAusentismoConceptoNomina", x => x.Id);
                    table.CheckConstraint("CK_TipoAusentismoConceptoNomina_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_TipoAusentismoConceptoNomina_ConceptoNomina_ConceptoNominaId",
                        column: x => x.ConceptoNominaId,
                        principalTable: "ConceptoNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TipoAusentismoConceptoNomina_TipoAusentismo_TipoAusentismoId",
                        column: x => x.TipoAusentismoId,
                        principalTable: "TipoAusentismo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TipoBeneficio",
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
                    ConceptoNominaDevengoId = table.Column<int>(nullable: true),
                    ConceptoNominaDeduccionId = table.Column<int>(nullable: true),
                    ConceptoNominaCalculoId = table.Column<int>(nullable: true),
                    RequiereAprobacionJefe = table.Column<bool>(nullable: false),
                    MontoMaximo = table.Column<decimal>(type: "money", nullable: false),
                    ValorSolicitado = table.Column<bool>(nullable: false),
                    PlazoMes = table.Column<bool>(nullable: false),
                    CuotaPermitida = table.Column<int>(nullable: false),
                    PeriodoPago = table.Column<bool>(nullable: false),
                    PermiteAuxilioEducativo = table.Column<bool>(nullable: false),
                    PermiteDescuentoNomina = table.Column<bool>(nullable: false),
                    ValorAutorizado = table.Column<bool>(nullable: false),
                    DiasAntiguedad = table.Column<int>(nullable: false),
                    VecesAnio = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoBeneficio", x => x.Id);
                    table.CheckConstraint("CK_TipoBeneficio_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_TipoBeneficio_ConceptoNomina_ConceptoNominaCalculoId",
                        column: x => x.ConceptoNominaCalculoId,
                        principalTable: "ConceptoNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TipoBeneficio_ConceptoNomina_ConceptoNominaDeduccionId",
                        column: x => x.ConceptoNominaDeduccionId,
                        principalTable: "ConceptoNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TipoBeneficio_ConceptoNomina_ConceptoNominaDevengoId",
                        column: x => x.ConceptoNominaDevengoId,
                        principalTable: "ConceptoNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoEmbargoConceptoNomina",
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
                    TipoEmbargoId = table.Column<int>(nullable: false),
                    ConceptoNominaId = table.Column<int>(nullable: false),
                    MaximoEmbargarConcepto = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEmbargoConceptoNomina", x => x.Id);
                    table.CheckConstraint("CK_TipoEmbargoConceptoNomina_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_TipoEmbargoConceptoNomina_ConceptoNomina_ConceptoNominaId",
                        column: x => x.ConceptoNominaId,
                        principalTable: "ConceptoNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TipoEmbargoConceptoNomina_TipoEmbargo_TipoEmbargoId",
                        column: x => x.TipoEmbargoId,
                        principalTable: "TipoEmbargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CargoDependencia",
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
                    table.CheckConstraint("CK_CargoDependencia_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_CargoDependencia_Cargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CargoDependencia_Dependencia_DependenciaId",
                        column: x => x.DependenciaId,
                        principalTable: "Dependencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CargoGrado",
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
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    CargoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoGrado", x => x.Id);
                    table.CheckConstraint("CK_CargoGrado_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_CargoGrado_Cargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CargoReporta",
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
                    CargoFuncionarioId = table.Column<int>(nullable: false),
                    CargoJefeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoReporta", x => x.Id);
                    table.CheckConstraint("CK_CargoReporta_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_CargoReporta_Cargo_CargoFuncionarioId",
                        column: x => x.CargoFuncionarioId,
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CargoReporta_Cargo_CargoJefeId",
                        column: x => x.CargoJefeId,
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DivisionPoliticaNivel2",
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
                    DivisionPoliticaNivel1Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionPoliticaNivel2", x => x.Id);
                    table.CheckConstraint("CK_DivisionPoliticaNivel2_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_DivisionPoliticaNivel2_DivisionPoliticaNivel1_DivisionPoliticaNivel1Id",
                        column: x => x.DivisionPoliticaNivel1Id,
                        principalTable: "DivisionPoliticaNivel1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nomina",
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
                    Numero = table.Column<int>(nullable: false),
                    PeriodoContableId = table.Column<int>(nullable: false),
                    TipoLiquidacionId = table.Column<int>(nullable: false),
                    SubperiodoId = table.Column<int>(nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "date", nullable: false),
                    FechaAplicacion = table.Column<DateTime>(type: "date", nullable: true),
                    Estado = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nomina", x => x.Id);
                    table.CheckConstraint("CK_Nomina_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_Nomina_Estado", "([Estado]='Inicializada' OR [Estado]='Modificada' OR [Estado]='EnLiquidacion' OR [Estado]='Liquidada' OR [Estado]='Aprobada' OR [Estado]='Aplicada')");
                    table.ForeignKey(
                        name: "FK_Nomina_PeriodoContable_PeriodoContableId",
                        column: x => x.PeriodoContableId,
                        principalTable: "PeriodoContable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nomina_SubPeriodo_SubperiodoId",
                        column: x => x.SubperiodoId,
                        principalTable: "SubPeriodo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nomina_TipoLiquidacion_TipoLiquidacionId",
                        column: x => x.TipoLiquidacionId,
                        principalTable: "TipoLiquidacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoLiquidacionConcepto",
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
                    TipoliquidacionId = table.Column<int>(nullable: false),
                    ConceptoNominaId = table.Column<int>(nullable: false),
                    TipoContratoId = table.Column<int>(nullable: true),
                    SubPeriodoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoLiquidacionConcepto", x => x.Id);
                    table.CheckConstraint("CK_TipoLiquidacionConcepto_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_TipoLiquidacionConcepto_ConceptoNomina_ConceptoNominaId",
                        column: x => x.ConceptoNominaId,
                        principalTable: "ConceptoNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TipoLiquidacionConcepto_SubPeriodo_SubPeriodoId",
                        column: x => x.SubPeriodoId,
                        principalTable: "SubPeriodo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TipoLiquidacionConcepto_TipoContrato_TipoContratoId",
                        column: x => x.TipoContratoId,
                        principalTable: "TipoContrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TipoLiquidacionConcepto_TipoLiquidacion_TipoliquidacionId",
                        column: x => x.TipoliquidacionId,
                        principalTable: "TipoLiquidacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoLiquidacionEstado",
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
                    TipoLiquidacionId = table.Column<int>(nullable: false),
                    EstadoFuncionario = table.Column<string>(type: "varchar(20)", nullable: false),
                    EstadoContrato = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoLiquidacionEstado", x => x.Id);
                    table.CheckConstraint("CK_TipoLiquidacionEstado_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_TipoLiquidacionEstado_TipoLiquidacion_TipoLiquidacionId",
                        column: x => x.TipoLiquidacionId,
                        principalTable: "TipoLiquidacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TipoBeneficioRequisito",
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
                    TipoBeneficioId = table.Column<int>(nullable: false),
                    TipoSoporteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoBeneficioRequisito", x => x.Id);
                    table.CheckConstraint("CK_TipoBeneficioRequisito_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_TipoBeneficioRequisito_TipoBeneficio_TipoBeneficioId",
                        column: x => x.TipoBeneficioId,
                        principalTable: "TipoBeneficio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TipoBeneficioRequisito_TipoSoporte_TipoSoporteId",
                        column: x => x.TipoSoporteId,
                        principalTable: "TipoSoporte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntidadFinanciera",
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
                    DivisionPoliticaNivel2Id = table.Column<int>(nullable: false),
                    Telefono = table.Column<string>(type: "varchar(255)", nullable: false),
                    Direccion = table.Column<string>(type: "varchar(255)", nullable: false),
                    RepresentanteLegal = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntidadFinanciera", x => x.Id);
                    table.CheckConstraint("CK_EntidadFinanciera_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_EntidadFinanciera_DivisionPoliticaNivel2_DivisionPoliticaNivel2Id",
                        column: x => x.DivisionPoliticaNivel2Id,
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Funcionario",
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
                    table.CheckConstraint("CK_Funcionario_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_Funcionario_ClaseLibretaMilitar_ClaseLibretaMilitarId",
                        column: x => x.ClaseLibretaMilitarId,
                        principalTable: "ClaseLibretaMilitar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_DivisionPoliticaNivel2_DivisionPoliticaNivel2ExpedicionDocumentoId",
                        column: x => x.DivisionPoliticaNivel2ExpedicionDocumentoId,
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_DivisionPoliticaNivel2_DivisionPoliticaNivel2OrigenId",
                        column: x => x.DivisionPoliticaNivel2OrigenId,
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_DivisionPoliticaNivel2_DivisionPoliticaNivel2ResidenciaId",
                        column: x => x.DivisionPoliticaNivel2ResidenciaId,
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_EstadoCivil_EstadoCivilId",
                        column: x => x.EstadoCivilId,
                        principalTable: "EstadoCivil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionario_LicenciaConduccion_LicenciaConduccionAId",
                        column: x => x.LicenciaConduccionAId,
                        principalTable: "LicenciaConduccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_LicenciaConduccion_LicenciaConduccionBId",
                        column: x => x.LicenciaConduccionBId,
                        principalTable: "LicenciaConduccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_LicenciaConduccion_LicenciaConduccionCId",
                        column: x => x.LicenciaConduccionCId,
                        principalTable: "LicenciaConduccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_Ocupacion_OcupacionId",
                        column: x => x.OcupacionId,
                        principalTable: "Ocupacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionario_Sexo_SexoId",
                        column: x => x.SexoId,
                        principalTable: "Sexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionario_TipoDocumento_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TipoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionario_TipoSangre_TipoSangreId",
                        column: x => x.TipoSangreId,
                        principalTable: "TipoSangre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionario_TipoVivienda_TipoViviendaId",
                        column: x => x.TipoViviendaId,
                        principalTable: "TipoVivienda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InformacionBasica",
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
                    CorreoElectronico = table.Column<string>(type: "varchar(255)", nullable: true),
                    Web = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaConstitucion = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    TipoContribuyenteId = table.Column<int>(nullable: false),
                    OperadorPagoId = table.Column<int>(nullable: false),
                    ArlId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformacionBasica", x => x.Id);
                    table.CheckConstraint("CK_InformacionBasica_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_InformacionBasica_ActividadEconomica_ActividadEconomicaId",
                        column: x => x.ActividadEconomicaId,
                        principalTable: "ActividadEconomica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacionBasica_Administradora_ArlId",
                        column: x => x.ArlId,
                        principalTable: "Administradora",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacionBasica_DivisionPoliticaNivel2_DivisionPoliticaNivel2Id",
                        column: x => x.DivisionPoliticaNivel2Id,
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacionBasica_OperadorPago_OperadorPagoId",
                        column: x => x.OperadorPagoId,
                        principalTable: "OperadorPago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacionBasica_TipoContribuyente_TipoContribuyenteId",
                        column: x => x.TipoContribuyenteId,
                        principalTable: "TipoContribuyente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Juzgado",
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
                    NumeroCuentaJudicial = table.Column<string>(type: "varchar(255)", nullable: false),
                    DivisionPoliticaNivel2Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Juzgado", x => x.Id);
                    table.CheckConstraint("CK_Juzgado_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_Juzgado_DivisionPoliticaNivel2_DivisionPoliticaNivel2Id",
                        column: x => x.DivisionPoliticaNivel2Id,
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AusentismoFuncionario",
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
                    FuncionarioId = table.Column<int>(nullable: false),
                    TipoAusentismoId = table.Column<int>(nullable: false),
                    DiagnosticoCieId = table.Column<int>(nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "time", nullable: true),
                    HoraFin = table.Column<TimeSpan>(type: "time", nullable: true),
                    NumeroIncapacidad = table.Column<string>(type: "varchar(255)", nullable: true),
                    Adjunto = table.Column<string>(type: "varchar(255)", nullable: true),
                    Estado = table.Column<string>(type: "char(20)", nullable: false),
                    Justificacion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AusentismoFuncionario", x => x.Id);
                    table.CheckConstraint("CK_AusentismoFuncionario_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_AusentismoFuncionario_DiagnosticoCie_DiagnosticoCieId",
                        column: x => x.DiagnosticoCieId,
                        principalTable: "DiagnosticoCie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AusentismoFuncionario_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AusentismoFuncionario_TipoAusentismo_TipoAusentismoId",
                        column: x => x.TipoAusentismoId,
                        principalTable: "TipoAusentismo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Beneficio",
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
                    FuncionarioId = table.Column<int>(nullable: false),
                    TipoBeneficioId = table.Column<int>(nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "date", nullable: false),
                    ValorSolicitud = table.Column<decimal>(type: "money", nullable: true),
                    PlazoMaximo = table.Column<int>(nullable: true),
                    TipoPeriodoId = table.Column<int>(nullable: true),
                    OpcionAuxilioEducativo = table.Column<string>(type: "varchar(100)", nullable: true),
                    CantidadHoraSemana = table.Column<int>(nullable: true),
                    FechaInicioEstudio = table.Column<DateTime>(type: "date", nullable: true),
                    FechaFinalizacionEstudio = table.Column<DateTime>(type: "date", nullable: true),
                    Observacion = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<string>(type: "varchar(100)", nullable: false),
                    ObservacionAprobacion = table.Column<string>(type: "text", nullable: true),
                    ObservacionAutorizacion = table.Column<string>(type: "text", nullable: true),
                    ValorAutorizado = table.Column<decimal>(type: "money", nullable: true),
                    ValorCobrar = table.Column<decimal>(type: "money", nullable: true),
                    NotaAcademica = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Saldo = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficio", x => x.Id);
                    table.CheckConstraint("CK_Beneficio_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_Beneficio_Estado", "([Estado] = 'EnTramite' OR[Estado] = 'Aprobada' OR[Estado] = 'Autorizada' OR[Estado] = 'Otorgada' OR[Estado] = 'EnReembolso' OR[Estado] = 'EnCondonacion' OR[Estado] = 'Condonada' OR[Estado] = 'Rechazada' OR[Estado] = 'Cancelada' OR[Estado] = 'Finalizada')");
                    table.ForeignKey(
                        name: "FK_Beneficio_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Beneficio_TipoBeneficio_TipoBeneficioId",
                        column: x => x.TipoBeneficioId,
                        principalTable: "TipoBeneficio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Beneficio_TipoPeriodo_TipoPeriodoId",
                        column: x => x.TipoPeriodoId,
                        principalTable: "TipoPeriodo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contrato",
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
                    FuncionarioId = table.Column<int>(nullable: false),
                    TipoContratoId = table.Column<int>(nullable: false),
                    Estado = table.Column<string>(type: "char(30)", nullable: false),
                    NumeroContrato = table.Column<string>(type: "varchar(255)", nullable: false),
                    CargoDependenciaId = table.Column<int>(nullable: false),
                    PeriodoPrueba = table.Column<string>(type: "varchar(255)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFinalizacion = table.Column<DateTime>(type: "date", nullable: true),
                    FechaTerminacion = table.Column<DateTime>(type: "date", nullable: true),
                    Sueldo = table.Column<decimal>(type: "money", nullable: false),
                    CentroOperativoId = table.Column<int>(nullable: false),
                    DivisionPoliticaNivel2Id = table.Column<int>(nullable: false),
                    CentroCostoId = table.Column<int>(nullable: false),
                    FormaPagoId = table.Column<int>(nullable: false),
                    TipoMonedaId = table.Column<int>(nullable: false),
                    EntidadFinancieraId = table.Column<int>(nullable: true),
                    TipoCuentaId = table.Column<int>(nullable: true),
                    NumeroCuenta = table.Column<string>(type: "varchar(255)", nullable: true),
                    RecibeDotacion = table.Column<bool>(nullable: false),
                    JornadaLaboralId = table.Column<int>(nullable: false),
                    EmpleadoConfianza = table.Column<bool>(nullable: false),
                    ProcedimientoRetencio = table.Column<string>(type: "varchar(255)", nullable: false),
                    CentroTrabajoId = table.Column<int>(nullable: false),
                    Observaciones = table.Column<string>(type: "text", nullable: true),
                    GrupoNominaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato", x => x.Id);
                    table.CheckConstraint("CK_Contrato_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_Contrato_CargoDependencia_CargoDependenciaId",
                        column: x => x.CargoDependenciaId,
                        principalTable: "CargoDependencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contrato_CentroCosto_CentroCostoId",
                        column: x => x.CentroCostoId,
                        principalTable: "CentroCosto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contrato_CentroOperativo_CentroOperativoId",
                        column: x => x.CentroOperativoId,
                        principalTable: "CentroOperativo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contrato_CentroTrabajo_CentroTrabajoId",
                        column: x => x.CentroTrabajoId,
                        principalTable: "CentroTrabajo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contrato_DivisionPoliticaNivel2_DivisionPoliticaNivel2Id",
                        column: x => x.DivisionPoliticaNivel2Id,
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contrato_EntidadFinanciera_EntidadFinancieraId",
                        column: x => x.EntidadFinancieraId,
                        principalTable: "EntidadFinanciera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contrato_FormaPago_FormaPagoId",
                        column: x => x.FormaPagoId,
                        principalTable: "FormaPago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contrato_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contrato_GrupoNomina_GrupoNominaId",
                        column: x => x.GrupoNominaId,
                        principalTable: "GrupoNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contrato_JornadaLaboral_JornadaLaboralId",
                        column: x => x.JornadaLaboralId,
                        principalTable: "JornadaLaboral",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contrato_TipoContrato_TipoContratoId",
                        column: x => x.TipoContratoId,
                        principalTable: "TipoContrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contrato_TipoCuenta_TipoCuentaId",
                        column: x => x.TipoCuentaId,
                        principalTable: "TipoCuenta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contrato_TipoMoneda_TipoMonedaId",
                        column: x => x.TipoMonedaId,
                        principalTable: "TipoMoneda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentoFuncionario",
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
                    FuncionarioId = table.Column<int>(nullable: false),
                    TipoSoporteId = table.Column<int>(nullable: false),
                    Comentario = table.Column<string>(type: "text", nullable: false),
                    Adjunto = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentoFuncionario", x => x.Id);
                    table.CheckConstraint("CK_DocumentoFuncionario_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_DocumentoFuncionario_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentoFuncionario_TipoSoporte_TipoSoporteId",
                        column: x => x.TipoSoporteId,
                        principalTable: "TipoSoporte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperienciaLaboral",
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
                    FuncionarioId = table.Column<int>(nullable: false),
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
                    Observaciones = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<string>(nullable: false),
                    Justificacion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienciaLaboral", x => x.Id);
                    table.CheckConstraint("CK_ExperienciaLaboral_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_ExperienciaLaboral_Estado", "([Estado]='Pendiente' OR [Estado]='Rechazado' OR [Estado]='Validado')");
                    table.ForeignKey(
                        name: "FK_ExperienciaLaboral_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FuncionarioEstudio",
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
                    FuncionarioId = table.Column<int>(nullable: false),
                    NivelEducativoId = table.Column<int>(nullable: false),
                    InstitucionEducativa = table.Column<string>(type: "varchar(255)", nullable: false),
                    PaisId = table.Column<int>(nullable: false),
                    ProfesionId = table.Column<int>(nullable: true),
                    AnioDeInicio = table.Column<DateTime>(type: "date", nullable: false),
                    AnioDeFin = table.Column<DateTime>(type: "date", nullable: true),
                    EstadoEstudio = table.Column<string>(type: "varchar(255)", nullable: false),
                    TarjetaProfesional = table.Column<string>(type: "varchar(255)", nullable: true),
                    Titulo = table.Column<string>(type: "varchar(255)", nullable: false),
                    Observacion = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<string>(nullable: false),
                    Justificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionarioEstudio", x => x.Id);
                    table.CheckConstraint("CK_FuncionarioEstudio_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_FuncionarioEstudio_Estado", "([Estado]='Pendiente' OR [Estado]='Rechazado' OR [Estado]='Validado')");
                    table.ForeignKey(
                        name: "FK_FuncionarioEstudio_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuncionarioEstudio_NivelEducativo_NivelEducativoId",
                        column: x => x.NivelEducativoId,
                        principalTable: "NivelEducativo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuncionarioEstudio_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuncionarioEstudio_Profesion_ProfesionId",
                        column: x => x.ProfesionId,
                        principalTable: "Profesion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InformacionFamiliar",
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
                    FuncionarioId = table.Column<int>(nullable: false),
                    PrimerNombre = table.Column<string>(type: "varchar(255)", nullable: false),
                    SegundoNombre = table.Column<string>(type: "varchar(255)", nullable: true),
                    PrimerApellido = table.Column<string>(type: "varchar(255)", nullable: false),
                    SegundoApellido = table.Column<string>(type: "varchar(255)", nullable: true),
                    SexoId = table.Column<int>(nullable: false),
                    ParentescoId = table.Column<int>(nullable: false),
                    Dependiente = table.Column<bool>(nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "date", nullable: false),
                    TipoDocumentoId = table.Column<int>(nullable: false),
                    NumeroDocumento = table.Column<string>(type: "varchar(255)", nullable: false),
                    NivelEducativoId = table.Column<int>(nullable: true),
                    TelefonoFijo = table.Column<string>(type: "varchar(255)", nullable: true),
                    Celular = table.Column<string>(type: "varchar(255)", nullable: false),
                    DivisionPoliticaNivel2Id = table.Column<int>(nullable: false),
                    Direccion = table.Column<string>(type: "varchar(255)", nullable: false),
                    Estado = table.Column<string>(nullable: false),
                    Justificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformacionFamiliar", x => x.Id);
                    table.CheckConstraint("CK_InformacionFamiliar_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_InformacionFamiliar_Estado", "([Estado]='Pendiente' OR [Estado]='Rechazado' OR [Estado]='Validado')");
                    table.ForeignKey(
                        name: "FK_InformacionFamiliar_DivisionPoliticaNivel2_DivisionPoliticaNivel2Id",
                        column: x => x.DivisionPoliticaNivel2Id,
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InformacionFamiliar_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacionFamiliar_NivelEducativo_NivelEducativoId",
                        column: x => x.NivelEducativoId,
                        principalTable: "NivelEducativo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InformacionFamiliar_Parentesco_ParentescoId",
                        column: x => x.ParentescoId,
                        principalTable: "Parentesco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacionFamiliar_Sexo_SexoId",
                        column: x => x.SexoId,
                        principalTable: "Sexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InformacionFamiliar_TipoDocumento_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TipoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Libranza",
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
                    FuncionarioId = table.Column<int>(nullable: false),
                    EntidadFinancieraId = table.Column<int>(nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    ValorPrestamo = table.Column<decimal>(type: "money", nullable: false),
                    Estado = table.Column<string>(type: "varchar(10)", nullable: false),
                    NumeroCuotas = table.Column<int>(nullable: true),
                    Observacion = table.Column<string>(type: "text", nullable: true),
                    ValorCuota = table.Column<decimal>(type: "money", nullable: false),
                    Justificacion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libranza", x => x.Id);
                    table.CheckConstraint("CK_Libranza_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_Libranza_Estado", "([Estado]='Vigente' OR [Estado]='Terminada' OR [Estado]='Pendiente' OR [Estado]='Anulada')");
                    table.ForeignKey(
                        name: "FK_Libranza_EntidadFinanciera_EntidadFinancieraId",
                        column: x => x.EntidadFinancieraId,
                        principalTable: "EntidadFinanciera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Libranza_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NominaFuncionario",
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
                    NominaId = table.Column<int>(nullable: false),
                    FuncionarioId = table.Column<int>(nullable: false),
                    NetoPagar = table.Column<decimal>(type: "money", nullable: false),
                    Estado = table.Column<string>(type: "char(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NominaFuncionario", x => x.Id);
                    table.CheckConstraint("CK_NominaFuncionario_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_NominaFuncionario_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NominaFuncionario_Nomina_NominaId",
                        column: x => x.NominaId,
                        principalTable: "Nomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificacionDestinatario",
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
                    FuncionarioId = table.Column<int>(nullable: false),
                    Estado = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificacionDestinatario", x => x.Id);
                    table.CheckConstraint("CK_NotificacionDestinatario_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_NotificacionDestinatario_Estado", "([Estado]='Pendiente' OR [Estado]='Enviado' OR [Estado]='Fallido')");
                    table.ForeignKey(
                        name: "FK_NotificacionDestinatario_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificacionDestinatario_Notificacion_NotificacionId",
                        column: x => x.NotificacionId,
                        principalTable: "Notificacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificacionDestinatarioLog",
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
                    FuncionarioId = table.Column<int>(nullable: false),
                    Estado = table.Column<string>(type: "varchar(255)", nullable: false),
                    Resultado = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificacionDestinatarioLog", x => x.Id);
                    table.CheckConstraint("CK_NotificacionDestinatarioLog_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_NotificacionDestinatarioLog_Estado", "([Estado]='Pendiente' OR [Estado]='Enviado' OR [Estado]='Fallido')");
                    table.ForeignKey(
                        name: "FK_NotificacionDestinatarioLog_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificacionDestinatarioLog_Notificacion_NotificacionId",
                        column: x => x.NotificacionId,
                        principalTable: "Notificacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepresentanteEmpresa",
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
                    FuncionarioId = table.Column<int>(nullable: false),
                    CargoId = table.Column<int>(nullable: false),
                    GrupoDocumentoSlug = table.Column<string>(type: "varchar(255)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepresentanteEmpresa", x => x.Id);
                    table.CheckConstraint("CK_RepresentanteEmpresa_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_RepresentanteEmpresa_Cargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepresentanteEmpresa_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudPermiso",
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
                    FuncionarioId = table.Column<int>(nullable: false),
                    TipoAusentismoId = table.Column<int>(nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: false),
                    HoraSalida = table.Column<TimeSpan>(type: "time", nullable: true),
                    HoraLlegada = table.Column<TimeSpan>(type: "time", nullable: true),
                    Observaciones = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<string>(type: "char(20)", nullable: false),
                    Justificacion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudPermiso", x => x.Id);
                    table.CheckConstraint("CK_SolicitudPermiso_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_SolicitudPermiso_Estado", "([Estado]='Aprobada' OR [Estado]='Autorizada' OR [Estado]='Cancelada' OR [Estado]='Rechazada' OR [Estado]='Solicitada')");
                    table.ForeignKey(
                        name: "FK_SolicitudPermiso_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudPermiso_TipoAusentismo_TipoAusentismoId",
                        column: x => x.TipoAusentismoId,
                        principalTable: "TipoAusentismo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Embargo",
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
                    FuncionarioId = table.Column<int>(nullable: false),
                    JuzgadoId = table.Column<int>(nullable: true),
                    TipoEmbargoId = table.Column<int>(nullable: false),
                    NumeroProceso = table.Column<string>(type: "varchar(255)", nullable: true),
                    ValorEmbargo = table.Column<decimal>(type: "money", nullable: true),
                    ValorCuota = table.Column<decimal>(type: "money", nullable: true),
                    Prioridad = table.Column<int>(nullable: false),
                    EntidadFinancieraId = table.Column<int>(nullable: false),
                    NumeroCuenta = table.Column<string>(type: "varchar(255)", nullable: false),
                    NumeroDocumentoDemandante = table.Column<string>(type: "varchar(255)", nullable: false),
                    DigitoVerificacionDemandante = table.Column<short>(type: "smallint", nullable: true),
                    Demandante = table.Column<string>(type: "varchar(255)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: true),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true),
                    Estado = table.Column<string>(type: "varchar(10)", nullable: false),
                    ActualizaPrioridad = table.Column<bool>(type: "bit", nullable: true),
                    PorcentajeCuota = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Justificacion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Embargo", x => x.Id);
                    table.CheckConstraint("CK_Embargo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_Embargo_Estado", "([Estado]='Vigente' OR [Estado]='Pendiente' OR [Estado]='Liquidado' OR [Estado]='Anulado' OR [Estado]='Terminado')");
                    table.ForeignKey(
                        name: "FK_Embargo_EntidadFinanciera_EntidadFinancieraId",
                        column: x => x.EntidadFinancieraId,
                        principalTable: "EntidadFinanciera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Embargo_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Embargo_Juzgado_JuzgadoId",
                        column: x => x.JuzgadoId,
                        principalTable: "Juzgado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Embargo_TipoEmbargo_TipoEmbargoId",
                        column: x => x.TipoEmbargoId,
                        principalTable: "TipoEmbargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProrrogaAusentismo",
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
                    ProrrogaId = table.Column<int>(nullable: false),
                    AusentismoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProrrogaAusentismo", x => x.Id);
                    table.CheckConstraint("CK_ProrrogaAusentismo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_ProrrogaAusentismo_AusentismoFuncionario_AusentismoId",
                        column: x => x.AusentismoId,
                        principalTable: "AusentismoFuncionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProrrogaAusentismo_AusentismoFuncionario_ProrrogaId",
                        column: x => x.ProrrogaId,
                        principalTable: "AusentismoFuncionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BeneficioAdjunto",
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
                    BeneficioId = table.Column<int>(nullable: false),
                    TipoBeneficioRequisitoId = table.Column<int>(nullable: false),
                    Adjunto = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeneficioAdjunto", x => x.Id);
                    table.CheckConstraint("CK_BeneficioAdjunto_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_BeneficioAdjunto_Beneficio_BeneficioId",
                        column: x => x.BeneficioId,
                        principalTable: "Beneficio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeneficioAdjunto_TipoBeneficioRequisito_TipoBeneficioRequisitoId",
                        column: x => x.TipoBeneficioRequisitoId,
                        principalTable: "TipoBeneficioRequisito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BeneficioSubperiodo",
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
                    BeneficioId = table.Column<int>(nullable: false),
                    SubPeriodoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeneficioSubperiodo", x => x.Id);
                    table.CheckConstraint("CK_BeneficioSubperiodo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_BeneficioSubperiodo_Beneficio_BeneficioId",
                        column: x => x.BeneficioId,
                        principalTable: "Beneficio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeneficioSubperiodo_SubPeriodo_SubPeriodoId",
                        column: x => x.SubPeriodoId,
                        principalTable: "SubPeriodo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContratoAdministradora",
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
                    ContratoId = table.Column<int>(nullable: false),
                    AdministradoraId = table.Column<int>(nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratoAdministradora", x => x.Id);
                    table.CheckConstraint("CK_ContratoAdministradora_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_ContratoAdministradora_Administradora_AdministradoraId",
                        column: x => x.AdministradoraId,
                        principalTable: "Administradora",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContratoAdministradora_Contrato_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContratoOtroSi",
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
                    ContratoId = table.Column<int>(nullable: false),
                    TipoContratoId = table.Column<int>(nullable: false),
                    FechaFinalizacion = table.Column<DateTime>(type: "date", nullable: true),
                    CargoDependenciaId = table.Column<int>(nullable: false),
                    NumeroOtroSi = table.Column<int>(nullable: false),
                    Sueldo = table.Column<decimal>(type: "money", nullable: false),
                    FechaAplicacion = table.Column<DateTime>(type: "date", nullable: false),
                    CentroOperativoId = table.Column<int>(nullable: false),
                    DivisionPoliticaNivel2Id = table.Column<int>(nullable: false),
                    Observaciones = table.Column<string>(type: "text", nullable: false),
                    Prorroga = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratoOtroSi", x => x.Id);
                    table.CheckConstraint("CK_ContratoOtroSi_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_ContratoOtroSi_CargoDependencia_CargoDependenciaId",
                        column: x => x.CargoDependenciaId,
                        principalTable: "CargoDependencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContratoOtroSi_CentroOperativo_CentroOperativoId",
                        column: x => x.CentroOperativoId,
                        principalTable: "CentroOperativo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContratoOtroSi_Contrato_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContratoOtroSi_DivisionPoliticaNivel2_DivisionPoliticaNivel2Id",
                        column: x => x.DivisionPoliticaNivel2Id,
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContratoOtroSi_TipoContrato_TipoContratoId",
                        column: x => x.TipoContratoId,
                        principalTable: "TipoContrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LibroVacaciones",
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
                    ContratoId = table.Column<int>(nullable: false),
                    InicioCausacion = table.Column<DateTime>(type: "date", nullable: false),
                    FinCausacion = table.Column<DateTime>(type: "date", nullable: false),
                    Tipo = table.Column<string>(type: "varchar(255)", nullable: false),
                    DiasTrabajados = table.Column<short>(type: "smallint", nullable: false),
                    DiasCausados = table.Column<byte>(type: "tinyint", nullable: false),
                    DiasDisponibles = table.Column<double>(type: "float", nullable: false),
                    DiasDebe = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibroVacaciones", x => x.Id);
                    table.CheckConstraint("CK_LibroVacaciones_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_LibroVacaciones_Tipo", "([Tipo]='Anticipado' OR [Tipo]='Causado')");
                    table.ForeignKey(
                        name: "FK_LibroVacaciones_Contrato_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LibranzaSubperiodo",
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
                    LibranzaId = table.Column<int>(nullable: false),
                    SubPeriodoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibranzaSubperiodo", x => x.Id);
                    table.CheckConstraint("CK_LibranzaSubperiodo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_LibranzaSubperiodo_Libranza_LibranzaId",
                        column: x => x.LibranzaId,
                        principalTable: "Libranza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibranzaSubperiodo_SubPeriodo_SubPeriodoId",
                        column: x => x.SubPeriodoId,
                        principalTable: "SubPeriodo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NominaDetalle",
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
                    NominaFuncionarioId = table.Column<int>(nullable: false),
                    NominaFuenteNovedadId = table.Column<int>(nullable: true),
                    ConceptoNominaId = table.Column<int>(nullable: false),
                    UnidadMedida = table.Column<string>(type: "varchar(255)", nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(type: "money", nullable: false),
                    Estado = table.Column<string>(type: "varchar(255)", nullable: false),
                    Inconsistencia = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NominaDetalle", x => x.Id);
                    table.CheckConstraint("CK_NominaDetalle_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_NominaDetalle_ConceptoNomina_ConceptoNominaId",
                        column: x => x.ConceptoNominaId,
                        principalTable: "ConceptoNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NominaDetalle_NominaFuenteNovedad_NominaFuenteNovedadId",
                        column: x => x.NominaFuenteNovedadId,
                        principalTable: "NominaFuenteNovedad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NominaDetalle_NominaFuncionario_NominaFuncionarioId",
                        column: x => x.NominaFuncionarioId,
                        principalTable: "NominaFuncionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SoporteSolicitudPermiso",
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
                    SolicitudPermisoId = table.Column<int>(nullable: false),
                    TipoSoporteId = table.Column<int>(nullable: false),
                    Comentario = table.Column<string>(type: "text", nullable: true),
                    Adjunto = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoporteSolicitudPermiso", x => x.Id);
                    table.CheckConstraint("CK_SoporteSolicitudPermiso_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_SoporteSolicitudPermiso_SolicitudPermiso_SolicitudPermisoId",
                        column: x => x.SolicitudPermisoId,
                        principalTable: "SolicitudPermiso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoporteSolicitudPermiso_TipoSoporte_TipoSoporteId",
                        column: x => x.TipoSoporteId,
                        principalTable: "TipoSoporte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmbargoConceptoNomina",
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
                    EmbargoId = table.Column<int>(nullable: false),
                    ConceptoNominaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmbargoConceptoNomina", x => x.Id);
                    table.CheckConstraint("CK_EmbargoConceptoNomina_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_EmbargoConceptoNomina_ConceptoNomina_ConceptoNominaId",
                        column: x => x.ConceptoNominaId,
                        principalTable: "ConceptoNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmbargoConceptoNomina_Embargo_EmbargoId",
                        column: x => x.EmbargoId,
                        principalTable: "Embargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmbargoSubperiodo",
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
                    EmbargoId = table.Column<int>(nullable: false),
                    SubPeriodoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmbargoSubperiodo", x => x.Id);
                    table.CheckConstraint("CK_EmbargoSubperiodo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_EmbargoSubperiodo_Embargo_EmbargoId",
                        column: x => x.EmbargoId,
                        principalTable: "Embargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmbargoSubperiodo_SubPeriodo_SubPeriodoId",
                        column: x => x.SubPeriodoId,
                        principalTable: "SubPeriodo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudVacaciones",
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
                    FuncionarioId = table.Column<int>(nullable: false),
                    LibroVacacionesId = table.Column<int>(nullable: false),
                    NominaId = table.Column<int>(nullable: true),
                    FechaInicioDisfrute = table.Column<DateTime>(type: "date", nullable: false),
                    DiasDisfrute = table.Column<byte>(type: "tinyint", nullable: false),
                    FechaFinDisfrute = table.Column<DateTime>(type: "date", nullable: false),
                    DiasDinero = table.Column<byte>(type: "tinyint", nullable: false),
                    Observacion = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<string>(type: "varchar(255)", nullable: false),
                    Justificacion = table.Column<string>(type: "text", nullable: true),
                    FechaPago = table.Column<DateTime>(type: "date", nullable: false),
                    Remuneracion = table.Column<decimal>(type: "money", nullable: false),
                    FechaRegreso = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudVacaciones", x => x.Id);
                    table.CheckConstraint("CK_SolicitudVacaciones_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_SolicitudVacaciones_Estado", "([Estado]='Aprobada' OR [Estado]='Autorizada' OR [Estado]='Cancelada' OR [Estado]='EnCurso' OR [Estado]='Interrumpida' OR [Estado]='Rechazada' OR [Estado]='Solicitada'  OR [Estado]='Terminada')");
                    table.ForeignKey(
                        name: "FK_SolicitudVacaciones_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudVacaciones_LibroVacaciones_LibroVacacionesId",
                        column: x => x.LibroVacacionesId,
                        principalTable: "LibroVacaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudVacaciones_Nomina_NominaId",
                        column: x => x.NominaId,
                        principalTable: "Nomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudVacacionesInterrupcion",
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
                    SolicitudVacacionesId = table.Column<int>(nullable: false),
                    FuncionarioAusentismoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudVacacionesInterrupcion", x => x.Id);
                    table.CheckConstraint("CK_SolicitudVacacionesInterrupcion_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_SolicitudVacacionesInterrupcion_AusentismoFuncionario_FuncionarioAusentismoId",
                        column: x => x.FuncionarioAusentismoId,
                        principalTable: "AusentismoFuncionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudVacacionesInterrupcion_SolicitudVacaciones_SolicitudVacacionesId",
                        column: x => x.SolicitudVacacionesId,
                        principalTable: "SolicitudVacaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administradora_TipoAdministradoraId",
                table: "Administradora",
                column: "TipoAdministradoraId");

            migrationBuilder.CreateIndex(
                name: "IX_AusentismoFuncionario_DiagnosticoCieId",
                table: "AusentismoFuncionario",
                column: "DiagnosticoCieId");

            migrationBuilder.CreateIndex(
                name: "IX_AusentismoFuncionario_FuncionarioId",
                table: "AusentismoFuncionario",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AusentismoFuncionario_TipoAusentismoId",
                table: "AusentismoFuncionario",
                column: "TipoAusentismoId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficio_FuncionarioId",
                table: "Beneficio",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficio_TipoBeneficioId",
                table: "Beneficio",
                column: "TipoBeneficioId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficio_TipoPeriodoId",
                table: "Beneficio",
                column: "TipoPeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_BeneficioAdjunto_BeneficioId",
                table: "BeneficioAdjunto",
                column: "BeneficioId");

            migrationBuilder.CreateIndex(
                name: "IX_BeneficioAdjunto_TipoBeneficioRequisitoId",
                table: "BeneficioAdjunto",
                column: "TipoBeneficioRequisitoId");

            migrationBuilder.CreateIndex(
                name: "IX_BeneficioSubperiodo_BeneficioId",
                table: "BeneficioSubperiodo",
                column: "BeneficioId");

            migrationBuilder.CreateIndex(
                name: "IX_BeneficioSubperiodo_SubPeriodoId",
                table: "BeneficioSubperiodo",
                column: "SubPeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_NivelCargoId",
                table: "Cargo",
                column: "NivelCargoId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoDependencia_CargoId",
                table: "CargoDependencia",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoDependencia_DependenciaId",
                table: "CargoDependencia",
                column: "DependenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoGrado_CargoId",
                table: "CargoGrado",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoReporta_CargoFuncionarioId",
                table: "CargoReporta",
                column: "CargoFuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoReporta_CargoJefeId",
                table: "CargoReporta",
                column: "CargoJefeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaseAusentismo_Codigo",
                table: "ClaseAusentismo",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConceptoBase_ConceptoNominaAgrupadorId",
                table: "ConceptoBase",
                column: "ConceptoNominaAgrupadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ConceptoBase_ConceptoNominaId",
                table: "ConceptoBase",
                column: "ConceptoNominaId");

            migrationBuilder.CreateIndex(
                name: "IX_ConceptoNomina_Alias",
                table: "ConceptoNomina",
                column: "Alias");

            migrationBuilder.CreateIndex(
                name: "IX_ConceptoNomina_FuncionNominaId",
                table: "ConceptoNomina",
                column: "FuncionNominaId");

            migrationBuilder.CreateIndex(
                name: "IX_ConceptoNominaCuentaContable_CentroCostoId",
                table: "ConceptoNominaCuentaContable",
                column: "CentroCostoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConceptoNominaCuentaContable_ConceptoNominaId",
                table: "ConceptoNominaCuentaContable",
                column: "ConceptoNominaId");

            migrationBuilder.CreateIndex(
                name: "IX_ConceptoNominaCuentaContable_CuentaContableId",
                table: "ConceptoNominaCuentaContable",
                column: "CuentaContableId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_CargoDependenciaId",
                table: "Contrato",
                column: "CargoDependenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_CentroCostoId",
                table: "Contrato",
                column: "CentroCostoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_CentroOperativoId",
                table: "Contrato",
                column: "CentroOperativoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_CentroTrabajoId",
                table: "Contrato",
                column: "CentroTrabajoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_DivisionPoliticaNivel2Id",
                table: "Contrato",
                column: "DivisionPoliticaNivel2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_EntidadFinancieraId",
                table: "Contrato",
                column: "EntidadFinancieraId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_FormaPagoId",
                table: "Contrato",
                column: "FormaPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_FuncionarioId",
                table: "Contrato",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_GrupoNominaId",
                table: "Contrato",
                column: "GrupoNominaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_JornadaLaboralId",
                table: "Contrato",
                column: "JornadaLaboralId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_TipoContratoId",
                table: "Contrato",
                column: "TipoContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_TipoCuentaId",
                table: "Contrato",
                column: "TipoCuentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_TipoMonedaId",
                table: "Contrato",
                column: "TipoMonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoAdministradora_AdministradoraId",
                table: "ContratoAdministradora",
                column: "AdministradoraId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoAdministradora_ContratoId",
                table: "ContratoAdministradora",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoOtroSi_CargoDependenciaId",
                table: "ContratoOtroSi",
                column: "CargoDependenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoOtroSi_CentroOperativoId",
                table: "ContratoOtroSi",
                column: "CentroOperativoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoOtroSi_ContratoId",
                table: "ContratoOtroSi",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoOtroSi_DivisionPoliticaNivel2Id",
                table: "ContratoOtroSi",
                column: "DivisionPoliticaNivel2Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoOtroSi_TipoContratoId",
                table: "ContratoOtroSi",
                column: "TipoContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_DependenciaJerarquia_DependenciaHijoId",
                table: "DependenciaJerarquia",
                column: "DependenciaHijoId");

            migrationBuilder.CreateIndex(
                name: "IX_DependenciaJerarquia_DependenciaPadreId",
                table: "DependenciaJerarquia",
                column: "DependenciaPadreId");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionPoliticaNivel1_PaisId",
                table: "DivisionPoliticaNivel1",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionPoliticaNivel2_DivisionPoliticaNivel1Id",
                table: "DivisionPoliticaNivel2",
                column: "DivisionPoliticaNivel1Id");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentoFuncionario_FuncionarioId",
                table: "DocumentoFuncionario",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentoFuncionario_TipoSoporteId",
                table: "DocumentoFuncionario",
                column: "TipoSoporteId");

            migrationBuilder.CreateIndex(
                name: "IX_Embargo_EntidadFinancieraId",
                table: "Embargo",
                column: "EntidadFinancieraId");

            migrationBuilder.CreateIndex(
                name: "IX_Embargo_FuncionarioId",
                table: "Embargo",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Embargo_JuzgadoId",
                table: "Embargo",
                column: "JuzgadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Embargo_TipoEmbargoId",
                table: "Embargo",
                column: "TipoEmbargoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbargoConceptoNomina_ConceptoNominaId",
                table: "EmbargoConceptoNomina",
                column: "ConceptoNominaId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbargoConceptoNomina_EmbargoId",
                table: "EmbargoConceptoNomina",
                column: "EmbargoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbargoSubperiodo_EmbargoId",
                table: "EmbargoSubperiodo",
                column: "EmbargoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbargoSubperiodo_SubPeriodoId",
                table: "EmbargoSubperiodo",
                column: "SubPeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_EntidadFinanciera_DivisionPoliticaNivel2Id",
                table: "EntidadFinanciera",
                column: "DivisionPoliticaNivel2Id");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienciaLaboral_FuncionarioId",
                table: "ExperienciaLaboral",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_ClaseLibretaMilitarId",
                table: "Funcionario",
                column: "ClaseLibretaMilitarId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_DivisionPoliticaNivel2ExpedicionDocumentoId",
                table: "Funcionario",
                column: "DivisionPoliticaNivel2ExpedicionDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_DivisionPoliticaNivel2OrigenId",
                table: "Funcionario",
                column: "DivisionPoliticaNivel2OrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_DivisionPoliticaNivel2ResidenciaId",
                table: "Funcionario",
                column: "DivisionPoliticaNivel2ResidenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_EstadoCivilId",
                table: "Funcionario",
                column: "EstadoCivilId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_LicenciaConduccionAId",
                table: "Funcionario",
                column: "LicenciaConduccionAId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_LicenciaConduccionBId",
                table: "Funcionario",
                column: "LicenciaConduccionBId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_LicenciaConduccionCId",
                table: "Funcionario",
                column: "LicenciaConduccionCId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_OcupacionId",
                table: "Funcionario",
                column: "OcupacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_SexoId",
                table: "Funcionario",
                column: "SexoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_TipoDocumentoId",
                table: "Funcionario",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_TipoSangreId",
                table: "Funcionario",
                column: "TipoSangreId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_TipoViviendaId",
                table: "Funcionario",
                column: "TipoViviendaId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioEstudio_FuncionarioId",
                table: "FuncionarioEstudio",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioEstudio_NivelEducativoId",
                table: "FuncionarioEstudio",
                column: "NivelEducativoId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioEstudio_PaisId",
                table: "FuncionarioEstudio",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioEstudio_ProfesionId",
                table: "FuncionarioEstudio",
                column: "ProfesionId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionVariable_FuncionNominaId",
                table: "FuncionVariable",
                column: "FuncionNominaId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionVariable_VariableNominaId",
                table: "FuncionVariable",
                column: "VariableNominaId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasica_ActividadEconomicaId",
                table: "InformacionBasica",
                column: "ActividadEconomicaId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasica_ArlId",
                table: "InformacionBasica",
                column: "ArlId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasica_DivisionPoliticaNivel2Id",
                table: "InformacionBasica",
                column: "DivisionPoliticaNivel2Id");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasica_OperadorPagoId",
                table: "InformacionBasica",
                column: "OperadorPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasica_TipoContribuyenteId",
                table: "InformacionBasica",
                column: "TipoContribuyenteId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionFamiliar_DivisionPoliticaNivel2Id",
                table: "InformacionFamiliar",
                column: "DivisionPoliticaNivel2Id");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionFamiliar_FuncionarioId",
                table: "InformacionFamiliar",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionFamiliar_NivelEducativoId",
                table: "InformacionFamiliar",
                column: "NivelEducativoId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionFamiliar_ParentescoId",
                table: "InformacionFamiliar",
                column: "ParentescoId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionFamiliar_SexoId",
                table: "InformacionFamiliar",
                column: "SexoId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionFamiliar_TipoDocumentoId",
                table: "InformacionFamiliar",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_JornadaLaboralDia_JornadaLaboralId",
                table: "JornadaLaboralDia",
                column: "JornadaLaboralId");

            migrationBuilder.CreateIndex(
                name: "IX_Juzgado_DivisionPoliticaNivel2Id",
                table: "Juzgado",
                column: "DivisionPoliticaNivel2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Libranza_EntidadFinancieraId",
                table: "Libranza",
                column: "EntidadFinancieraId");

            migrationBuilder.CreateIndex(
                name: "IX_Libranza_FuncionarioId",
                table: "Libranza",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_LibranzaSubperiodo_LibranzaId",
                table: "LibranzaSubperiodo",
                column: "LibranzaId");

            migrationBuilder.CreateIndex(
                name: "IX_LibranzaSubperiodo_SubPeriodoId",
                table: "LibranzaSubperiodo",
                column: "SubPeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_LibroVacaciones_ContratoId",
                table: "LibroVacaciones",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Nomina_PeriodoContableId",
                table: "Nomina",
                column: "PeriodoContableId");

            migrationBuilder.CreateIndex(
                name: "IX_Nomina_SubperiodoId",
                table: "Nomina",
                column: "SubperiodoId");

            migrationBuilder.CreateIndex(
                name: "IX_Nomina_TipoLiquidacionId",
                table: "Nomina",
                column: "TipoLiquidacionId");

            migrationBuilder.CreateIndex(
                name: "IX_NominaDetalle_ConceptoNominaId",
                table: "NominaDetalle",
                column: "ConceptoNominaId");

            migrationBuilder.CreateIndex(
                name: "IX_NominaDetalle_NominaFuenteNovedadId",
                table: "NominaDetalle",
                column: "NominaFuenteNovedadId");

            migrationBuilder.CreateIndex(
                name: "IX_NominaDetalle_NominaFuncionarioId",
                table: "NominaDetalle",
                column: "NominaFuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_NominaFuncionario_FuncionarioId",
                table: "NominaFuncionario",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_NominaFuncionario_NominaId_FuncionarioId",
                table: "NominaFuncionario",
                columns: new[] { "NominaId", "FuncionarioId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificacionDestinatario_FuncionarioId",
                table: "NotificacionDestinatario",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificacionDestinatario_NotificacionId",
                table: "NotificacionDestinatario",
                column: "NotificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificacionDestinatarioLog_FuncionarioId",
                table: "NotificacionDestinatarioLog",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificacionDestinatarioLog_NotificacionId",
                table: "NotificacionDestinatarioLog",
                column: "NotificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ParametroGeneral_CategoriaParametroId",
                table: "ParametroGeneral",
                column: "CategoriaParametroId");

            migrationBuilder.CreateIndex(
                name: "IX_ProrrogaAusentismo_AusentismoId",
                table: "ProrrogaAusentismo",
                column: "AusentismoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProrrogaAusentismo_ProrrogaId",
                table: "ProrrogaAusentismo",
                column: "ProrrogaId");

            migrationBuilder.CreateIndex(
                name: "IX_RepresentanteEmpresa_CargoId",
                table: "RepresentanteEmpresa",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_RepresentanteEmpresa_FuncionarioId",
                table: "RepresentanteEmpresa",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudPermiso_FuncionarioId",
                table: "SolicitudPermiso",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudPermiso_TipoAusentismoId",
                table: "SolicitudPermiso",
                column: "TipoAusentismoId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudVacaciones_FuncionarioId",
                table: "SolicitudVacaciones",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudVacaciones_LibroVacacionesId",
                table: "SolicitudVacaciones",
                column: "LibroVacacionesId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudVacaciones_NominaId",
                table: "SolicitudVacaciones",
                column: "NominaId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudVacacionesInterrupcion_FuncionarioAusentismoId",
                table: "SolicitudVacacionesInterrupcion",
                column: "FuncionarioAusentismoId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudVacacionesInterrupcion_SolicitudVacacionesId",
                table: "SolicitudVacacionesInterrupcion",
                column: "SolicitudVacacionesId");

            migrationBuilder.CreateIndex(
                name: "IX_SoporteSolicitudPermiso_SolicitudPermisoId",
                table: "SoporteSolicitudPermiso",
                column: "SolicitudPermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_SoporteSolicitudPermiso_TipoSoporteId",
                table: "SoporteSolicitudPermiso",
                column: "TipoSoporteId");

            migrationBuilder.CreateIndex(
                name: "IX_SubPeriodo_TipoPeriodoId",
                table: "SubPeriodo",
                column: "TipoPeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_TareaProgramadaLog_TareaProgramadaId",
                table: "TareaProgramadaLog",
                column: "TareaProgramadaId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoAusentismo_ClaseAusentismoId",
                table: "TipoAusentismo",
                column: "ClaseAusentismoId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoAusentismo_Codigo",
                table: "TipoAusentismo",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoAusentismoConceptoNomina_ConceptoNominaId",
                table: "TipoAusentismoConceptoNomina",
                column: "ConceptoNominaId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoAusentismoConceptoNomina_TipoAusentismoId",
                table: "TipoAusentismoConceptoNomina",
                column: "TipoAusentismoId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoBeneficio_ConceptoNominaCalculoId",
                table: "TipoBeneficio",
                column: "ConceptoNominaCalculoId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoBeneficio_ConceptoNominaDeduccionId",
                table: "TipoBeneficio",
                column: "ConceptoNominaDeduccionId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoBeneficio_ConceptoNominaDevengoId",
                table: "TipoBeneficio",
                column: "ConceptoNominaDevengoId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoBeneficioRequisito_TipoBeneficioId",
                table: "TipoBeneficioRequisito",
                column: "TipoBeneficioId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoBeneficioRequisito_TipoSoporteId",
                table: "TipoBeneficioRequisito",
                column: "TipoSoporteId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoEmbargoConceptoNomina_ConceptoNominaId",
                table: "TipoEmbargoConceptoNomina",
                column: "ConceptoNominaId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoEmbargoConceptoNomina_TipoEmbargoId",
                table: "TipoEmbargoConceptoNomina",
                column: "TipoEmbargoId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoLiquidacion_TipoPeriodoId",
                table: "TipoLiquidacion",
                column: "TipoPeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoLiquidacionConcepto_ConceptoNominaId",
                table: "TipoLiquidacionConcepto",
                column: "ConceptoNominaId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoLiquidacionConcepto_SubPeriodoId",
                table: "TipoLiquidacionConcepto",
                column: "SubPeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoLiquidacionConcepto_TipoContratoId",
                table: "TipoLiquidacionConcepto",
                column: "TipoContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoLiquidacionConcepto_TipoliquidacionId",
                table: "TipoLiquidacionConcepto",
                column: "TipoliquidacionId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoLiquidacionEstado_TipoLiquidacionId",
                table: "TipoLiquidacionEstado",
                column: "TipoLiquidacionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeneficioAdjunto");

            migrationBuilder.DropTable(
                name: "BeneficioSubperiodo");

            migrationBuilder.DropTable(
                name: "Calendario");

            migrationBuilder.DropTable(
                name: "CargoGrado");

            migrationBuilder.DropTable(
                name: "CargoReporta");

            migrationBuilder.DropTable(
                name: "ConceptoBase");

            migrationBuilder.DropTable(
                name: "ConceptoNominaCuentaContable");

            migrationBuilder.DropTable(
                name: "ContratoAdministradora");

            migrationBuilder.DropTable(
                name: "ContratoOtroSi");

            migrationBuilder.DropTable(
                name: "DependenciaJerarquia");

            migrationBuilder.DropTable(
                name: "DocumentoFuncionario");

            migrationBuilder.DropTable(
                name: "EmbargoConceptoNomina");

            migrationBuilder.DropTable(
                name: "EmbargoSubperiodo");

            migrationBuilder.DropTable(
                name: "ExperienciaLaboral");

            migrationBuilder.DropTable(
                name: "FuncionarioEstudio");

            migrationBuilder.DropTable(
                name: "FuncionVariable");

            migrationBuilder.DropTable(
                name: "Idioma");

            migrationBuilder.DropTable(
                name: "InformacionBasica");

            migrationBuilder.DropTable(
                name: "InformacionFamiliar");

            migrationBuilder.DropTable(
                name: "JornadaLaboralDia");

            migrationBuilder.DropTable(
                name: "LibranzaSubperiodo");

            migrationBuilder.DropTable(
                name: "NomenclaturaDian");

            migrationBuilder.DropTable(
                name: "NominaDetalle");

            migrationBuilder.DropTable(
                name: "NotificacionDestinatario");

            migrationBuilder.DropTable(
                name: "NotificacionDestinatarioLog");

            migrationBuilder.DropTable(
                name: "NotificacionPlantilla");

            migrationBuilder.DropTable(
                name: "ParametroGeneral");

            migrationBuilder.DropTable(
                name: "ProrrogaAusentismo");

            migrationBuilder.DropTable(
                name: "RepresentanteEmpresa");

            migrationBuilder.DropTable(
                name: "SolicitudVacacionesInterrupcion");

            migrationBuilder.DropTable(
                name: "SoporteSolicitudPermiso");

            migrationBuilder.DropTable(
                name: "TareaProgramadaLog");

            migrationBuilder.DropTable(
                name: "TipoAusentismoConceptoNomina");

            migrationBuilder.DropTable(
                name: "TipoEmbargoConceptoNomina");

            migrationBuilder.DropTable(
                name: "TipoLiquidacionConcepto");

            migrationBuilder.DropTable(
                name: "TipoLiquidacionEstado");

            migrationBuilder.DropTable(
                name: "_LogConfiguracion",
                schema: "util");

            migrationBuilder.DropTable(
                name: "TipoBeneficioRequisito");

            migrationBuilder.DropTable(
                name: "Beneficio");

            migrationBuilder.DropTable(
                name: "CuentaContable");

            migrationBuilder.DropTable(
                name: "Embargo");

            migrationBuilder.DropTable(
                name: "Profesion");

            migrationBuilder.DropTable(
                name: "VariableNomina");

            migrationBuilder.DropTable(
                name: "ActividadEconomica");

            migrationBuilder.DropTable(
                name: "Administradora");

            migrationBuilder.DropTable(
                name: "OperadorPago");

            migrationBuilder.DropTable(
                name: "TipoContribuyente");

            migrationBuilder.DropTable(
                name: "NivelEducativo");

            migrationBuilder.DropTable(
                name: "Parentesco");

            migrationBuilder.DropTable(
                name: "Libranza");

            migrationBuilder.DropTable(
                name: "NominaFuenteNovedad");

            migrationBuilder.DropTable(
                name: "NominaFuncionario");

            migrationBuilder.DropTable(
                name: "Notificacion");

            migrationBuilder.DropTable(
                name: "CategoriaParametro");

            migrationBuilder.DropTable(
                name: "AusentismoFuncionario");

            migrationBuilder.DropTable(
                name: "SolicitudVacaciones");

            migrationBuilder.DropTable(
                name: "SolicitudPermiso");

            migrationBuilder.DropTable(
                name: "TareaProgramada");

            migrationBuilder.DropTable(
                name: "TipoSoporte");

            migrationBuilder.DropTable(
                name: "TipoBeneficio");

            migrationBuilder.DropTable(
                name: "Juzgado");

            migrationBuilder.DropTable(
                name: "TipoEmbargo");

            migrationBuilder.DropTable(
                name: "TipoAdministradora");

            migrationBuilder.DropTable(
                name: "DiagnosticoCie");

            migrationBuilder.DropTable(
                name: "LibroVacaciones");

            migrationBuilder.DropTable(
                name: "Nomina");

            migrationBuilder.DropTable(
                name: "TipoAusentismo");

            migrationBuilder.DropTable(
                name: "ConceptoNomina");

            migrationBuilder.DropTable(
                name: "Contrato");

            migrationBuilder.DropTable(
                name: "PeriodoContable");

            migrationBuilder.DropTable(
                name: "SubPeriodo");

            migrationBuilder.DropTable(
                name: "TipoLiquidacion");

            migrationBuilder.DropTable(
                name: "ClaseAusentismo");

            migrationBuilder.DropTable(
                name: "FuncionNomina");

            migrationBuilder.DropTable(
                name: "CargoDependencia");

            migrationBuilder.DropTable(
                name: "CentroCosto");

            migrationBuilder.DropTable(
                name: "CentroOperativo");

            migrationBuilder.DropTable(
                name: "CentroTrabajo");

            migrationBuilder.DropTable(
                name: "EntidadFinanciera");

            migrationBuilder.DropTable(
                name: "FormaPago");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "GrupoNomina");

            migrationBuilder.DropTable(
                name: "JornadaLaboral");

            migrationBuilder.DropTable(
                name: "TipoContrato");

            migrationBuilder.DropTable(
                name: "TipoCuenta");

            migrationBuilder.DropTable(
                name: "TipoMoneda");

            migrationBuilder.DropTable(
                name: "TipoPeriodo");

            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropTable(
                name: "Dependencia");

            migrationBuilder.DropTable(
                name: "ClaseLibretaMilitar");

            migrationBuilder.DropTable(
                name: "DivisionPoliticaNivel2");

            migrationBuilder.DropTable(
                name: "EstadoCivil");

            migrationBuilder.DropTable(
                name: "LicenciaConduccion");

            migrationBuilder.DropTable(
                name: "Ocupacion");

            migrationBuilder.DropTable(
                name: "Sexo");

            migrationBuilder.DropTable(
                name: "TipoDocumento");

            migrationBuilder.DropTable(
                name: "TipoSangre");

            migrationBuilder.DropTable(
                name: "TipoVivienda");

            migrationBuilder.DropTable(
                name: "NivelCargo");

            migrationBuilder.DropTable(
                name: "DivisionPoliticaNivel1");

            migrationBuilder.DropTable(
                name: "Pais");
        }
    }
}
