using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint13017 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anno",
                table: "CargoPresupuesto");

            migrationBuilder.AddColumn<int>(
                name: "AnnoVigenciaId",
                table: "CargoPresupuesto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CargoPresupuesto_AnnoVigenciaId",
                table: "CargoPresupuesto",
                column: "AnnoVigenciaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoPresupuesto_AnnoVigencia_AnnoVigenciaId",
                table: "CargoPresupuesto",
                column: "AnnoVigenciaId",
                principalTable: "AnnoVigencia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoPresupuesto_AnnoVigencia_AnnoVigenciaId",
                table: "CargoPresupuesto");

            migrationBuilder.DropIndex(
                name: "IX_CargoPresupuesto_AnnoVigenciaId",
                table: "CargoPresupuesto");

            migrationBuilder.DropColumn(
                name: "AnnoVigenciaId",
                table: "CargoPresupuesto");

            migrationBuilder.AddColumn<int>(
                name: "Anno",
                table: "CargoPresupuesto",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
