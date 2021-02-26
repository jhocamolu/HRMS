using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14037 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_TipoGastoViaje_Estado",
                table: "TipoGastoViaje");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_TipoGastoViaje_Estado",
                table: "TipoGastoViaje",
                sql: "([Tipo]='ViaticosHospedaje' OR [Tipo]='ViaticosAlimentacion' OR [Tipo]='FaltanteViaticos' OR [Tipo]='PagoAnticipoGV' OR [Tipo]='BaseViaticosAlimentacion' OR [Tipo]='BaseViaticosRetefuente' OR [Tipo]='BaseRetefuenteGV' OR [Tipo]='BaseViaticosHospedaje')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_TipoGastoViaje_Estado",
                table: "TipoGastoViaje");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_TipoGastoViaje_Estado",
                table: "TipoGastoViaje",
                sql: "([Tipo]='ViaticosHospedaje' OR [Tipo]='ViaticosAlimentacion' OR [Tipo]='FaltanteViaticos' OR [Tipo]='PagoAnticipoGV')");
        }
    }
}
