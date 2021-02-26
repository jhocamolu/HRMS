using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint14014 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "RequisicionPersonal",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(30)");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAutorizacion",
                table: "RequisicionPersonal",
                type: "date",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaAutorizacion",
                table: "RequisicionPersonal");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "RequisicionPersonal",
                type: "char(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");
        }
    }
}
