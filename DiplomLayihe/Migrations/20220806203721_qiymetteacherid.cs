using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomLayihe.Migrations
{
    public partial class qiymetteacherid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Qiymetler",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Qiymetler");
        }
    }
}
