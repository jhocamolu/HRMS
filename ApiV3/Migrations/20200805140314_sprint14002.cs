using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint14002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CausalTerminacionId",
                table: "Contrato",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObservacionFinalizacionContrato",
                table: "Contrato",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CausalTerminacion",
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
                    Codigo = table.Column<string>(type: "char(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CausalTerminacion", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_CausalTerminacionId",
                table: "Contrato",
                column: "CausalTerminacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_CausalTerminacion_CausalTerminacionId",
                table: "Contrato",
                column: "CausalTerminacionId",
                principalTable: "CausalTerminacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_CausalTerminacion_CausalTerminacionId",
                table: "Contrato");

            migrationBuilder.DropTable(
                name: "CausalTerminacion");

            migrationBuilder.DropIndex(
                name: "IX_Contrato_CausalTerminacionId",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "CausalTerminacionId",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "ObservacionFinalizacionContrato",
                table: "Contrato");
        }
    }
}
