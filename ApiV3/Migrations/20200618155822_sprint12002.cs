using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint12002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AplicacionExternaCargo_Cargo_CargoDependienteId",
                table: "AplicacionExternaCargo");

            migrationBuilder.DropIndex(
                name: "IX_AplicacionExternaCargo_CargoDependienteId",
                table: "AplicacionExternaCargo");

            migrationBuilder.DropColumn(
                name: "CargoDependienteId",
                table: "AplicacionExternaCargo");

            migrationBuilder.AddColumn<string>(
                name: "Justificacion",
                table: "SolicitudCesantia",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AplicacionExternaCargoDependiente",
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
                    CargoDependienteId = table.Column<int>(nullable: false),
                    AplicacionExternaCargoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AplicacionExternaCargoDependiente", x => x.Id);
                    table.CheckConstraint("CK_AplicacionExternaCargoDependiente_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_AplicacionExternaCargoDependiente_AplicacionExternaCargo_AplicacionExternaCargoId",
                        column: x => x.AplicacionExternaCargoId,
                        principalTable: "AplicacionExternaCargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AplicacionExternaCargoDependiente_Cargo_CargoDependienteId",
                        column: x => x.CargoDependienteId,
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AplicacionExternaCargoDependiente_AplicacionExternaCargoId",
                table: "AplicacionExternaCargoDependiente",
                column: "AplicacionExternaCargoId");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacionExternaCargoDependiente_CargoDependienteId",
                table: "AplicacionExternaCargoDependiente",
                column: "CargoDependienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AplicacionExternaCargoDependiente");

            migrationBuilder.DropColumn(
                name: "Justificacion",
                table: "SolicitudCesantia");

            migrationBuilder.AddColumn<int>(
                name: "CargoDependienteId",
                table: "AplicacionExternaCargo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AplicacionExternaCargo_CargoDependienteId",
                table: "AplicacionExternaCargo",
                column: "CargoDependienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_AplicacionExternaCargo_Cargo_CargoDependienteId",
                table: "AplicacionExternaCargo",
                column: "CargoDependienteId",
                principalTable: "Cargo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
