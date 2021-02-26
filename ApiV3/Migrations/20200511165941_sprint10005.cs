using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint10005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaseCargo",
                table: "Cargo");

            migrationBuilder.AddColumn<string>(
                name: "Clase",
                table: "Cargo",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Clase",
                table: "Cargo");

            migrationBuilder.AddColumn<string>(
                name: "ClaseCargo",
                table: "Cargo",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");
        }
    }
}
