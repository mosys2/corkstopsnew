using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Persistence.Migrations
{
    public partial class chenage_Decimal_To_Doble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rolse",
                keyColumn: "Id",
                keyValue: "322f4ac3-9e1c-4f53-919b-4e7d43c30065");

            migrationBuilder.DeleteData(
                table: "Rolse",
                keyColumn: "Id",
                keyValue: "5fb47c1d-3957-4922-a8b5-8c07cf00d9e3");

            migrationBuilder.AlterColumn<double>(
                name: "PostageFeeBasedQuantity",
                table: "Products",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "PostageFee",
                table: "Products",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "Rolse",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "InsertTime", "IsRemoved", "MyTitle", "Name", "NormalizedName", "RemoveTime", "UpdateTime" },
                values: new object[] { "7157ee61-943b-40b9-bd3f-6506b902e112", null, null, "Role", null, false, null, "Admin", "ADMIN", null, null });

            migrationBuilder.InsertData(
                table: "Rolse",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "InsertTime", "IsRemoved", "MyTitle", "Name", "NormalizedName", "RemoveTime", "UpdateTime" },
                values: new object[] { "f764ad3c-bbdb-4cbd-a594-a7bf9a7164ef", null, null, "Role", null, false, null, "Customer", "CUSTOMER", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rolse",
                keyColumn: "Id",
                keyValue: "7157ee61-943b-40b9-bd3f-6506b902e112");

            migrationBuilder.DeleteData(
                table: "Rolse",
                keyColumn: "Id",
                keyValue: "f764ad3c-bbdb-4cbd-a594-a7bf9a7164ef");

            migrationBuilder.AlterColumn<decimal>(
                name: "PostageFeeBasedQuantity",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "PostageFee",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.InsertData(
                table: "Rolse",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "InsertTime", "IsRemoved", "MyTitle", "Name", "NormalizedName", "RemoveTime", "UpdateTime" },
                values: new object[] { "322f4ac3-9e1c-4f53-919b-4e7d43c30065", null, null, "Role", null, false, null, "Customer", "CUSTOMER", null, null });

            migrationBuilder.InsertData(
                table: "Rolse",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "InsertTime", "IsRemoved", "MyTitle", "Name", "NormalizedName", "RemoveTime", "UpdateTime" },
                values: new object[] { "5fb47c1d-3957-4922-a8b5-8c07cf00d9e3", null, null, "Role", null, false, null, "Admin", "ADMIN", null, null });
        }
    }
}
