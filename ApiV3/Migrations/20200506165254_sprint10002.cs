using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint10002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateCheckConstraint(
                name: "CK_TareaProgramadaLog_Estado",
                table: "TareaProgramadaLog",
                sql: "([Estado]='Exitoso' OR [Estado]='Fallido')");

            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "TareaProgramada",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TareaProgramada_Alias",
                table: "TareaProgramada",
                column: "Alias",
                unique: true,
                filter: "[Alias] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_TareaProgramadaLog_Estado",
                table: "TareaProgramadaLog");

            migrationBuilder.DropIndex(
                name: "IX_TareaProgramada_Alias",
                table: "TareaProgramada");

            migrationBuilder.DropColumn(
                name: "Alias",
                table: "TareaProgramada");
        }
    }
}
