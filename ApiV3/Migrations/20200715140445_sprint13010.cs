using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint13010 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateCheckConstraint(
                name: "CK_NominaDetalle_UnidadMedida",
                table: "NominaDetalle",
                sql: "([UnidadMedida]='Horas' OR [UnidadMedida]='Dias' OR [UnidadMedida]='Unidad' OR [UnidadMedida]='Porcentaje')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_NominaDetalle_UnidadMedida",
                table: "NominaDetalle");
        }
    }
}
