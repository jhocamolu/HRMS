using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reclutamiento.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "reclutamiento");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            /*migrationBuilder.CreateTable(
                name: "Sexo",
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
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sexo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumento",
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
                    CodigoPila = table.Column<string>(type: "varchar(10)", nullable: false),
                    CodigoDian = table.Column<string>(type: "varchar(10)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false),
                    Formato = table.Column<string>(type: "varchar(255)", nullable: false),
                    Validacion = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumento", x => x.Id);
                });
                */
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "reclutamiento",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            /*migrationBuilder.CreateTable(
                name: "ClaseLibretaMilitar",
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
                    table.PrimaryKey("PK_ClaseLibretaMilitar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadoCivil",
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
                    table.PrimaryKey("PK_EstadoCivil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LicenciaConduccion",
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
                    Clase = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenciaConduccion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ocupacion",
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
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocupacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
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
                    Nacionalidad = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoSangre",
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
                    table.PrimaryKey("PK_TipoSangre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoVivienda",
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
                    table.PrimaryKey("PK_TipoVivienda", x => x.Id);
                });
                
            */migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "reclutamiento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "reclutamiento",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            
            /*migrationBuilder.CreateTable(
                name: "DivisionPoliticaNivel1",
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
                    PaisId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionPoliticaNivel1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DivisionPoliticaNivel1_Pais_PaisId",
                        column: x => x.PaisId,
                        principalSchema: "reclutamiento",
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DivisionPoliticaNivel2",
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
                    DivisionPoliticaNivel1Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionPoliticaNivel2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DivisionPoliticaNivel2_DivisionPoliticaNivel1_DivisionPoliticaNivel1Id",
                        column: x => x.DivisionPoliticaNivel1Id,
                        principalSchema: "reclutamiento",
                        principalTable: "DivisionPoliticaNivel1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HojaDeVida",
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
                    table.PrimaryKey("PK_HojaDeVida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HojaDeVida_ClaseLibretaMilitar_ClaseLibretaMilitarId",
                        column: x => x.ClaseLibretaMilitarId,
                        principalSchema: "reclutamiento",
                        principalTable: "ClaseLibretaMilitar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HojaDeVida_DivisionPoliticaNivel2_DivisionPoliticaNivel2ExpedicionDocumentoId",
                        column: x => x.DivisionPoliticaNivel2ExpedicionDocumentoId,
                        principalSchema: "reclutamiento",
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HojaDeVida_DivisionPoliticaNivel2_DivisionPoliticaNivel2OrigenId",
                        column: x => x.DivisionPoliticaNivel2OrigenId,
                        principalSchema: "reclutamiento",
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HojaDeVida_DivisionPoliticaNivel2_DivisionPoliticaNivel2ResidenciaId",
                        column: x => x.DivisionPoliticaNivel2ResidenciaId,
                        principalSchema: "reclutamiento",
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HojaDeVida_EstadoCivil_EstadoCivilId",
                        column: x => x.EstadoCivilId,
                        principalSchema: "reclutamiento",
                        principalTable: "EstadoCivil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HojaDeVida_LicenciaConduccion_LicenciaConduccionAId",
                        column: x => x.LicenciaConduccionAId,
                        principalSchema: "reclutamiento",
                        principalTable: "LicenciaConduccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HojaDeVida_LicenciaConduccion_LicenciaConduccionBId",
                        column: x => x.LicenciaConduccionBId,
                        principalSchema: "reclutamiento",
                        principalTable: "LicenciaConduccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HojaDeVida_LicenciaConduccion_LicenciaConduccionCId",
                        column: x => x.LicenciaConduccionCId,
                        principalSchema: "reclutamiento",
                        principalTable: "LicenciaConduccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HojaDeVida_Ocupacion_OcupacionId",
                        column: x => x.OcupacionId,
                        principalSchema: "reclutamiento",
                        principalTable: "Ocupacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HojaDeVida_Sexo_SexoId",
                        column: x => x.SexoId,
                        principalSchema: "dbo",
                        principalTable: "Sexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HojaDeVida_TipoDocumento_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalSchema: "dbo",
                        principalTable: "TipoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HojaDeVida_TipoSangre_TipoSangreId",
                        column: x => x.TipoSangreId,
                        principalSchema: "reclutamiento",
                        principalTable: "TipoSangre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HojaDeVida_TipoVivienda_TipoViviendaId",
                        column: x => x.TipoViviendaId,
                        principalSchema: "reclutamiento",
                        principalTable: "TipoVivienda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
                */
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "reclutamiento",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    HojaDeVidaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_HojaDeVida_HojaDeVidaId",
                        column: x => x.HojaDeVidaId,
                        principalSchema: "dbo",
                        principalTable: "HojaDeVida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "reclutamiento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "reclutamiento",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "reclutamiento",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "reclutamiento",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            
            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "reclutamiento",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "reclutamiento",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "reclutamiento",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            
            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "reclutamiento",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "reclutamiento",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            /*
            migrationBuilder.CreateIndex(
                name: "IX_HojaDeVida_ClaseLibretaMilitarId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "ClaseLibretaMilitarId");

            migrationBuilder.CreateIndex(
                name: "IX_HojaDeVida_DivisionPoliticaNivel2ExpedicionDocumentoId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "DivisionPoliticaNivel2ExpedicionDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_HojaDeVida_DivisionPoliticaNivel2OrigenId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "DivisionPoliticaNivel2OrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_HojaDeVida_DivisionPoliticaNivel2ResidenciaId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "DivisionPoliticaNivel2ResidenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_HojaDeVida_EstadoCivilId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "EstadoCivilId");

            migrationBuilder.CreateIndex(
                name: "IX_HojaDeVida_LicenciaConduccionAId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "LicenciaConduccionAId");

            migrationBuilder.CreateIndex(
                name: "IX_HojaDeVida_LicenciaConduccionBId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "LicenciaConduccionBId");

            migrationBuilder.CreateIndex(
                name: "IX_HojaDeVida_LicenciaConduccionCId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "LicenciaConduccionCId");

            migrationBuilder.CreateIndex(
                name: "IX_HojaDeVida_OcupacionId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "OcupacionId");

            migrationBuilder.CreateIndex(
                name: "IX_HojaDeVida_SexoId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "SexoId");

            migrationBuilder.CreateIndex(
                name: "IX_HojaDeVida_TipoDocumentoId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_HojaDeVida_TipoSangreId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "TipoSangreId");

            migrationBuilder.CreateIndex(
                name: "IX_HojaDeVida_TipoViviendaId",
                schema: "dbo",
                table: "HojaDeVida",
                column: "TipoViviendaId");

            */migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "reclutamiento",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "reclutamiento",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");
              

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "reclutamiento",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "reclutamiento",
                table: "AspNetUserLogins",
                column: "UserId");
            
            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "reclutamiento",
                table: "AspNetUserRoles",
                column: "RoleId");
            
            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HojaDeVidaId",
                schema: "reclutamiento",
                table: "AspNetUsers",
                column: "HojaDeVidaId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "reclutamiento",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "reclutamiento",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            /*migrationBuilder.CreateIndex(
                name: "IX_DivisionPoliticaNivel1_PaisId",
                schema: "reclutamiento",
                table: "DivisionPoliticaNivel1",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionPoliticaNivel2_DivisionPoliticaNivel1Id",
                schema: "reclutamiento",
                table: "DivisionPoliticaNivel2",
                column: "DivisionPoliticaNivel1Id");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "reclutamiento");
                
            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "reclutamiento");
            
            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "reclutamiento");
            
            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "reclutamiento");
            
            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "reclutamiento");
            
            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "reclutamiento");

            /*migrationBuilder.DropTable(
                name: "HojaDeVida",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ClaseLibretaMilitar",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "DivisionPoliticaNivel2",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "EstadoCivil",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "LicenciaConduccion",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "Ocupacion",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "Sexo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TipoDocumento",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TipoSangre",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "TipoVivienda",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "DivisionPoliticaNivel1",
                schema: "reclutamiento");

            migrationBuilder.DropTable(
                name: "Pais",
                schema: "reclutamiento");*/
        }
    }
}
