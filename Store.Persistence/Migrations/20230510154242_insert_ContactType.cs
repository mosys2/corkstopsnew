using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Persistence.Migrations
{
    public partial class insert_ContactType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ContactTypes",
                columns: new[] { "Id", "CssClass", "Icon", "InsertTime", "IsRemoved", "RemoveTime", "Title", "UpdateTime", "Value" },
                values: new object[] { 1L, null, null, new DateTime(2023, 5, 10, 20, 12, 40, 845, DateTimeKind.Local).AddTicks(9978), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تلفن همراه", null, "Mobile" });

            migrationBuilder.UpdateData(
                table: "Rolls",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 10, 20, 12, 40, 845, DateTimeKind.Local).AddTicks(9764));

            migrationBuilder.UpdateData(
                table: "Rolls",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 10, 20, 12, 40, 845, DateTimeKind.Local).AddTicks(9859));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.UpdateData(
                table: "Rolls",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 9, 18, 13, 45, 881, DateTimeKind.Local).AddTicks(3367));

            migrationBuilder.UpdateData(
                table: "Rolls",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 9, 18, 13, 45, 881, DateTimeKind.Local).AddTicks(3445));
        }
    }
}
