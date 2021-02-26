using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Candidato_Estado",
                table: "Candidato");

            migrationBuilder.RenameTable(
                name: "TipoVivienda",
                newName: "TipoVivienda",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "TipoSoporte",
                newName: "TipoSoporte",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "TipoSangre",
                newName: "TipoSangre",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Profesion",
                newName: "Profesion",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Pais",
                newName: "Pais",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Ocupacion",
                newName: "Ocupacion",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "NivelEducativo",
                newName: "NivelEducativo",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "LicenciaConduccion",
                newName: "LicenciaConduccion",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "HojaDeVidaExperienciaLaboral",
                newName: "HojaDeVidaExperienciaLaboral",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "HojaDeVidaEstudio",
                newName: "HojaDeVidaEstudio",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "HojaDeVidaDocumento",
                newName: "HojaDeVidaDocumento",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "EstadoCivil",
                newName: "EstadoCivil",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "DivisionPoliticaNivel2",
                newName: "DivisionPoliticaNivel2",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ClaseLibretaMilitar",
                newName: "ClaseLibretaMilitar",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Candidato",
                newName: "Candidato",
                newSchema: "dbo");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_Candidato_Estado",
                schema: "dbo",
                table: "Candidato",
                sql: "([Estado]='Postulado' OR [Estado]='Descartado' OR [Estado]='Competente' OR [Estado]='Elegible' OR [Estado]='NoApto' OR [Estado]='Seleccionado' OR [Estado]='Reprobado' OR [Estado]='Anulado')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Candidato_Estado",
                schema: "dbo",
                table: "Candidato");

            migrationBuilder.RenameTable(
                name: "TipoVivienda",
                schema: "dbo",
                newName: "TipoVivienda");

            migrationBuilder.RenameTable(
                name: "TipoSoporte",
                schema: "dbo",
                newName: "TipoSoporte");

            migrationBuilder.RenameTable(
                name: "TipoSangre",
                schema: "dbo",
                newName: "TipoSangre");

            migrationBuilder.RenameTable(
                name: "Profesion",
                schema: "dbo",
                newName: "Profesion");

            migrationBuilder.RenameTable(
                name: "Pais",
                schema: "dbo",
                newName: "Pais");

            migrationBuilder.RenameTable(
                name: "Ocupacion",
                schema: "dbo",
                newName: "Ocupacion");

            migrationBuilder.RenameTable(
                name: "NivelEducativo",
                schema: "dbo",
                newName: "NivelEducativo");

            migrationBuilder.RenameTable(
                name: "LicenciaConduccion",
                schema: "dbo",
                newName: "LicenciaConduccion");

            migrationBuilder.RenameTable(
                name: "HojaDeVidaExperienciaLaboral",
                schema: "dbo",
                newName: "HojaDeVidaExperienciaLaboral");

            migrationBuilder.RenameTable(
                name: "HojaDeVidaEstudio",
                schema: "dbo",
                newName: "HojaDeVidaEstudio");

            migrationBuilder.RenameTable(
                name: "HojaDeVidaDocumento",
                schema: "dbo",
                newName: "HojaDeVidaDocumento");

            migrationBuilder.RenameTable(
                name: "EstadoCivil",
                schema: "dbo",
                newName: "EstadoCivil");

            migrationBuilder.RenameTable(
                name: "DivisionPoliticaNivel2",
                schema: "dbo",
                newName: "DivisionPoliticaNivel2");

            migrationBuilder.RenameTable(
                name: "ClaseLibretaMilitar",
                schema: "dbo",
                newName: "ClaseLibretaMilitar");

            migrationBuilder.RenameTable(
                name: "Candidato",
                schema: "dbo",
                newName: "Candidato");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_Candidato_Estado",
                table: "Candidato",
                sql: "([Estado]='Postulado' OR [Estado]='Descartado' OR [Estado]='Competente' OR [Estado]='Elegible' OR [Estado]='NoApto' OR [Estado]='Seleccionado' OR [Estado]='Reprobado')");
        }
    }
}
