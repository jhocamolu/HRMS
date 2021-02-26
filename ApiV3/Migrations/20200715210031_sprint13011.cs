using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint13011 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "SalarioPortalReclutamiento",
                table: "RequisicionPersonal",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<decimal>(
                name: "Salario",
                table: "RequisicionPersonal",
                type: "money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<bool>(
                name: "PerfilPortalReclutamiento",
                table: "RequisicionPersonal",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "CompetenciaPortalReclutamiento",
                table: "RequisicionPersonal",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "SalarioPortalReclutamiento",
                table: "RequisicionPersonal",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Salario",
                table: "RequisicionPersonal",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "PerfilPortalReclutamiento",
                table: "RequisicionPersonal",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "CompetenciaPortalReclutamiento",
                table: "RequisicionPersonal",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
