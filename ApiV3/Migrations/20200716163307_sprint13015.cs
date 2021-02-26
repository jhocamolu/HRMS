using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint13015 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FuncionarioAQuienReemplazaId",
                table: "RequisicionPersonal",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFin",
                table: "RequisicionPersonal",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "Cantidad",
                table: "RequisicionPersonal",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FuncionarioAQuienReemplazaId",
                table: "RequisicionPersonal",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFin",
                table: "RequisicionPersonal",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "Cantidad",
                table: "RequisicionPersonal",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
