using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14030 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Cantidad",
                table: "Novedad",
                type: "decimal(16,6)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Cantidad",
                table: "NominaDetalle",
                type: "decimal(16,6)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Cantidad",
                table: "Novedad",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Cantidad",
                table: "NominaDetalle",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,6)");
        }
    }
}
