using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14045 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoOtroSiId",
                table: "ContratoOtroSi",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TipoOtroSi",
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
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false),
                    Numeracion = table.Column<bool>(nullable: false),
                    DocumentoSlug = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoOtroSi", x => x.Id);
                    table.CheckConstraint("CK_TipoOtroSi_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContratoOtroSi_TipoOtroSiId",
                table: "ContratoOtroSi",
                column: "TipoOtroSiId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContratoOtroSi_TipoOtroSi_TipoOtroSiId",
                table: "ContratoOtroSi",
                column: "TipoOtroSiId",
                principalTable: "TipoOtroSi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContratoOtroSi_TipoOtroSi_TipoOtroSiId",
                table: "ContratoOtroSi");

            migrationBuilder.DropTable(
                name: "TipoOtroSi");

            migrationBuilder.DropIndex(
                name: "IX_ContratoOtroSi_TipoOtroSiId",
                table: "ContratoOtroSi");

            migrationBuilder.DropColumn(
                name: "TipoOtroSiId",
                table: "ContratoOtroSi");
        }
    }
}
