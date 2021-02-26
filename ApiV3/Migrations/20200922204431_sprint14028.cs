using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14028 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "AusentismoFuncionario",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(20)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "AusentismoFuncionario",
                type: "char(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");
        }
    }
}
