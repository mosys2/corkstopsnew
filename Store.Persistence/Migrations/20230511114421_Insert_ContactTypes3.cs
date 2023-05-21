using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Persistence.Migrations
{
    public partial class Insert_ContactTypes3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Icon", "InsertTime" },
                values: new object[] { "smartphone", new DateTime(2023, 5, 11, 16, 14, 20, 999, DateTimeKind.Local).AddTicks(8617) });

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Icon", "InsertTime" },
                values: new object[] { "phone", new DateTime(2023, 5, 11, 16, 14, 20, 999, DateTimeKind.Local).AddTicks(8634) });

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Icon", "InsertTime" },
                values: new object[] { "mail", new DateTime(2023, 5, 11, 16, 14, 20, 999, DateTimeKind.Local).AddTicks(8647) });

            migrationBuilder.InsertData(
                table: "ContactTypes",
                columns: new[] { "Id", "CssClass", "Icon", "InsertTime", "IsRemoved", "RemoveTime", "Title", "UpdateTime", "Value" },
                values: new object[,]
                {
                    { 4L, null, "map-pin", new DateTime(2023, 5, 11, 16, 14, 20, 999, DateTimeKind.Local).AddTicks(8660), false, null, "Address", null, "Address" },
                    { 5L, null, "home", new DateTime(2023, 5, 11, 16, 14, 20, 999, DateTimeKind.Local).AddTicks(8671), false, null, "PostalCode", null, "PostalCode" }
                });

            migrationBuilder.UpdateData(
                table: "Rolls",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 11, 16, 14, 20, 999, DateTimeKind.Local).AddTicks(8525));

            migrationBuilder.UpdateData(
                table: "Rolls",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 11, 16, 14, 20, 999, DateTimeKind.Local).AddTicks(8595));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Icon", "InsertTime" },
                values: new object[] { null, new DateTime(2023, 5, 11, 11, 48, 10, 995, DateTimeKind.Local).AddTicks(4211) });

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Icon", "InsertTime" },
                values: new object[] { null, new DateTime(2023, 5, 11, 11, 48, 10, 995, DateTimeKind.Local).AddTicks(4248) });

            migrationBuilder.UpdateData(
                table: "ContactTypes",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Icon", "InsertTime" },
                values: new object[] { null, new DateTime(2023, 5, 11, 11, 48, 10, 995, DateTimeKind.Local).AddTicks(4269) });

            migrationBuilder.UpdateData(
                table: "Rolls",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 11, 11, 48, 10, 995, DateTimeKind.Local).AddTicks(4061));

            migrationBuilder.UpdateData(
                table: "Rolls",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2023, 5, 11, 11, 48, 10, 995, DateTimeKind.Local).AddTicks(4172));
        }
    }
}
