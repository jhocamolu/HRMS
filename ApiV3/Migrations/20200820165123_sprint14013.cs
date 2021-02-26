using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14013 : Migration
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
        }
    }
}
