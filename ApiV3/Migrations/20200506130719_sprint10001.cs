using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint10001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClaseCargo",
                table: "Cargo",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Sustituto",
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
                    CargoASustituirId = table.Column<int>(nullable: false),
                    CentroOperativoASutituirId = table.Column<int>(nullable: true),
                    CargoSustitutoId = table.Column<int>(nullable: false),
                    CentroOperativoSustitutoId = table.Column<int>(nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime", nullable: false),
                    Observacion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sustituto", x => x.Id);
                    table.CheckConstraint("CK_Sustituto_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_Sustituto_Cargo_CargoASustituirId",
                        column: x => x.CargoASustituirId,
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sustituto_Cargo_CargoSustitutoId",
                        column: x => x.CargoSustitutoId,
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sustituto_CentroOperativo_CentroOperativoASutituirId",
                        column: x => x.CentroOperativoASutituirId,
                        principalTable: "CentroOperativo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sustituto_CentroOperativo_CentroOperativoSustitutoId",
                        column: x => x.CentroOperativoSustitutoId,
                        principalTable: "CentroOperativo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sustituto_CargoASustituirId",
                table: "Sustituto",
                column: "CargoASustituirId");

            migrationBuilder.CreateIndex(
                name: "IX_Sustituto_CargoSustitutoId",
                table: "Sustituto",
                column: "CargoSustitutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Sustituto_CentroOperativoASutituirId",
                table: "Sustituto",
                column: "CentroOperativoASutituirId");

            migrationBuilder.CreateIndex(
                name: "IX_Sustituto_CentroOperativoSustitutoId",
                table: "Sustituto",
                column: "CentroOperativoSustitutoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sustituto");

            migrationBuilder.DropColumn(
                name: "ClaseCargo",
                table: "Cargo");
        }
    }
}
