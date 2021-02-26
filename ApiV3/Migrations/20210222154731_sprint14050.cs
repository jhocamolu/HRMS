using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiV3.Migrations
{
    public partial class sprint14050 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreadoPor",
                table: "CargoCentroCosto",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EliminadoPor",
                table: "CargoCentroCosto",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstadoRegistro",
                table: "CargoCentroCosto",
                type: "char(10)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "CargoCentroCosto",
                type: "smalldatetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEliminacion",
                table: "CargoCentroCosto",
                type: "smalldatetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "CargoCentroCosto",
                type: "smalldatetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "CargoCentroCosto",
                type: "varchar(255)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreadoPor",
                table: "CargoCentroCosto");

            migrationBuilder.DropColumn(
                name: "EliminadoPor",
                table: "CargoCentroCosto");

            migrationBuilder.DropColumn(
                name: "EstadoRegistro",
                table: "CargoCentroCosto");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "CargoCentroCosto");

            migrationBuilder.DropColumn(
                name: "FechaEliminacion",
                table: "CargoCentroCosto");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "CargoCentroCosto");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "CargoCentroCosto");
        }
    }
}
