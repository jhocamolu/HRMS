using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint13026 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BeneficiarioImpuestoEquidad",
                table: "InformacionBasica",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BeneficiarioLey1429De2010",
                table: "InformacionBasica",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CargoId",
                table: "InformacionBasica",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClaseAportanteTipoAportanteId",
                table: "InformacionBasica",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "InformacionBasica",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NaturalezaJuridicaId",
                table: "InformacionBasica",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoDocumentoId",
                table: "InformacionBasica",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoPersonaId",
                table: "InformacionBasica",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasica_CargoId",
                table: "InformacionBasica",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasica_ClaseAportanteTipoAportanteId",
                table: "InformacionBasica",
                column: "ClaseAportanteTipoAportanteId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasica_NaturalezaJuridicaId",
                table: "InformacionBasica",
                column: "NaturalezaJuridicaId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasica_TipoDocumentoId",
                table: "InformacionBasica",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasica_TipoPersonaId",
                table: "InformacionBasica",
                column: "TipoPersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_InformacionBasica_Cargo_CargoId",
                table: "InformacionBasica",
                column: "CargoId",
                principalTable: "Cargo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InformacionBasica_ClaseAportanteTipoAportante_ClaseAportanteTipoAportanteId",
                table: "InformacionBasica",
                column: "ClaseAportanteTipoAportanteId",
                principalTable: "ClaseAportanteTipoAportante",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InformacionBasica_NaturalezaJuridica_NaturalezaJuridicaId",
                table: "InformacionBasica",
                column: "NaturalezaJuridicaId",
                principalTable: "NaturalezaJuridica",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InformacionBasica_TipoDocumento_TipoDocumentoId",
                table: "InformacionBasica",
                column: "TipoDocumentoId",
                principalTable: "TipoDocumento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InformacionBasica_TipoPersona_TipoPersonaId",
                table: "InformacionBasica",
                column: "TipoPersonaId",
                principalTable: "TipoPersona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InformacionBasica_Cargo_CargoId",
                table: "InformacionBasica");

            migrationBuilder.DropForeignKey(
                name: "FK_InformacionBasica_ClaseAportanteTipoAportante_ClaseAportanteTipoAportanteId",
                table: "InformacionBasica");

            migrationBuilder.DropForeignKey(
                name: "FK_InformacionBasica_NaturalezaJuridica_NaturalezaJuridicaId",
                table: "InformacionBasica");

            migrationBuilder.DropForeignKey(
                name: "FK_InformacionBasica_TipoDocumento_TipoDocumentoId",
                table: "InformacionBasica");

            migrationBuilder.DropForeignKey(
                name: "FK_InformacionBasica_TipoPersona_TipoPersonaId",
                table: "InformacionBasica");

            migrationBuilder.DropIndex(
                name: "IX_InformacionBasica_CargoId",
                table: "InformacionBasica");

            migrationBuilder.DropIndex(
                name: "IX_InformacionBasica_ClaseAportanteTipoAportanteId",
                table: "InformacionBasica");

            migrationBuilder.DropIndex(
                name: "IX_InformacionBasica_NaturalezaJuridicaId",
                table: "InformacionBasica");

            migrationBuilder.DropIndex(
                name: "IX_InformacionBasica_TipoDocumentoId",
                table: "InformacionBasica");

            migrationBuilder.DropIndex(
                name: "IX_InformacionBasica_TipoPersonaId",
                table: "InformacionBasica");

            migrationBuilder.DropColumn(
                name: "BeneficiarioImpuestoEquidad",
                table: "InformacionBasica");

            migrationBuilder.DropColumn(
                name: "BeneficiarioLey1429De2010",
                table: "InformacionBasica");

            migrationBuilder.DropColumn(
                name: "CargoId",
                table: "InformacionBasica");

            migrationBuilder.DropColumn(
                name: "ClaseAportanteTipoAportanteId",
                table: "InformacionBasica");

            migrationBuilder.DropColumn(
                name: "Fax",
                table: "InformacionBasica");

            migrationBuilder.DropColumn(
                name: "NaturalezaJuridicaId",
                table: "InformacionBasica");

            migrationBuilder.DropColumn(
                name: "TipoDocumentoId",
                table: "InformacionBasica");

            migrationBuilder.DropColumn(
                name: "TipoPersonaId",
                table: "InformacionBasica");
        }
    }
}
