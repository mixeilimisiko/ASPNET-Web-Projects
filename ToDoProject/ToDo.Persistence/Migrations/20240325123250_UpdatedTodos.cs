using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Persistence.Migrations
{
    public partial class UpdatedTodos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Users",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 25, 16, 32, 49, 936, DateTimeKind.Local).AddTicks(6869),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 3, 24, 22, 55, 27, 671, DateTimeKind.Local).AddTicks(2666));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 25, 16, 32, 49, 936, DateTimeKind.Local).AddTicks(6708),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 3, 24, 22, 55, 27, 671, DateTimeKind.Local).AddTicks(2467));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "ToDos",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 25, 16, 32, 49, 936, DateTimeKind.Local).AddTicks(5201),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 3, 24, 22, 55, 27, 671, DateTimeKind.Local).AddTicks(879));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ToDos",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 25, 16, 32, 49, 936, DateTimeKind.Local).AddTicks(5004),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 3, 24, 22, 55, 27, 671, DateTimeKind.Local).AddTicks(729));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Subtasks",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 25, 16, 32, 49, 936, DateTimeKind.Local).AddTicks(3034),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 3, 24, 22, 55, 27, 670, DateTimeKind.Local).AddTicks(9504));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Subtasks",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 25, 16, 32, 49, 936, DateTimeKind.Local).AddTicks(2817),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 3, 24, 22, 55, 27, 670, DateTimeKind.Local).AddTicks(9202));

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_Title_UserId",
                table: "ToDos",
                columns: new[] { "Title", "UserId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ToDos_Title_UserId",
                table: "ToDos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Users",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 24, 22, 55, 27, 671, DateTimeKind.Local).AddTicks(2666),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 3, 25, 16, 32, 49, 936, DateTimeKind.Local).AddTicks(6869));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 24, 22, 55, 27, 671, DateTimeKind.Local).AddTicks(2467),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 3, 25, 16, 32, 49, 936, DateTimeKind.Local).AddTicks(6708));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "ToDos",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 24, 22, 55, 27, 671, DateTimeKind.Local).AddTicks(879),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 3, 25, 16, 32, 49, 936, DateTimeKind.Local).AddTicks(5201));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ToDos",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 24, 22, 55, 27, 671, DateTimeKind.Local).AddTicks(729),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 3, 25, 16, 32, 49, 936, DateTimeKind.Local).AddTicks(5004));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Subtasks",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 24, 22, 55, 27, 670, DateTimeKind.Local).AddTicks(9504),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 3, 25, 16, 32, 49, 936, DateTimeKind.Local).AddTicks(3034));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Subtasks",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 24, 22, 55, 27, 670, DateTimeKind.Local).AddTicks(9202),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 3, 25, 16, 32, 49, 936, DateTimeKind.Local).AddTicks(2817));
        }
    }
}
