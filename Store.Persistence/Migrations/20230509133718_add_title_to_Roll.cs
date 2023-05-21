using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Persistence.Migrations
{
    public partial class add_title_to_Roll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Rolls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Rolls",
                columns: new[] { "Id", "Description", "InsertTime", "IsRemoved", "RemoveTime", "RollName", "Title", "UpdateTime" },
                values: new object[] { 1L, null, new DateTime(2023, 5, 9, 18, 7, 18, 371, DateTimeKind.Local).AddTicks(1362), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rolls",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Rolls");
        }
    }
}
