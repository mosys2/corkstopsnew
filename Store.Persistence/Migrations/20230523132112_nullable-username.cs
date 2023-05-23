using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Persistence.Migrations
{
    public partial class nullableusername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 17, 51, 11, 630, DateTimeKind.Local).AddTicks(8561));

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 17, 51, 11, 630, DateTimeKind.Local).AddTicks(8580));

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 17, 51, 11, 630, DateTimeKind.Local).AddTicks(8592));

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 17, 51, 11, 630, DateTimeKind.Local).AddTicks(8604));

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 17, 51, 11, 630, DateTimeKind.Local).AddTicks(8616));

            migrationBuilder.UpdateData(
                table: "Rolls",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 17, 51, 11, 630, DateTimeKind.Local).AddTicks(8411));

            migrationBuilder.UpdateData(
                table: "Rolls",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 23, 17, 51, 11, 630, DateTimeKind.Local).AddTicks(8476));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 21, 19, 47, 28, 425, DateTimeKind.Local).AddTicks(5184));

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 21, 19, 47, 28, 425, DateTimeKind.Local).AddTicks(5201));

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 21, 19, 47, 28, 425, DateTimeKind.Local).AddTicks(5213));

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 21, 19, 47, 28, 425, DateTimeKind.Local).AddTicks(5224));

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 21, 19, 47, 28, 425, DateTimeKind.Local).AddTicks(5235));

            migrationBuilder.UpdateData(
                table: "Rolls",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 21, 19, 47, 28, 425, DateTimeKind.Local).AddTicks(5100));

            migrationBuilder.UpdateData(
                table: "Rolls",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 21, 19, 47, 28, 425, DateTimeKind.Local).AddTicks(5169));
        }
    }
}
