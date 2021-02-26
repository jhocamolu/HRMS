using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint13018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anio",
                table: "DeduccionRetefuente");

            migrationBuilder.AddColumn<int>(
                name: "AnnoVigenciaId",
                table: "DeduccionRetefuente",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DeduccionRetefuente_AnnoVigenciaId",
                table: "DeduccionRetefuente",
                column: "AnnoVigenciaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeduccionRetefuente_AnnoVigencia_AnnoVigenciaId",
                table: "DeduccionRetefuente",
                column: "AnnoVigenciaId",
                principalTable: "AnnoVigencia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeduccionRetefuente_AnnoVigencia_AnnoVigenciaId",
                table: "DeduccionRetefuente");

            migrationBuilder.DropIndex(
                name: "IX_DeduccionRetefuente_AnnoVigenciaId",
                table: "DeduccionRetefuente");

            migrationBuilder.DropColumn(
                name: "AnnoVigenciaId",
                table: "DeduccionRetefuente");

            migrationBuilder.AddColumn<short>(
                name: "Anio",
                table: "DeduccionRetefuente",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
