using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint10012 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actividad",
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
                    PromedioProductividad = table.Column<int>(nullable: false),
                    ValorComplejidad = table.Column<decimal>(type: "decimal(19, 6)", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividad", x => x.Id);
                    table.CheckConstraint("CK_Actividad_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "ActividadCentroCosto",
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
                    ActividadId = table.Column<int>(nullable: false),
                    CentroCostoId = table.Column<int>(nullable: false),
                    MunicipioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActividadCentroCosto", x => x.Id);
                    table.CheckConstraint("CK_ActividadCentroCosto_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_ActividadCentroCosto_Actividad_ActividadId",
                        column: x => x.ActividadId,
                        principalTable: "Actividad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActividadCentroCosto_CentroCosto_CentroCostoId",
                        column: x => x.CentroCostoId,
                        principalTable: "CentroCosto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActividadCentroCosto_DivisionPoliticaNivel2_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActividadCentroCosto_ActividadId",
                table: "ActividadCentroCosto",
                column: "ActividadId");

            migrationBuilder.CreateIndex(
                name: "IX_ActividadCentroCosto_CentroCostoId",
                table: "ActividadCentroCosto",
                column: "CentroCostoId");

            migrationBuilder.CreateIndex(
                name: "IX_ActividadCentroCosto_MunicipioId",
                table: "ActividadCentroCosto",
                column: "MunicipioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActividadCentroCosto");

            migrationBuilder.DropTable(
                name: "Actividad");
        }
    }
}
