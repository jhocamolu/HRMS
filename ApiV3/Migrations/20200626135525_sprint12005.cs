using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint12005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tercero",
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
                    DigitoVerificacion = table.Column<short>(type: "smallint", nullable: false),
                    DivisionPoliticaNivel1Id = table.Column<int>(nullable: false),
                    Telefono = table.Column<string>(type: "varchar(255)", nullable: false),
                    Direccion = table.Column<string>(type: "varchar(255)", nullable: false),
                    EntidadFinancieraId = table.Column<int>(nullable: false),
                    TipoCuentaId = table.Column<int>(nullable: false),
                    NumeroCuenta = table.Column<string>(type: "varchar(255)", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tercero", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tercero_DivisionPoliticaNivel1_DivisionPoliticaNivel1Id",
                        column: x => x.DivisionPoliticaNivel1Id,
                        principalTable: "DivisionPoliticaNivel1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tercero_EntidadFinanciera_EntidadFinancieraId",
                        column: x => x.EntidadFinancieraId,
                        principalTable: "EntidadFinanciera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tercero_TipoCuenta_TipoCuentaId",
                        column: x => x.TipoCuentaId,
                        principalTable: "TipoCuenta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tercero_DivisionPoliticaNivel1Id",
                table: "Tercero",
                column: "DivisionPoliticaNivel1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tercero_EntidadFinancieraId",
                table: "Tercero",
                column: "EntidadFinancieraId");

            migrationBuilder.CreateIndex(
                name: "IX_Tercero_TipoCuentaId",
                table: "Tercero",
                column: "TipoCuentaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tercero");
        }
    }
}
