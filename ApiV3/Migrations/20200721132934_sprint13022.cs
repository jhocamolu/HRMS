using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint13022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClaseAportanteTipoAportante",
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
                    table.CheckConstraint("CK_ClaseAportanteTipoAportante_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_ClaseAportanteTipoAportante_ClaseAportante_ClaseAportanteId",
                        column: x => x.ClaseAportanteId,
                        principalTable: "ClaseAportante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClaseAportanteTipoAportante_TipoAportante_TipoAportanteId",
                        column: x => x.TipoAportanteId,
                        principalTable: "TipoAportante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NaturalezaJuridica",
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
                    table.CheckConstraint("CK_NaturalezaJuridica_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "TipoAccion",
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
                    table.PrimaryKey("PK_TipoAccion", x => x.Id);
                    table.CheckConstraint("CK_TipoAccion_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "TipoCotizanteTipoPlanilla",
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
                    TipoCotizanteId = table.Column<int>(nullable: false),
                    TipoPlanillaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCotizanteTipoPlanilla", x => x.Id);
                    table.CheckConstraint("CK_TipoCotizanteTipoPlanilla_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_TipoCotizanteTipoPlanilla_TipoCotizante_TipoCotizanteId",
                        column: x => x.TipoCotizanteId,
                        principalTable: "TipoCotizante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TipoCotizanteTipoPlanilla_TipoPlanilla_TipoPlanillaId",
                        column: x => x.TipoPlanillaId,
                        principalTable: "TipoPlanilla",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaseAportanteTipoAportante_ClaseAportanteId",
                table: "ClaseAportanteTipoAportante",
                column: "ClaseAportanteId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaseAportanteTipoAportante_TipoAportanteId",
                table: "ClaseAportanteTipoAportante",
                column: "TipoAportanteId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoCotizanteTipoPlanilla_TipoCotizanteId",
                table: "TipoCotizanteTipoPlanilla",
                column: "TipoCotizanteId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoCotizanteTipoPlanilla_TipoPlanillaId",
                table: "TipoCotizanteTipoPlanilla",
                column: "TipoPlanillaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaseAportanteTipoAportante");

            migrationBuilder.DropTable(
                name: "NaturalezaJuridica");

            migrationBuilder.DropTable(
                name: "TipoAccion");

            migrationBuilder.DropTable(
                name: "TipoCotizanteTipoPlanilla");
        }
    }
}
