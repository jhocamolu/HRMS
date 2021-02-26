using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14011 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Candidato_Estado",
                table: "Candidato");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_Candidato_Estado",
                table: "Candidato",
                sql: "([Estado]='Postulado' OR [Estado]='Descartado' OR [Estado]='Competente' OR [Estado]='Elegible' OR [Estado]='NoApto' OR [Estado]='Seleccionado' OR [Estado]='Reprobado')");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Candidato",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Candidato_Estado",
                table: "Candidato");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_Candidato_Estado",
                table: "Candidato",
                sql: "([Estado]='Postulado' OR [Estado]='Descartado' OR [Estado]='Competente' OR [Estado]='Elegible' OR [Estado]='NoApto' OR [Estado]='Seleccionado')");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Candidato",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");
        }
    }
}
