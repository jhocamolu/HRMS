using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14027 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateCheckConstraint(
                name: "CK_TipoLiquidacionModulo_Modulo",
                table: "TipoLiquidacionModulo",
                sql: "([Modulo]='Libranzas' or [Modulo]='Embargos' or [Modulo]='Ausentismos' or [Modulo]='Beneficios' or [Modulo]='HorasExtra' or [Modulo]='HorasExtra' or [Modulo]='GastosViaje' or [Modulo]='OtrasNovedades' or [Modulo]='Vacaciones' or [Modulo]='AnticipoCesantia')");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_TipoLiquidacion_OperacionTotal",
                table: "TipoLiquidacion",
                sql: "([OperacionTotal]='TotalDevengosMenosTotalDeducciones' or [OperacionTotal]='TotalCalculos' or [OperacionTotal]='SoloCalculosSinAgrupar' or [OperacionTotal]='TotalDeducciones')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_TipoLiquidacionModulo_Modulo",
                table: "TipoLiquidacionModulo");

            migrationBuilder.DropCheckConstraint(
                name: "CK_TipoLiquidacion_OperacionTotal",
                table: "TipoLiquidacion");
        }
    }
}
