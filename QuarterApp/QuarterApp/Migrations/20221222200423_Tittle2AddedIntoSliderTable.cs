using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuarterApp.Migrations
{
    public partial class Tittle2AddedIntoSliderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tittle",
                table: "Sliders",
                newName: "Tittle2");

            migrationBuilder.AddColumn<string>(
                name: "Tittle1",
                table: "Sliders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tittle1",
                table: "Sliders");

            migrationBuilder.RenameColumn(
                name: "Tittle2",
                table: "Sliders",
                newName: "Tittle");
        }
    }
}
