using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14016 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificacionDestinatario_Funcionario_FuncionarioId",
                table: "NotificacionDestinatario");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificacionDestinatarioLog_Funcionario_FuncionarioId",
                table: "NotificacionDestinatarioLog");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "TipoDocumento",
                newName: "TipoDocumento",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "TareaProgramada",
                newName: "TareaProgramada",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Sexo",
                newName: "Sexo",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "RequisicionPersonal",
                newName: "RequisicionPersonal",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "NotificacionPlantilla",
                newName: "NotificacionPlantilla",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "NotificacionDestinatario",
                newName: "NotificacionDestinatario",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Notificacion",
                newName: "Notificacion",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "InformacionBasica",
                newName: "InformacionBasica",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "HojaDeVida",
                newName: "HojaDeVida",
                newSchema: "dbo");

            migrationBuilder.AlterColumn<int>(
                name: "FuncionarioId",
                table: "NotificacionDestinatarioLog",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CorreoEletronico",
                table: "NotificacionDestinatarioLog",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FuncionarioId",
                schema: "dbo",
                table: "NotificacionDestinatario",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CorreoEletronico",
                schema: "dbo",
                table: "NotificacionDestinatario",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificacionDestinatarioLog_Funcionario_FuncionarioId",
                table: "NotificacionDestinatarioLog",
                column: "FuncionarioId",
                principalTable: "Funcionario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificacionDestinatario_Funcionario_FuncionarioId",
                schema: "dbo",
                table: "NotificacionDestinatario",
                column: "FuncionarioId",
                principalTable: "Funcionario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificacionDestinatarioLog_Funcionario_FuncionarioId",
                table: "NotificacionDestinatarioLog");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificacionDestinatario_Funcionario_FuncionarioId",
                schema: "dbo",
                table: "NotificacionDestinatario");

            migrationBuilder.DropColumn(
                name: "CorreoEletronico",
                table: "NotificacionDestinatarioLog");

            migrationBuilder.DropColumn(
                name: "CorreoEletronico",
                schema: "dbo",
                table: "NotificacionDestinatario");

            migrationBuilder.RenameTable(
                name: "TipoDocumento",
                schema: "dbo",
                newName: "TipoDocumento");

            migrationBuilder.RenameTable(
                name: "TareaProgramada",
                schema: "dbo",
                newName: "TareaProgramada");

            migrationBuilder.RenameTable(
                name: "Sexo",
                schema: "dbo",
                newName: "Sexo");

            migrationBuilder.RenameTable(
                name: "RequisicionPersonal",
                schema: "dbo",
                newName: "RequisicionPersonal");

            migrationBuilder.RenameTable(
                name: "NotificacionPlantilla",
                schema: "dbo",
                newName: "NotificacionPlantilla");

            migrationBuilder.RenameTable(
                name: "NotificacionDestinatario",
                schema: "dbo",
                newName: "NotificacionDestinatario");

            migrationBuilder.RenameTable(
                name: "Notificacion",
                schema: "dbo",
                newName: "Notificacion");

            migrationBuilder.RenameTable(
                name: "InformacionBasica",
                schema: "dbo",
                newName: "InformacionBasica");

            migrationBuilder.RenameTable(
                name: "HojaDeVida",
                schema: "dbo",
                newName: "HojaDeVida");

            migrationBuilder.AlterColumn<int>(
                name: "FuncionarioId",
                table: "NotificacionDestinatarioLog",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FuncionarioId",
                table: "NotificacionDestinatario",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificacionDestinatario_Funcionario_FuncionarioId",
                table: "NotificacionDestinatario",
                column: "FuncionarioId",
                principalTable: "Funcionario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificacionDestinatarioLog_Funcionario_FuncionarioId",
                table: "NotificacionDestinatarioLog",
                column: "FuncionarioId",
                principalTable: "Funcionario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
