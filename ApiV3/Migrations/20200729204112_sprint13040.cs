using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint13040 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Contrato",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(30)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Contrato",
                type: "char(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");
        }
    }
}
