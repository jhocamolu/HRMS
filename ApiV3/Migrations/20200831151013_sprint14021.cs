using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint14021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_EnlaceExterno",
                schema: "util",
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
                    Titulo = table.Column<string>(type: "varchar(255)", nullable: false),
                    Url = table.Column<string>(type: "varchar(255)", nullable: false),
                    Imagen = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EnlaceExterno", x => x.Id);
                    table.CheckConstraint("CK_EnlaceExterno_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "_MenuFavorito",
                schema: "util",
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
                    FuncionarioId = table.Column<int>(nullable: false),
                    ItemMenu = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MenuFavorito", x => x.Id);
                    table.CheckConstraint("CK_MenuFavorito_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_EnlaceExterno",
                schema: "util");

            migrationBuilder.DropTable(
                name: "_MenuFavorito",
                schema: "util");
        }
    }
}
