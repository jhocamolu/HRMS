using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AplicacionExternaCargoDependiente_AplicacionExternaCargo_AplicacionExternaCargoId",
                table: "AplicacionExternaCargoDependiente");

            migrationBuilder.AddForeignKey(
                name: "FK_AplicacionExternaCargoDependiente_AplicacionExternaCargo_AplicacionExternaCargoId",
                table: "AplicacionExternaCargoDependiente",
                column: "AplicacionExternaCargoId",
                principalTable: "AplicacionExternaCargo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AplicacionExternaCargoDependiente_AplicacionExternaCargo_AplicacionExternaCargoId",
                table: "AplicacionExternaCargoDependiente");

            migrationBuilder.AddForeignKey(
                name: "FK_AplicacionExternaCargoDependiente_AplicacionExternaCargo_AplicacionExternaCargoId",
                table: "AplicacionExternaCargoDependiente",
                column: "AplicacionExternaCargoId",
                principalTable: "AplicacionExternaCargo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
