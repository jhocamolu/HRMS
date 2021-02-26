using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint13039 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcedimientoRetencio",
                table: "Contrato");

            migrationBuilder.AlterColumn<int>(
                name: "PeriodoPrueba",
                table: "Contrato",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AddColumn<string>(
                name: "ProcedimientoRetencion",
                table: "Contrato",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcedimientoRetencion",
                table: "Contrato");

            migrationBuilder.AlterColumn<string>(
                name: "PeriodoPrueba",
                table: "Contrato",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "ProcedimientoRetencio",
                table: "Contrato",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");
        }
    }
}
