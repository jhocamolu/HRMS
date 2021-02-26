using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14047 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacion",
                table: "HoraExtra",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrigenId",
                table: "HoraExtra",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacion",
                table: "HoraExtra");

            migrationBuilder.DropColumn(
                name: "OrigenId",
                table: "HoraExtra");
        }
    }
}
