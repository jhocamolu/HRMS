using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint13013 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_RequisicionPersonal_Estado",
                table: "RequisicionPersonal");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_RequisicionPersonal_Estado",
                table: "RequisicionPersonal",
                sql: "([estado]='Anulada' OR [estado]='Aprobada' OR [estado]='Autorizada' OR [estado]='Cancelada' OR [estado]='Cubierta' OR [estado]='Rechazada' OR [estado]='Revisada' OR [estado]='Solicitada')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_RequisicionPersonal_Estado",
                table: "RequisicionPersonal");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_RequisicionPersonal_Estado",
                table: "RequisicionPersonal",
                sql: "([estado]='Anulada' OR [estado]='Aprobada' OR [estado]='Autorizacion' OR [estado]='Cancelada' OR [estado]='Cubierta' OR [estado]='Rechazada' OR [estado]='Revisada' OR [estado]='Solicitada')");
        }
    }
}
