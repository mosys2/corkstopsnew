using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Persistence.Migrations
{
    public partial class insert_Roll_to_Rolls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rolls",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "InsertTime", "Title" },
                values: new object[] { new DateTime(2023, 5, 9, 18, 13, 45, 881, DateTimeKind.Local).AddTicks(3367), "Admin" });

            migrationBuilder.InsertData(
                table: "Rolls",
                columns: new[] { "Id", "Description", "InsertTime", "IsRemoved", "RemoveTime", "RollName", "Title", "UpdateTime" },
                values: new object[] { 2L, null, new DateTime(2023, 5, 9, 18, 13, 45, 881, DateTimeKind.Local).AddTicks(3445), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Customer", "Customer", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rolls",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.UpdateData(
                table: "Rolls",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "InsertTime", "Title" },
                values: new object[] { new DateTime(2023, 5, 9, 18, 7, 18, 371, DateTimeKind.Local).AddTicks(1362), null });
        }
    }
}
