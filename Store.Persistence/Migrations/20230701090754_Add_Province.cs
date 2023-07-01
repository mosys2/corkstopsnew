using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Persistence.Migrations
{
    public partial class Add_Province : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rolse",
                keyColumn: "Id",
                keyValue: "0301d1d8-e124-4da0-843a-e1dddbacbed8");

            migrationBuilder.DeleteData(
                table: "Rolse",
                keyColumn: "Id",
                keyValue: "21d0cf74-ce83-4e4e-872f-14c630f20f4f");

            migrationBuilder.InsertData(
                table: "Rolse",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "InsertTime", "IsRemoved", "MyTitle", "Name", "NormalizedName", "RemoveTime", "UpdateTime" },
                values: new object[] { "3cabb961-c544-47af-afdd-ea79a6b765f4", null, null, "Role", null, false, null, "Customer", "CUSTOMER", null, null });

            migrationBuilder.InsertData(
                table: "Rolse",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "InsertTime", "IsRemoved", "MyTitle", "Name", "NormalizedName", "RemoveTime", "UpdateTime" },
                values: new object[] { "6c6a17ac-01ee-46c3-976b-5761c7365dc2", null, null, "Role", null, false, null, "Admin", "ADMIN", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rolse",
                keyColumn: "Id",
                keyValue: "3cabb961-c544-47af-afdd-ea79a6b765f4");

            migrationBuilder.DeleteData(
                table: "Rolse",
                keyColumn: "Id",
                keyValue: "6c6a17ac-01ee-46c3-976b-5761c7365dc2");

            migrationBuilder.InsertData(
                table: "Rolse",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "InsertTime", "IsRemoved", "MyTitle", "Name", "NormalizedName", "RemoveTime", "UpdateTime" },
                values: new object[] { "0301d1d8-e124-4da0-843a-e1dddbacbed8", null, null, "Role", null, false, null, "Customer", "CUSTOMER", null, null });

            migrationBuilder.InsertData(
                table: "Rolse",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "InsertTime", "IsRemoved", "MyTitle", "Name", "NormalizedName", "RemoveTime", "UpdateTime" },
                values: new object[] { "21d0cf74-ce83-4e4e-872f-14c630f20f4f", null, null, "Role", null, false, null, "Admin", "ADMIN", null, null });
        }
    }
}
