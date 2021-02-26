using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateCheckConstraint(
                name: "CK_TipoDocumento_Formato",
                schema: "dbo",
                table: "TipoDocumento",
                sql: "([Formato]='Alfanumerico' OR [Formato]='Numerico')");

            migrationBuilder.AlterColumn<string>(
                name: "CodigoPila",
                schema: "dbo",
                table: "TipoDocumento",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.AlterColumn<string>(
                name: "CodigoDian",
                schema: "dbo",
                table: "TipoDocumento",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_TipoDocumento_Formato",
                schema: "dbo",
                table: "TipoDocumento");

            migrationBuilder.AlterColumn<string>(
                name: "CodigoPila",
                schema: "dbo",
                table: "TipoDocumento",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodigoDian",
                schema: "dbo",
                table: "TipoDocumento",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);
        }
    }
}
