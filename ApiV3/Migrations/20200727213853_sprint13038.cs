using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint13038 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequisicionPersonal_CentroOperativo_CentroOperativoSolicitadoID",
                table: "RequisicionPersonal");

            migrationBuilder.DropForeignKey(
                name: "FK_RequisicionPersonal_TipoContrato_TipoContratoID",
                table: "RequisicionPersonal");

            migrationBuilder.RenameColumn(
                name: "TipoContratoID",
                table: "RequisicionPersonal",
                newName: "TipoContratoId");

            migrationBuilder.RenameColumn(
                name: "CentroOperativoSolicitadoID",
                table: "RequisicionPersonal",
                newName: "CentroOperativoSolicitadoId");

            migrationBuilder.RenameIndex(
                name: "IX_RequisicionPersonal_TipoContratoID",
                table: "RequisicionPersonal",
                newName: "IX_RequisicionPersonal_TipoContratoId");

            migrationBuilder.RenameIndex(
                name: "IX_RequisicionPersonal_CentroOperativoSolicitadoID",
                table: "RequisicionPersonal",
                newName: "IX_RequisicionPersonal_CentroOperativoSolicitadoId");

            migrationBuilder.AlterColumn<int>(
                name: "CentroOperativoSolicitanteId",
                table: "RequisicionPersonal",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CentroOperativoSolicitadoId",
                table: "RequisicionPersonal",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RequisicionPersonal_CentroOperativo_CentroOperativoSolicitadoId",
                table: "RequisicionPersonal",
                column: "CentroOperativoSolicitadoId",
                principalTable: "CentroOperativo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequisicionPersonal_TipoContrato_TipoContratoId",
                table: "RequisicionPersonal",
                column: "TipoContratoId",
                principalTable: "TipoContrato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequisicionPersonal_CentroOperativo_CentroOperativoSolicitadoId",
                table: "RequisicionPersonal");

            migrationBuilder.DropForeignKey(
                name: "FK_RequisicionPersonal_TipoContrato_TipoContratoId",
                table: "RequisicionPersonal");

            migrationBuilder.RenameColumn(
                name: "TipoContratoId",
                table: "RequisicionPersonal",
                newName: "TipoContratoID");

            migrationBuilder.RenameColumn(
                name: "CentroOperativoSolicitadoId",
                table: "RequisicionPersonal",
                newName: "CentroOperativoSolicitadoID");

            migrationBuilder.RenameIndex(
                name: "IX_RequisicionPersonal_TipoContratoId",
                table: "RequisicionPersonal",
                newName: "IX_RequisicionPersonal_TipoContratoID");

            migrationBuilder.RenameIndex(
                name: "IX_RequisicionPersonal_CentroOperativoSolicitadoId",
                table: "RequisicionPersonal",
                newName: "IX_RequisicionPersonal_CentroOperativoSolicitadoID");

            migrationBuilder.AlterColumn<int>(
                name: "CentroOperativoSolicitanteId",
                table: "RequisicionPersonal",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CentroOperativoSolicitadoID",
                table: "RequisicionPersonal",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RequisicionPersonal_CentroOperativo_CentroOperativoSolicitadoID",
                table: "RequisicionPersonal",
                column: "CentroOperativoSolicitadoID",
                principalTable: "CentroOperativo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequisicionPersonal_TipoContrato_TipoContratoID",
                table: "RequisicionPersonal",
                column: "TipoContratoID",
                principalTable: "TipoContrato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
