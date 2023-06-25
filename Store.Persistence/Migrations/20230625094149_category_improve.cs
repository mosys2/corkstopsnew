using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Persistence.Migrations
{
    public partial class category_improve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rolse",
                keyColumn: "Id",
                keyValue: "7157ee61-943b-40b9-bd3f-6506b902e112");

            migrationBuilder.DeleteData(
                table: "Rolse",
                keyColumn: "Id",
                keyValue: "f764ad3c-bbdb-4cbd-a594-a7bf9a7164ef");

            migrationBuilder.InsertData(
                table: "Rolse",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "InsertTime", "IsRemoved", "MyTitle", "Name", "NormalizedName", "RemoveTime", "UpdateTime" },
                values: new object[] { "2cddcf97-6535-4971-9946-389db55a75f8", null, null, "Role", null, false, null, "Admin", "ADMIN", null, null });

            migrationBuilder.InsertData(
                table: "Rolse",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "InsertTime", "IsRemoved", "MyTitle", "Name", "NormalizedName", "RemoveTime", "UpdateTime" },
                values: new object[] { "6c972e57-e696-4bcf-afb3-09efcd8e626d", null, null, "Role", null, false, null, "Customer", "CUSTOMER", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rolse",
                keyColumn: "Id",
                keyValue: "2cddcf97-6535-4971-9946-389db55a75f8");

            migrationBuilder.DeleteData(
                table: "Rolse",
                keyColumn: "Id",
                keyValue: "6c972e57-e696-4bcf-afb3-09efcd8e626d");

            migrationBuilder.InsertData(
                table: "Rolse",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "InsertTime", "IsRemoved", "MyTitle", "Name", "NormalizedName", "RemoveTime", "UpdateTime" },
                values: new object[] { "7157ee61-943b-40b9-bd3f-6506b902e112", null, null, "Role", null, false, null, "Admin", "ADMIN", null, null });

            migrationBuilder.InsertData(
                table: "Rolse",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "InsertTime", "IsRemoved", "MyTitle", "Name", "NormalizedName", "RemoveTime", "UpdateTime" },
                values: new object[] { "f764ad3c-bbdb-4cbd-a594-a7bf9a7164ef", null, null, "Role", null, false, null, "Customer", "CUSTOMER", null, null });
        }
    }
}
