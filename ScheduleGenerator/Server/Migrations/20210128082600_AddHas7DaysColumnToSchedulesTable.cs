using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleGenerator.Server.Migrations
{
    public partial class AddHas7DaysColumnToSchedulesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Has7Days",
                table: "Schedules",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Has7Days",
                table: "Schedules");
        }
    }
}
