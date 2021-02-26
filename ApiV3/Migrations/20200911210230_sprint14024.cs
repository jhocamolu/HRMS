using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14024 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AportaAArl",
                table: "TipoCotizanteSubtipoCotizante",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AportaACcf",
                table: "TipoCotizanteSubtipoCotizante",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AportaAIcbf",
                table: "TipoCotizanteSubtipoCotizante",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AportaAPension",
                table: "TipoCotizanteSubtipoCotizante",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AportaASalud",
                table: "TipoCotizanteSubtipoCotizante",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AportaASena",
                table: "TipoCotizanteSubtipoCotizante",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AportaAArl",
                table: "TipoCotizanteSubtipoCotizante");

            migrationBuilder.DropColumn(
                name: "AportaACcf",
                table: "TipoCotizanteSubtipoCotizante");

            migrationBuilder.DropColumn(
                name: "AportaAIcbf",
                table: "TipoCotizanteSubtipoCotizante");

            migrationBuilder.DropColumn(
                name: "AportaAPension",
                table: "TipoCotizanteSubtipoCotizante");

            migrationBuilder.DropColumn(
                name: "AportaASalud",
                table: "TipoCotizanteSubtipoCotizante");

            migrationBuilder.DropColumn(
                name: "AportaASena",
                table: "TipoCotizanteSubtipoCotizante");
        }
    }
}
