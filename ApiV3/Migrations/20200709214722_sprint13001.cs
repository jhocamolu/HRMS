using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint13001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumerosProrroga",
                table: "ContratoOtroSi");

            migrationBuilder.AddColumn<int>(
                name: "NumeroProrroga",
                table: "ContratoOtroSi",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HoraExtra",
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
                    TipoHoraExtraId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Cantidad = table.Column<string>(type: "varchar(255)", nullable: false),
                    Valor = table.Column<decimal>(type: "money", nullable: false),
                    FormaRegistro = table.Column<string>(type: "varchar(255)", nullable: false),
                    Estado = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoraExtra", x => x.Id);
                    table.CheckConstraint("CK_HoraExtra_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_HoraExtra_FormaRegistro", "([FormaRegistro]='Manual' OR [FormaRegistro]='Automatico' )");
                    table.CheckConstraint("CK_HoraExtra_Estado", "([Estado]='Pendiente' OR [Estado]='Aplicada')");
                    table.ForeignKey(
                        name: "FK_HoraExtra_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoraExtra_TipoHoraExtra_TipoHoraExtraId",
                        column: x => x.TipoHoraExtraId,
                        principalTable: "TipoHoraExtra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HoraExtra_FuncionarioId",
                table: "HoraExtra",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_HoraExtra_TipoHoraExtraId",
                table: "HoraExtra",
                column: "TipoHoraExtraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoraExtra");

            migrationBuilder.DropColumn(
                name: "NumeroProrroga",
                table: "ContratoOtroSi");

            migrationBuilder.AddColumn<int>(
                name: "NumerosProrroga",
                table: "ContratoOtroSi",
                type: "int",
                nullable: true);
        }
    }
}
