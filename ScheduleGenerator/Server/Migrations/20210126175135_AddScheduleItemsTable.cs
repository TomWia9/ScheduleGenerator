using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleGenerator.Server.Migrations
{
    public partial class AddScheduleItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleItem_Schedules_ScheduleId",
                table: "ScheduleItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleItem",
                table: "ScheduleItem");

            migrationBuilder.RenameTable(
                name: "ScheduleItem",
                newName: "ScheduleItems");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleItem_ScheduleId",
                table: "ScheduleItems",
                newName: "IX_ScheduleItems_ScheduleId");

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "ScheduleItems",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoomNumber",
                table: "ScheduleItems",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Lecturer",
                table: "ScheduleItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleItems",
                table: "ScheduleItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleItems_Schedules_ScheduleId",
                table: "ScheduleItems",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleItems_Schedules_ScheduleId",
                table: "ScheduleItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleItems",
                table: "ScheduleItems");

            migrationBuilder.RenameTable(
                name: "ScheduleItems",
                newName: "ScheduleItem");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleItems_ScheduleId",
                table: "ScheduleItem",
                newName: "IX_ScheduleItem_ScheduleId");

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "ScheduleItem",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "RoomNumber",
                table: "ScheduleItem",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Lecturer",
                table: "ScheduleItem",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleItem",
                table: "ScheduleItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleItem_Schedules_ScheduleId",
                table: "ScheduleItem",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
