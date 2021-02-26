using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14049 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CargoCentroCosto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CargoId = table.Column<int>(nullable: false),
                    Porcentaje = table.Column<decimal>(type: "decimal(16,6)", nullable: true),
                    FechaCorte = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoCentroCosto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargoCentroCosto_Cargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CargoCentroCosto_CargoId",
                table: "CargoCentroCosto",
                column: "CargoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargoCentroCosto");
        }
    }
}
