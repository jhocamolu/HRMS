using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint13007 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "HoraExtra",
                type: "money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AddColumn<string>(
                name: "ObservacionNotaAcademica",
                table: "Beneficio",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObservacionNotaAcademica",
                table: "Beneficio");

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "HoraExtra",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true);
        }
    }
}
