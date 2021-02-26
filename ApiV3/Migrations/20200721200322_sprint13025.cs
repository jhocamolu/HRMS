using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint13025 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Novedad_Tercero_TerceroId",
                table: "Novedad");

            migrationBuilder.DropForeignKey(
                name: "FK_Novedad_TipoPeriodo_TipoPeriodoId",
                table: "Novedad");

            migrationBuilder.DropIndex(
                name: "IX_Novedad_TerceroId",
                table: "Novedad");

            migrationBuilder.DropIndex(
                name: "IX_Novedad_TipoPeriodoId",
                table: "Novedad");

            migrationBuilder.DropColumn(
                name: "TipoPeriodoId",
                table: "Novedad");

            migrationBuilder.AlterColumn<string>(
                name: "Unidad",
                table: "Novedad",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TerceroId",
                table: "Novedad",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Unidad",
                table: "Novedad",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<int>(
                name: "TerceroId",
                table: "Novedad",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoPeriodoId",
                table: "Novedad",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Novedad_TerceroId",
                table: "Novedad",
                column: "TerceroId");

            migrationBuilder.CreateIndex(
                name: "IX_Novedad_TipoPeriodoId",
                table: "Novedad",
                column: "TipoPeriodoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Novedad_Tercero_TerceroId",
                table: "Novedad",
                column: "TerceroId",
                principalTable: "Tercero",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Novedad_TipoPeriodo_TipoPeriodoId",
                table: "Novedad",
                column: "TipoPeriodoId",
                principalTable: "TipoPeriodo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
