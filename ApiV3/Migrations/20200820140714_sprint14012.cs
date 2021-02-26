using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ApiV3.Migrations
{
    public partial class sprint14012 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_EstadoCivil_EstadoCivilId",
                table: "HojaDeVida");

            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_Ocupacion_OcupacionId",
                table: "HojaDeVida");

            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_TipoSangre_TipoSangreId",
                table: "HojaDeVida");

            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_TipoVivienda_TipoViviendaId",
                table: "HojaDeVida");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Candidato_Estado",
                table: "Candidato");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_Candidato_Estado",
                table: "Candidato",
                sql: "([Estado]='Postulado' OR [Estado]='Descartado' OR [Estado]='Competente' OR [Estado]='Elegible' OR [Estado]='NoApto' OR [Estado]='Seleccionado')");

            migrationBuilder.AlterColumn<bool>(
                name: "UsaLentes",
                table: "HojaDeVida",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "TipoViviendaId",
                table: "HojaDeVida",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TipoSangreId",
                table: "HojaDeVida",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "Pensionado",
                table: "HojaDeVida",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "OcupacionId",
                table: "HojaDeVida",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Nit",
                table: "HojaDeVida",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaNacimiento",
                table: "HojaDeVida",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaExpedicionDocumento",
                table: "HojaDeVida",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "EstadoCivilId",
                table: "HojaDeVida",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DivisionPoliticaNivel2ResidenciaId",
                table: "HojaDeVida",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DivisionPoliticaNivel2OrigenId",
                table: "HojaDeVida",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DivisionPoliticaNivel2ExpedicionDocumentoId",
                table: "HojaDeVida",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DigitoVerificacion",
                table: "HojaDeVida",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_EstadoCivil_EstadoCivilId",
                table: "HojaDeVida",
                column: "EstadoCivilId",
                principalTable: "EstadoCivil",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_Ocupacion_OcupacionId",
                table: "HojaDeVida",
                column: "OcupacionId",
                principalTable: "Ocupacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_TipoSangre_TipoSangreId",
                table: "HojaDeVida",
                column: "TipoSangreId",
                principalTable: "TipoSangre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_TipoVivienda_TipoViviendaId",
                table: "HojaDeVida",
                column: "TipoViviendaId",
                principalTable: "TipoVivienda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_EstadoCivil_EstadoCivilId",
                table: "HojaDeVida");

            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_Ocupacion_OcupacionId",
                table: "HojaDeVida");

            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_TipoSangre_TipoSangreId",
                table: "HojaDeVida");

            migrationBuilder.DropForeignKey(
                name: "FK_HojaDeVida_TipoVivienda_TipoViviendaId",
                table: "HojaDeVida");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Candidato_Estado",
                table: "Candidato");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_Candidato_Estado",
                table: "Candidato",
                sql: "([Estado]='Postulado' OR [Estado]='Descartado' OR [Estado]='Competente' OR [Estado]='Elegible' OR [Estado]='NoApto' OR [Estado]='Seleccionado' OR [Estado]='Reprobado')");

            migrationBuilder.AlterColumn<bool>(
                name: "UsaLentes",
                table: "HojaDeVida",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TipoViviendaId",
                table: "HojaDeVida",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TipoSangreId",
                table: "HojaDeVida",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Pensionado",
                table: "HojaDeVida",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OcupacionId",
                table: "HojaDeVida",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nit",
                table: "HojaDeVida",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaNacimiento",
                table: "HojaDeVida",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaExpedicionDocumento",
                table: "HojaDeVida",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EstadoCivilId",
                table: "HojaDeVida",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DivisionPoliticaNivel2ResidenciaId",
                table: "HojaDeVida",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DivisionPoliticaNivel2OrigenId",
                table: "HojaDeVida",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DivisionPoliticaNivel2ExpedicionDocumentoId",
                table: "HojaDeVida",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DigitoVerificacion",
                table: "HojaDeVida",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_EstadoCivil_EstadoCivilId",
                table: "HojaDeVida",
                column: "EstadoCivilId",
                principalTable: "EstadoCivil",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_Ocupacion_OcupacionId",
                table: "HojaDeVida",
                column: "OcupacionId",
                principalTable: "Ocupacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_TipoSangre_TipoSangreId",
                table: "HojaDeVida",
                column: "TipoSangreId",
                principalTable: "TipoSangre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HojaDeVida_TipoVivienda_TipoViviendaId",
                table: "HojaDeVida",
                column: "TipoViviendaId",
                principalTable: "TipoVivienda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
