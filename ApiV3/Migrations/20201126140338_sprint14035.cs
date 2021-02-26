using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14035 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActividadId",
                table: "ActividadFuncionario",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ActividadFuncionario_ActividadId",
                table: "ActividadFuncionario",
                column: "ActividadId");

            migrationBuilder.CreateIndex(
                name: "IX_Actividad_Codigo",
                table: "Actividad",
                column: "Codigo",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ActividadFuncionario_Actividad_ActividadId",
                table: "ActividadFuncionario",
                column: "ActividadId",
                principalTable: "Actividad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActividadFuncionario_Actividad_ActividadId",
                table: "ActividadFuncionario");

            migrationBuilder.DropIndex(
                name: "IX_ActividadFuncionario_ActividadId",
                table: "ActividadFuncionario");

            migrationBuilder.DropIndex(
                name: "IX_Actividad_Codigo",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "ActividadId",
                table: "ActividadFuncionario");
        }
    }
}
