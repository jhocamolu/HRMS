using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint13027 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Novedad",
                type: "money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<int>(
                name: "Cantidad",
                table: "Novedad",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Novedad",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Cantidad",
                table: "Novedad",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
