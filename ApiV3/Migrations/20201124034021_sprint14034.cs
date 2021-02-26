using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14034 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActividadFuncionario",
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
                    MunicipioId = table.Column<int>(nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFinalizacion = table.Column<DateTime>(type: "date", nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Estado = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActividadFuncionario", x => x.Id);
                    table.CheckConstraint("CK_ActividadFuncionario_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_ActividadFuncionario_Estado", "([Estado]='Pendiente' OR [Estado]='Aplicado' )");
                    table.ForeignKey(
                        name: "FK_ActividadFuncionario_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActividadFuncionario_DivisionPoliticaNivel2_MunicipioId",
                        column: x => x.MunicipioId,
                        principalSchema: "dbo",
                        principalTable: "DivisionPoliticaNivel2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FuncionarioCentroCosto",
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
                    CentroCostoId = table.Column<int>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Ponderado = table.Column<decimal>(type: "decimal(16,6)", nullable: true),
                    Porcentaje = table.Column<decimal>(type: "decimal(16,6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionarioCentroCosto", x => x.Id);
                    table.CheckConstraint("CK_FuncionarioCentroCosto_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_FuncionarioCentroCosto_CentroCosto_CentroCostoId",
                        column: x => x.CentroCostoId,
                        principalTable: "CentroCosto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FuncionarioCentroCosto_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActividadFuncionarioCentroCosto",
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
                    ActividadFuncionarioId = table.Column<int>(nullable: false),
                    FuncionarioCentroCostoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActividadFuncionarioCentroCosto", x => x.Id);
                    table.CheckConstraint("CK_ActividadFuncionarioCentroCosto_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_ActividadFuncionarioCentroCosto_ActividadFuncionario_ActividadFuncionarioId",
                        column: x => x.ActividadFuncionarioId,
                        principalTable: "ActividadFuncionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActividadFuncionarioCentroCosto_FuncionarioCentroCosto_FuncionarioCentroCostoId",
                        column: x => x.FuncionarioCentroCostoId,
                        principalTable: "FuncionarioCentroCosto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActividadFuncionario_FuncionarioId",
                table: "ActividadFuncionario",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ActividadFuncionario_MunicipioId",
                table: "ActividadFuncionario",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_ActividadFuncionarioCentroCosto_ActividadFuncionarioId",
                table: "ActividadFuncionarioCentroCosto",
                column: "ActividadFuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ActividadFuncionarioCentroCosto_FuncionarioCentroCostoId",
                table: "ActividadFuncionarioCentroCosto",
                column: "FuncionarioCentroCostoId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioCentroCosto_CentroCostoId",
                table: "FuncionarioCentroCosto",
                column: "CentroCostoId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioCentroCosto_FuncionarioId",
                table: "FuncionarioCentroCosto",
                column: "FuncionarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActividadFuncionarioCentroCosto");

            migrationBuilder.DropTable(
                name: "ActividadFuncionario");

            migrationBuilder.DropTable(
                name: "FuncionarioCentroCosto");
        }
    }
}
