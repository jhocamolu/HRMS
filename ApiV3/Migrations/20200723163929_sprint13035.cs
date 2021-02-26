using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint13035 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OcupacionId",
                table: "InformacionBasicaHojaDeVida",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InformacionBasicaHojaDeVida_OcupacionId",
                table: "InformacionBasicaHojaDeVida",
                column: "OcupacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_InformacionBasicaHojaDeVida_Ocupacion_OcupacionId",
                table: "InformacionBasicaHojaDeVida",
                column: "OcupacionId",
                principalTable: "Ocupacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InformacionBasicaHojaDeVida_Ocupacion_OcupacionId",
                table: "InformacionBasicaHojaDeVida");

            migrationBuilder.DropIndex(
                name: "IX_InformacionBasicaHojaDeVida_OcupacionId",
                table: "InformacionBasicaHojaDeVida");

            migrationBuilder.DropColumn(
                name: "OcupacionId",
                table: "InformacionBasicaHojaDeVida");
        }
    }
}
