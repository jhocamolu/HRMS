using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint14019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EquivalenteBancario",
                schema: "dbo",
                table: "TipoDocumento",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "TipoCuenta",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EntidadPorDefecto",
                table: "EntidadFinanciera",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CuentaBancaria",
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
                    EntidadFinancieraId = table.Column<int>(nullable: false),
                    TipoCuentaId = table.Column<int>(nullable: false),
                    Numero = table.Column<string>(type: "varchar(255)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentaBancaria", x => x.Id);
                    table.CheckConstraint("CK_CuentaBancaria_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_CuentaBancaria_EntidadFinanciera_EntidadFinancieraId",
                        column: x => x.EntidadFinancieraId,
                        principalTable: "EntidadFinanciera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CuentaBancaria_TipoCuenta_TipoCuentaId",
                        column: x => x.TipoCuentaId,
                        principalTable: "TipoCuenta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TipoDocumento_EquivalenteBancario",
                schema: "dbo",
                table: "TipoDocumento",
                column: "EquivalenteBancario",
                unique: true,
                filter: "[EquivalenteBancario] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TipoCuenta_Codigo",
                table: "TipoCuenta",
                column: "Codigo",
                unique: true,
                filter: "[Codigo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CuentaBancaria_EntidadFinancieraId",
                table: "CuentaBancaria",
                column: "EntidadFinancieraId");

            migrationBuilder.CreateIndex(
                name: "IX_CuentaBancaria_Nombre",
                table: "CuentaBancaria",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CuentaBancaria_Numero",
                table: "CuentaBancaria",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CuentaBancaria_TipoCuentaId",
                table: "CuentaBancaria",
                column: "TipoCuentaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CuentaBancaria");

            migrationBuilder.DropIndex(
                name: "IX_TipoDocumento_EquivalenteBancario",
                schema: "dbo",
                table: "TipoDocumento");

            migrationBuilder.DropIndex(
                name: "IX_TipoCuenta_Codigo",
                table: "TipoCuenta");

            migrationBuilder.DropColumn(
                name: "EquivalenteBancario",
                schema: "dbo",
                table: "TipoDocumento");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "TipoCuenta");

            migrationBuilder.DropColumn(
                name: "EntidadPorDefecto",
                table: "EntidadFinanciera");
        }
    }
}
