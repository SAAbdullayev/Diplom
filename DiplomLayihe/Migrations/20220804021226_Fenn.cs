using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomLayihe.Migrations
{
    public partial class Fenn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ixtisaslar_Gruplar_GruplarId",
                table: "Ixtisaslar");

            migrationBuilder.DropIndex(
                name: "IX_Ixtisaslar_GruplarId",
                table: "Ixtisaslar");

            migrationBuilder.DropColumn(
                name: "GruplarId",
                table: "Ixtisaslar");

            migrationBuilder.CreateTable(
                name: "TedrisFennleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedById = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TedrisFennleri", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TedrisFennleri");

            migrationBuilder.AddColumn<int>(
                name: "GruplarId",
                table: "Ixtisaslar",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ixtisaslar_GruplarId",
                table: "Ixtisaslar",
                column: "GruplarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ixtisaslar_Gruplar_GruplarId",
                table: "Ixtisaslar",
                column: "GruplarId",
                principalTable: "Gruplar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
