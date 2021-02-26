using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint13028 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFinalizacion",
                table: "Novedad",
                type: "smalldatetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFinalizacion",
                table: "Novedad",
                type: "smalldatetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime",
                oldNullable: true);
        }
    }
}
