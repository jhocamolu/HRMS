using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint13004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_HoraExtra_EstadoRegistro",
                table: "CargoPresupuesto");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_Tercero_EstadoRegistro",
                table: "Tercero",
                sql: "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_CargoPresupuesto_EstadoRegistro",
                table: "CargoPresupuesto",
                sql: "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");

            migrationBuilder.CreateTable(
                name: "DeduccionRetefuente",
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
                    Anio = table.Column<short>(type: "smallint", nullable: false),
                    InteresVivienda = table.Column<decimal>(type: "money", nullable: false),
                    MedicinaPrepagada = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeduccionRetefuente", x => x.Id);
                    table.CheckConstraint("CK_DeduccionRetefuente_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_DeduccionRetefuente_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeduccionRetefuente_FuncionarioId",
                table: "DeduccionRetefuente",
                column: "FuncionarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeduccionRetefuente");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Tercero_EstadoRegistro",
                table: "Tercero");

            migrationBuilder.DropCheckConstraint(
                name: "CK_CargoPresupuesto_EstadoRegistro",
                table: "CargoPresupuesto");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_HoraExtra_EstadoRegistro",
                table: "CargoPresupuesto",
                sql: "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
        }
    }
}
