using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint12012 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumerosProrroga",
                table: "ContratoOtroSi",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ConceptoNominaId",
                table: "CategoriaNovedad",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumerosProrroga",
                table: "ContratoOtroSi");

            migrationBuilder.AlterColumn<int>(
                name: "ConceptoNominaId",
                table: "CategoriaNovedad",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
