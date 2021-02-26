using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint13014 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anno",
                table: "ParametroGeneral");

            migrationBuilder.AddColumn<int>(
                name: "AnnoVigenciaId",
                table: "ParametroGeneral",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AnnoVigencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstadoRegistro = table.Column<string>(type: "char(10)", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    EliminadoPor = table.Column<string>(type: "varchar(255)", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Anno = table.Column<int>(nullable: false),
                    Estado = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnoVigencia", x => x.Id);
                    table.CheckConstraint("CK_AnnoVigencia_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_AnnoVigencia_Estado", "([Estado]='Vigente' OR [Estado]='Cerrado')");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParametroGeneral_AnnoVigenciaId",
                table: "ParametroGeneral",
                column: "AnnoVigenciaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParametroGeneral_AnnoVigencia_AnnoVigenciaId",
                table: "ParametroGeneral",
                column: "AnnoVigenciaId",
                principalTable: "AnnoVigencia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParametroGeneral_AnnoVigencia_AnnoVigenciaId",
                table: "ParametroGeneral");

            migrationBuilder.DropTable(
                name: "AnnoVigencia");

            migrationBuilder.DropIndex(
                name: "IX_ParametroGeneral_AnnoVigenciaId",
                table: "ParametroGeneral");

            migrationBuilder.DropColumn(
                name: "AnnoVigenciaId",
                table: "ParametroGeneral");

            migrationBuilder.AddColumn<int>(
                name: "Anno",
                table: "ParametroGeneral",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
