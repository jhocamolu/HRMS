using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint14026 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OperacionTotal",
                table: "TipoLiquidacion",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TipoLiquidacionModulo",
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
                    TipoLiquidacionId = table.Column<int>(nullable: false),
                    Modulo = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoLiquidacionModulo", x => x.Id);
                    table.CheckConstraint("CK_TipoLiquidacionModulo_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_TipoLiquidacionModulo_TipoLiquidacion_TipoLiquidacionId",
                        column: x => x.TipoLiquidacionId,
                        principalTable: "TipoLiquidacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TipoLiquidacionModulo_TipoLiquidacionId",
                table: "TipoLiquidacionModulo",
                column: "TipoLiquidacionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoLiquidacionModulo");

            migrationBuilder.DropColumn(
                name: "OperacionTotal",
                table: "TipoLiquidacion");
        }
    }
}
