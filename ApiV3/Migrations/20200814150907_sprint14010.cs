using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14010 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "TipoLiquidacion",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "AplicaPila",
                table: "TipoLiquidacion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ConceptoNominaAgrupadorId",
                table: "TipoLiquidacion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Contabiliza",
                table: "TipoLiquidacion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Proceso",
                table: "TipoLiquidacion",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TipoLiquidacion_ConceptoNominaAgrupadorId",
                table: "TipoLiquidacion",
                column: "ConceptoNominaAgrupadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoLiquidacion_ConceptoNomina_ConceptoNominaAgrupadorId",
                table: "TipoLiquidacion",
                column: "ConceptoNominaAgrupadorId",
                principalTable: "ConceptoNomina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoLiquidacion_ConceptoNomina_ConceptoNominaAgrupadorId",
                table: "TipoLiquidacion");

            migrationBuilder.DropIndex(
                name: "IX_TipoLiquidacion_ConceptoNominaAgrupadorId",
                table: "TipoLiquidacion");

            migrationBuilder.DropColumn(
                name: "AplicaPila",
                table: "TipoLiquidacion");

            migrationBuilder.DropColumn(
                name: "ConceptoNominaAgrupadorId",
                table: "TipoLiquidacion");

            migrationBuilder.DropColumn(
                name: "Contabiliza",
                table: "TipoLiquidacion");

            migrationBuilder.DropColumn(
                name: "Proceso",
                table: "TipoLiquidacion");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "TipoLiquidacion",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
