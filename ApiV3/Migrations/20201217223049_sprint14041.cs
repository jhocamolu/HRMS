using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14041 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoLiquidacionComprobante",
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
                    TipoLiquidacionId = table.Column<int>(nullable: false),
                    TipoComprobante = table.Column<string>(type: "varchar(255)", nullable: false),
                    CentroCostoId = table.Column<int>(nullable: false),
                    CuentaContableId = table.Column<int>(nullable: false),
                    Naturaleza = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoLiquidacionComprobante", x => x.Id);
                    table.CheckConstraint("CK_TipoLiquidacionComprobante_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_TipoLiquidacionComprobante_TipoComprobante", "([TipoComprobante]='Contabilizacion' OR [TipoComprobante]='Transferencia' OR [TipoComprobante]='Reversión' )");
                    table.CheckConstraint("CK_TipoLiquidacionComprobante_Naturaleza", "([Naturaleza]='Debito' OR [Naturaleza]='Credito' )");
                    table.ForeignKey(
                        name: "FK_TipoLiquidacionComprobante_CentroCosto_CentroCostoId",
                        column: x => x.CentroCostoId,
                        principalTable: "CentroCosto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TipoLiquidacionComprobante_CuentaContable_CuentaContableId",
                        column: x => x.CuentaContableId,
                        principalTable: "CuentaContable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TipoLiquidacionComprobante_TipoLiquidacion_TipoLiquidacionId",
                        column: x => x.TipoLiquidacionId,
                        principalTable: "TipoLiquidacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TipoLiquidacionComprobante_CentroCostoId",
                table: "TipoLiquidacionComprobante",
                column: "CentroCostoId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoLiquidacionComprobante_CuentaContableId",
                table: "TipoLiquidacionComprobante",
                column: "CuentaContableId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoLiquidacionComprobante_TipoLiquidacionId",
                table: "TipoLiquidacionComprobante",
                column: "TipoLiquidacionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoLiquidacionComprobante");
        }
    }
}
