using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint13020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClaseAportante",
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
                    Codigo = table.Column<string>(type: "char(1)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaseAportante", x => x.Id);
                    table.CheckConstraint("CK_ClaseAportante_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "SubtipoCotizante",
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
                    Codigo = table.Column<string>(type: "varchar(255)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubtipoCotizante", x => x.Id);
                    table.CheckConstraint("CK_SubtipoCotizante_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "TipoAportante",
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
                    Codigo = table.Column<string>(type: "char(2)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAportante", x => x.Id);
                    table.CheckConstraint("CK_TipoAportante_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "TipoCotizante",
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
                    Codigo = table.Column<string>(type: "char(2)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCotizante", x => x.Id);
                    table.CheckConstraint("CK_TipoCotizante_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "TipoPersona",
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
                    Codigo = table.Column<string>(type: "char(1)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPersona", x => x.Id);
                    table.CheckConstraint("CK_TipoPersonas_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "TipoPlanilla",
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
                    Codigo = table.Column<string>(type: "char(1)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPlanilla", x => x.Id);
                    table.CheckConstraint("CK_TipoPlanilla_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                });

            migrationBuilder.CreateTable(
                name: "ClaseAportanteTipoCotizante",
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
                    ClaseAportanteId = table.Column<int>(nullable: false),
                    TipoCotizanteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaseAportanteTipoCotizante", x => x.Id);
                    table.CheckConstraint("CK_ClaseAportanteTipoCotizante_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_ClaseAportanteTipoCotizante_ClaseAportante_ClaseAportanteId",
                        column: x => x.ClaseAportanteId,
                        principalTable: "ClaseAportante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClaseAportanteTipoCotizante_TipoCotizante_TipoCotizanteId",
                        column: x => x.TipoCotizanteId,
                        principalTable: "TipoCotizante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoAportanteTipoCotizante",
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
                    TipoAportanteId = table.Column<int>(nullable: false),
                    TipoCotizanteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAportanteTipoCotizante", x => x.Id);
                    table.CheckConstraint("CK_TipoAportanteTipoCotizante_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_TipoAportanteTipoCotizante_TipoAportante_TipoAportanteId",
                        column: x => x.TipoAportanteId,
                        principalTable: "TipoAportante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TipoAportanteTipoCotizante_TipoCotizante_TipoCotizanteId",
                        column: x => x.TipoCotizanteId,
                        principalTable: "TipoCotizante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoCotizanteSubtipoCotizante",
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
                    TipoCotizanteId = table.Column<int>(nullable: false),
                    SubtipoCotizanteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCotizanteSubtipoCotizante", x => x.Id);
                    table.CheckConstraint("CK_TipoCotizanteSubtipoCotizante_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_TipoCotizanteSubtipoCotizante_SubtipoCotizante_SubtipoCotizanteId",
                        column: x => x.SubtipoCotizanteId,
                        principalTable: "SubtipoCotizante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TipoCotizanteSubtipoCotizante_TipoCotizante_TipoCotizanteId",
                        column: x => x.TipoCotizanteId,
                        principalTable: "TipoCotizante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoAportanteTipoPlanilla",
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
                    TipoAportanteId = table.Column<int>(nullable: false),
                    TipoPlanillaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAportanteTipoPlanilla", x => x.Id);
                    table.CheckConstraint("CK_TipoAportanteTipoPlanilla_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.ForeignKey(
                        name: "FK_TipoAportanteTipoPlanilla_TipoAportante_TipoAportanteId",
                        column: x => x.TipoAportanteId,
                        principalTable: "TipoAportante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TipoAportanteTipoPlanilla_TipoPlanilla_TipoPlanillaId",
                        column: x => x.TipoPlanillaId,
                        principalTable: "TipoPlanilla",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaseAportante_Codigo",
                table: "ClaseAportante",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClaseAportante_Nombre",
                table: "ClaseAportante",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClaseAportanteTipoCotizante_ClaseAportanteId",
                table: "ClaseAportanteTipoCotizante",
                column: "ClaseAportanteId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaseAportanteTipoCotizante_TipoCotizanteId",
                table: "ClaseAportanteTipoCotizante",
                column: "TipoCotizanteId");

            migrationBuilder.CreateIndex(
                name: "IX_SubtipoCotizante_Codigo",
                table: "SubtipoCotizante",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubtipoCotizante_Nombre",
                table: "SubtipoCotizante",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoAportante_Codigo",
                table: "TipoAportante",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoAportante_Nombre",
                table: "TipoAportante",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoAportanteTipoCotizante_TipoAportanteId",
                table: "TipoAportanteTipoCotizante",
                column: "TipoAportanteId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoAportanteTipoCotizante_TipoCotizanteId",
                table: "TipoAportanteTipoCotizante",
                column: "TipoCotizanteId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoAportanteTipoPlanilla_TipoAportanteId",
                table: "TipoAportanteTipoPlanilla",
                column: "TipoAportanteId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoAportanteTipoPlanilla_TipoPlanillaId",
                table: "TipoAportanteTipoPlanilla",
                column: "TipoPlanillaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoCotizante_Codigo",
                table: "TipoCotizante",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoCotizante_Nombre",
                table: "TipoCotizante",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoCotizanteSubtipoCotizante_SubtipoCotizanteId",
                table: "TipoCotizanteSubtipoCotizante",
                column: "SubtipoCotizanteId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoCotizanteSubtipoCotizante_TipoCotizanteId",
                table: "TipoCotizanteSubtipoCotizante",
                column: "TipoCotizanteId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoPersona_Codigo",
                table: "TipoPersona",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoPersona_Nombre",
                table: "TipoPersona",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoPlanilla_Codigo",
                table: "TipoPlanilla",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoPlanilla_Nombre",
                table: "TipoPlanilla",
                column: "Nombre",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaseAportanteTipoCotizante");

            migrationBuilder.DropTable(
                name: "TipoAportanteTipoCotizante");

            migrationBuilder.DropTable(
                name: "TipoAportanteTipoPlanilla");

            migrationBuilder.DropTable(
                name: "TipoCotizanteSubtipoCotizante");

            migrationBuilder.DropTable(
                name: "TipoPersona");

            migrationBuilder.DropTable(
                name: "ClaseAportante");

            migrationBuilder.DropTable(
                name: "TipoAportante");

            migrationBuilder.DropTable(
                name: "TipoPlanilla");

            migrationBuilder.DropTable(
                name: "SubtipoCotizante");

            migrationBuilder.DropTable(
                name: "TipoCotizante");
        }
    }
}
