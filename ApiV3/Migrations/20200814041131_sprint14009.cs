using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14009 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Candidato_RequisicionPersonalId",
                table: "Candidato");

            migrationBuilder.CreateIndex(
                name: "IX_Candidato_RequisicionPersonalId_HojaDeVidaId",
                table: "Candidato",
                columns: new[] { "RequisicionPersonalId", "HojaDeVidaId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Candidato_RequisicionPersonalId_HojaDeVidaId",
                table: "Candidato");

            migrationBuilder.CreateIndex(
                name: "IX_Candidato_RequisicionPersonalId",
                table: "Candidato",
                column: "RequisicionPersonalId");
        }
    }
}
