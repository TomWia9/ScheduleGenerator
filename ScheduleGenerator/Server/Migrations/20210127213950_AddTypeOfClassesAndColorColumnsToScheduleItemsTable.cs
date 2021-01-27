using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleGenerator.Server.Migrations
{
    public partial class AddTypeOfClassesAndColorColumnsToScheduleItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Color",
                table: "ScheduleItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfClasses",
                table: "ScheduleItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "ScheduleItems");

            migrationBuilder.DropColumn(
                name: "TypeOfClasses",
                table: "ScheduleItems");
        }
    }
}
