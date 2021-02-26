using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14039 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateCheckConstraint(
                name: "CK_FuncionarioCentroCosto_Estado",
                table: "FuncionarioCentroCosto",
                sql: "([Estado]='Pendiente' OR [Estado]='Aplicado' )");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_FuncionarioCentroCosto_Estado",
                table: "FuncionarioCentroCosto");
        }
    }
}
