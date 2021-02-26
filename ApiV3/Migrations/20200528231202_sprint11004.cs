using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint11004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MotivoSolicitudCesantia",
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
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivoSolicitudCesantia", x => x.Id);
                    table.CheckConstraint("CK_MotivoSolicitudCesantia_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "SolicitudCesantia",
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
                    MotivoSolicitudCesantiaId = table.Column<int>(nullable: false),
                    FechaSolicitud = table.Column<DateTime>(nullable: false),
                    BaseCalculoCesantia = table.Column<decimal>(type: "money", nullable: false),
                    ValorSolicitado = table.Column<decimal>(type: "money", nullable: false),
                    Soporte = table.Column<string>(type: "varchar(255)", nullable: false),
                    Observacion = table.Column<string>(type: "varchar(255)", nullable: true),
                    Estado = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudCesantia", x => x.Id);
                    table.CheckConstraint("CK_SolicitudCesantia_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_SolicitudCesantia_Estado", "([Estado]='EnTramite' OR [Estado]='Aprobada' OR [Estado]='Rechazada' OR [Estado]='Cancelada' OR [Estado]='Finalizada' )");
                    table.ForeignKey(
                        name: "FK_SolicitudCesantia_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudCesantia_MotivoSolicitudCesantia_MotivoSolicitudCesantiaId",
                        column: x => x.MotivoSolicitudCesantiaId,
                        principalTable: "MotivoSolicitudCesantia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudCesantia_FuncionarioId",
                table: "SolicitudCesantia",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudCesantia_MotivoSolicitudCesantiaId",
                table: "SolicitudCesantia",
                column: "MotivoSolicitudCesantiaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitudCesantia");

            migrationBuilder.DropTable(
                name: "MotivoSolicitudCesantia");
        }
    }
}
