using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14042 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_TipoLiquidacionComprobante_TipoComprobante",
                table: "TipoLiquidacionComprobante");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_TipoLiquidacionComprobante_TipoComprobante",
                table: "TipoLiquidacionComprobante",
                sql: "([TipoComprobante]='Contabilizacion' OR [TipoComprobante]='Transferencia' OR [TipoComprobante]='Reversion' )");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_TipoLiquidacionComprobante_TipoComprobante",
                table: "TipoLiquidacionComprobante");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_TipoLiquidacionComprobante_TipoComprobante",
                table: "TipoLiquidacionComprobante",
                sql: "([TipoComprobante]='Contabilizacion' OR [TipoComprobante]='Transferencia' OR [TipoComprobante]='Reversión' )");
        }
    }
}
