using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint12006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tercero_DivisionPoliticaNivel1_DivisionPoliticaNivel1Id",
                table: "Tercero");

            migrationBuilder.DropIndex(
                name: "IX_Tercero_DivisionPoliticaNivel1Id",
                table: "Tercero");

            migrationBuilder.DropColumn(
                name: "DivisionPoliticaNivel1Id",
                table: "Tercero");

            migrationBuilder.AddColumn<int>(
                name: "DivisionPoliticaNivel2Id",
                table: "Tercero",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tercero_DivisionPoliticaNivel2Id",
                table: "Tercero",
                column: "DivisionPoliticaNivel2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tercero_DivisionPoliticaNivel2_DivisionPoliticaNivel2Id",
                table: "Tercero",
                column: "DivisionPoliticaNivel2Id",
                principalTable: "DivisionPoliticaNivel2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tercero_DivisionPoliticaNivel2_DivisionPoliticaNivel2Id",
                table: "Tercero");

            migrationBuilder.DropIndex(
                name: "IX_Tercero_DivisionPoliticaNivel2Id",
                table: "Tercero");

            migrationBuilder.DropColumn(
                name: "DivisionPoliticaNivel2Id",
                table: "Tercero");

            migrationBuilder.AddColumn<int>(
                name: "DivisionPoliticaNivel1Id",
                table: "Tercero",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tercero_DivisionPoliticaNivel1Id",
                table: "Tercero",
                column: "DivisionPoliticaNivel1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tercero_DivisionPoliticaNivel1_DivisionPoliticaNivel1Id",
                table: "Tercero",
                column: "DivisionPoliticaNivel1Id",
                principalTable: "DivisionPoliticaNivel1",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
