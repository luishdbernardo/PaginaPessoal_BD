using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaginaPessoal_BD.Migrations.PaginaPessoalBD
{
    public partial class FotoExperiencia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "Experiencia",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Experiencia");
        }
    }
}
