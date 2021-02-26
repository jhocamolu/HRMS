using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14036 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuncionarioCentroCosto_CentroCosto_CentroCostoId",
                table: "FuncionarioCentroCosto");

            migrationBuilder.DropIndex(
                name: "IX_FuncionarioCentroCosto_CentroCostoId",
                table: "FuncionarioCentroCosto");

            migrationBuilder.DropColumn(
                name: "CentroCostoId",
                table: "FuncionarioCentroCosto");

            migrationBuilder.AddColumn<int>(
                name: "ActividadCentroCostoId",
                table: "FuncionarioCentroCosto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioCentroCosto_ActividadCentroCostoId",
                table: "FuncionarioCentroCosto",
                column: "ActividadCentroCostoId");

            migrationBuilder.AddForeignKey(
                name: "FK_FuncionarioCentroCosto_ActividadCentroCosto_ActividadCentroCostoId",
                table: "FuncionarioCentroCosto",
                column: "ActividadCentroCostoId",
                principalTable: "ActividadCentroCosto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuncionarioCentroCosto_ActividadCentroCosto_ActividadCentroCostoId",
                table: "FuncionarioCentroCosto");

            migrationBuilder.DropIndex(
                name: "IX_FuncionarioCentroCosto_ActividadCentroCostoId",
                table: "FuncionarioCentroCosto");

            migrationBuilder.DropColumn(
                name: "ActividadCentroCostoId",
                table: "FuncionarioCentroCosto");

            migrationBuilder.AddColumn<int>(
                name: "CentroCostoId",
                table: "FuncionarioCentroCosto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioCentroCosto_CentroCostoId",
                table: "FuncionarioCentroCosto",
                column: "CentroCostoId");

            migrationBuilder.AddForeignKey(
                name: "FK_FuncionarioCentroCosto_CentroCosto_CentroCostoId",
                table: "FuncionarioCentroCosto",
                column: "CentroCostoId",
                principalTable: "CentroCosto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
