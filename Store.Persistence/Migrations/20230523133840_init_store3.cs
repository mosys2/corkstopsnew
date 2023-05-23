using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Persistence.Migrations
{
    public partial class init_store3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 18, 8, 39, 266, DateTimeKind.Local).AddTicks(9626));

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 18, 8, 39, 266, DateTimeKind.Local).AddTicks(9640));

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 18, 8, 39, 266, DateTimeKind.Local).AddTicks(9652));

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 18, 8, 39, 266, DateTimeKind.Local).AddTicks(9662));

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 18, 8, 39, 266, DateTimeKind.Local).AddTicks(9672));

            migrationBuilder.UpdateData(
                table: "Rolls",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 18, 8, 39, 266, DateTimeKind.Local).AddTicks(9550));

            migrationBuilder.UpdateData(
                table: "Rolls",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 18, 8, 39, 266, DateTimeKind.Local).AddTicks(9612));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 18, 4, 18, 570, DateTimeKind.Local).AddTicks(83));

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 18, 4, 18, 570, DateTimeKind.Local).AddTicks(102));

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 18, 4, 18, 570, DateTimeKind.Local).AddTicks(115));

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 18, 4, 18, 570, DateTimeKind.Local).AddTicks(128));

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 18, 4, 18, 570, DateTimeKind.Local).AddTicks(139));

            migrationBuilder.UpdateData(
                table: "Rolls",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 18, 4, 18, 569, DateTimeKind.Local).AddTicks(9933));

            migrationBuilder.UpdateData(
                table: "Rolls",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 18, 4, 18, 569, DateTimeKind.Local).AddTicks(9996));
        }
    }
}
