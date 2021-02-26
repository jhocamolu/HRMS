using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint13021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Novedad",
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
                    CategoriaNovedadId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    FechaFinalizacion = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    TipoPeriodoId = table.Column<int>(nullable: false),
                    Unidad = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(type: "money", nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    TerceroId = table.Column<int>(nullable: false),
                    Observacion = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Novedad", x => x.Id);
                    table.CheckConstraint("CK_Novedad_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_Novedad_Estado", "([Estado]='EnCurso' OR [Estado]='Pendiente' OR [Estado]='Liquidada' OR [Estado]='Anulada' OR [Estado]='Cancelada')");
                    table.ForeignKey(
                        name: "FK_Novedad_CategoriaNovedad_CategoriaNovedadId",
                        column: x => x.CategoriaNovedadId,
                        principalTable: "CategoriaNovedad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Novedad_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NovedadSubperiodo",
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
                    NovedadId = table.Column<int>(nullable: false),
                    SubperiodoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NovedadSubperiodo", x => x.Id);
                    table.CheckConstraint("CK_NovedadSubperiodo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_NovedadSubperiodo_Novedad_NovedadId",
                        column: x => x.NovedadId,
                        principalTable: "Novedad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NovedadSubperiodo_SubPeriodo_SubperiodoId",
                        column: x => x.SubperiodoId,
                        principalTable: "SubPeriodo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Novedad_CategoriaNovedadId",
                table: "Novedad",
                column: "CategoriaNovedadId");

            migrationBuilder.CreateIndex(
                name: "IX_Novedad_FuncionarioId",
                table: "Novedad",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_NovedadSubperiodo_NovedadId",
                table: "NovedadSubperiodo",
                column: "NovedadId");

            migrationBuilder.CreateIndex(
                name: "IX_NovedadSubperiodo_SubperiodoId",
                table: "NovedadSubperiodo",
                column: "SubperiodoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NovedadSubperiodo");

            migrationBuilder.DropTable(
                name: "Novedad");
        }
    }
}
