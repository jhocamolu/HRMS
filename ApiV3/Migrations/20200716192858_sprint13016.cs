using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint13016 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Adiciona",
                table: "RangoUvt",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sustrae",
                table: "RangoUvt",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidoDesde",
                table: "RangoUvt",
                type: "smalldatetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adiciona",
                table: "RangoUvt");

            migrationBuilder.DropColumn(
                name: "Sustrae",
                table: "RangoUvt");

            migrationBuilder.DropColumn(
                name: "ValidoDesde",
                table: "RangoUvt");
        }
    }
}
