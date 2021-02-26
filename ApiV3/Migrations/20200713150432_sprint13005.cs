using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint13005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MedicinaPrepagada",
                table: "DeduccionRetefuente",
                type: "money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<decimal>(
                name: "InteresVivienda",
                table: "DeduccionRetefuente",
                type: "money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MedicinaPrepagada",
                table: "DeduccionRetefuente",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "InteresVivienda",
                table: "DeduccionRetefuente",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true);
        }
    }
}
