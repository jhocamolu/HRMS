using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14008 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_SolicitudVacaciones_Estado",
                table: "SolicitudVacaciones");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_SolicitudVacaciones_Estado",
                table: "SolicitudVacaciones",
                sql: "([Estado]='Aprobada' OR [Estado]='Autorizada' OR [Estado]='Cancelada' OR [Estado]='EnCurso' OR [Estado]='Interrumpida' OR [Estado]='Rechazada' OR [Estado]='Solicitada'  OR [Estado]='Terminada' OR [Estado]='Anulada')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_SolicitudVacaciones_Estado",
                table: "SolicitudVacaciones");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_SolicitudVacaciones_Estado",
                table: "SolicitudVacaciones",
                sql: "([Estado]='Aprobada' OR [Estado]='Autorizada' OR [Estado]='Cancelada' OR [Estado]='EnCurso' OR [Estado]='Interrumpida' OR [Estado]='Rechazada' OR [Estado]='Solicitada'  OR [Estado]='Terminada')");
        }
    }
}
