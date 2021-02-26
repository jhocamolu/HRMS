using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint12008 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoHoraExtra",
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
                    Tipo = table.Column<string>(type: "varchar(255)", nullable: false),
                    ConceptoNominaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoHoraExtra", x => x.Id);
                    table.CheckConstraint("CK_TipoHoraExtra_EstadoRegistro", "([EstadoRegistro]='Eliminado' OR [EstadoRegistro]='Inactivo' OR [EstadoRegistro]='Activo')");
                    table.CheckConstraint("CK_TipoHoraExtra_Tipo", "([Tipo]='RecargoNocturno' OR [Tipo]='HoraExtraDiurna' OR [Tipo]='HoraExtraNocturna' OR [Tipo]='HoraExtraFestivaDominicalDiurna' OR [Tipo]='HoraExtraFestivaDominicalNocturna'  OR [Tipo]='RecargoNocturnoDominicalFestivo' OR [Tipo]=' DominicalFestivoCompensado' OR [Tipo]='DominicalFestivoNoCompensado')");
                    table.ForeignKey(
                        name: "FK_TipoHoraExtra_ConceptoNomina_ConceptoNominaId",
                        column: x => x.ConceptoNominaId,
                        principalTable: "ConceptoNomina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TipoHoraExtra_ConceptoNominaId",
                table: "TipoHoraExtra",
                column: "ConceptoNominaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoHoraExtra");
        }
    }
}
