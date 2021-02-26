using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint13024 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TipoAportanteTipoPlanilla_TipoPlanillaId",
                table: "TipoAportanteTipoPlanilla");

            migrationBuilder.CreateIndex(
                name: "IX_TipoAportanteTipoPlanilla_TipoPlanillaId",
                table: "TipoAportanteTipoPlanilla",
                column: "TipoPlanillaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TipoAportanteTipoPlanilla_TipoPlanillaId",
                table: "TipoAportanteTipoPlanilla");

            migrationBuilder.CreateIndex(
                name: "IX_TipoAportanteTipoPlanilla_TipoPlanillaId",
                table: "TipoAportanteTipoPlanilla",
                column: "TipoPlanillaId",
                unique: true);
        }
    }
}
