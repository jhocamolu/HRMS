using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14053 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CentroOperativoId",
                table: "CargoCentroCosto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CargoCentroCosto_CentroOperativoId",
                table: "CargoCentroCosto",
                column: "CentroOperativoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoCentroCosto_CentroOperativo_CentroOperativoId",
                table: "CargoCentroCosto",
                column: "CentroOperativoId",
                principalTable: "CentroOperativo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoCentroCosto_CentroOperativo_CentroOperativoId",
                table: "CargoCentroCosto");

            migrationBuilder.DropIndex(
                name: "IX_CargoCentroCosto_CentroOperativoId",
                table: "CargoCentroCosto");

            migrationBuilder.DropColumn(
                name: "CentroOperativoId",
                table: "CargoCentroCosto");
        }
    }
}
