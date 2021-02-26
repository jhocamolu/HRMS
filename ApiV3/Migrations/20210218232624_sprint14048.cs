using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14048 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ConceptoNominaId",
                table: "NominaContabilidad",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Cantidad",
                table: "FuncionarioCentroCosto",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "FormaRegistro",
                table: "FuncionarioCentroCosto",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormaRegistro",
                table: "FuncionarioCentroCosto");

            migrationBuilder.AlterColumn<int>(
                name: "ConceptoNominaId",
                table: "NominaContabilidad",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Cantidad",
                table: "FuncionarioCentroCosto",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
