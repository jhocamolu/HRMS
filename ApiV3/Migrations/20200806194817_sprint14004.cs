using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_TipoContrato_Clase",
                table: "TipoContrato");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_TipoContrato_Clase",
                table: "TipoContrato",
                sql: "([Clase]='NoIntegral' OR [Clase]='Integral' OR [Clase]='Aprendizaje' OR [Clase]='Practicante' OR [Clase]='Variable')");

            migrationBuilder.AddColumn<string>(
                name: "DocumentoSlug",
                table: "TipoContrato",
                type: "varchar(255)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_TipoContrato_Clase",
                table: "TipoContrato");

            migrationBuilder.DropColumn(
                name: "DocumentoSlug",
                table: "TipoContrato");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_TipoContrato_Clase",
                table: "TipoContrato",
                sql: "([Clase]='NoIntegral' OR [Clase]='Integral' OR [Clase]='Aprendizaje' OR [Clase]='Practicante')");
        }
    }
}
