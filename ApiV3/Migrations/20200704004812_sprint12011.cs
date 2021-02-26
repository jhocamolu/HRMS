using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint12011 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateCheckConstraint(
                name: "CK_CategoriaNovedad_UbicacionTercero",
                table: "CategoriaNovedad",
                sql: "([UbicacionTercero]='EntidadFinanciera' OR [UbicacionTercero]='Administradora' OR [UbicacionTercero]='OtrosTerceros' )");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "CategoriaNovedad",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_CategoriaNovedad_UbicacionTercero",
                table: "CategoriaNovedad");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "CategoriaNovedad");
        }
    }
}
