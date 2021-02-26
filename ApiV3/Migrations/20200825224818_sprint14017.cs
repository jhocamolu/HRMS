using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14017 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CorreoEletronico",
                schema: "dbo",
                table: "NotificacionDestinatario",
                newName: "CorreoElectronico");

            migrationBuilder.RenameColumn(
                name: "CorreoEletronico",
                table: "NotificacionDestinatarioLog",
                newName: "CorreoElectronico");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CorreoElectronico",
                schema: "dbo",
                table: "NotificacionDestinatario",
                newName: "CorreoEletronico");

            migrationBuilder.RenameColumn(
                name: "CorreoElectronico",
                table: "NotificacionDestinatarioLog",
                newName: "CorreoEletronico");
        }
    }
}
