using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint13033 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoGastoViaje",
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
                    Tipo = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoGastoViaje", x => x.Id);
                    table.CheckConstraint("CK_TipoGastoViaje_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_TipoGastoViaje_Estado", "([Tipo]='ViaticosHospedaje' OR [Tipo]='ViaticosAlimentacion' OR [Tipo]='FaltanteViaticos' OR [Tipo]='PagoAnticipoGV')");
                    table.ForeignKey(
                        name: "FK_TipoGastoViaje_ConceptoNomina_ConceptoNominaId",
                        column: x => x.ConceptoNominaId,
                        principalTable: "ConceptoNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GastoViaje",
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
                    TipoGastoViajeId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<decimal>(type: "money", nullable: false),
                    Estado = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GastoViaje", x => x.Id);
                    table.CheckConstraint("CK_GastoViaje_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_GastoViaje_Estado", "([Estado]='Pendiente' OR [Estado]='Aplicada')");
                    table.ForeignKey(
                        name: "FK_GastoViaje_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GastoViaje_TipoGastoViaje_TipoGastoViajeId",
                        column: x => x.TipoGastoViajeId,
                        principalTable: "TipoGastoViaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GastoViaje_FuncionarioId",
                table: "GastoViaje",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_GastoViaje_TipoGastoViajeId",
                table: "GastoViaje",
                column: "TipoGastoViajeId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoGastoViaje_ConceptoNominaId",
                table: "TipoGastoViaje",
                column: "ConceptoNominaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GastoViaje");

            migrationBuilder.DropTable(
                name: "TipoGastoViaje");
        }
    }
}
