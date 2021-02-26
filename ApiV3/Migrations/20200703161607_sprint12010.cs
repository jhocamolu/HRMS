using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint12010 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_TipoHoraExtra_Tipo",
                table: "TipoHoraExtra");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_TipoHoraExtra_Tipo",
                table: "TipoHoraExtra",
                sql: "([Tipo]='RecargoNocturno' OR [Tipo]='HoraExtraDiurna' OR [Tipo]='HoraExtraNocturna' OR [Tipo]='HoraExtraFestivaDominicalDiurna' OR [Tipo]='HoraExtraFestivaDominicalNocturna'  OR [Tipo]='RecargoNocturnoDominicalFestivo' OR [Tipo]='DominicalFestivoCompensado' OR [Tipo]='DominicalFestivoNoCompensado')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_TipoHoraExtra_Tipo",
                table: "TipoHoraExtra");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_TipoHoraExtra_Tipo",
                table: "TipoHoraExtra",
                sql: "([Tipo]='RecargoNocturno' OR [Tipo]='HoraExtraDiurna' OR [Tipo]='HoraExtraNocturna' OR [Tipo]='HoraExtraFestivaDominicalDiurna' OR [Tipo]='HoraExtraFestivaDominicalNocturna'  OR [Tipo]='RecargoNocturnoDominicalFestivo' OR [Tipo]=' DominicalFestivoCompensado' OR [Tipo]='DominicalFestivoNoCompensado')");
        }
    }
}
