using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint12023 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RequiereFechaPagoPlanilla",
                table: "TipoPlanilla",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiereNumeroPlanilla",
                table: "TipoPlanilla",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiereFechaPagoPlanilla",
                table: "TipoPlanilla");

            migrationBuilder.DropColumn(
                name: "RequiereNumeroPlanilla",
                table: "TipoPlanilla");
        }
    }
}
