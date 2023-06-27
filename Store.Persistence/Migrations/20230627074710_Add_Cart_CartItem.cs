using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Persistence.Migrations
{
    public partial class Add_Cart_CartItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_UserId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Rolse",
                keyColumn: "Id",
                keyValue: "2cddcf97-6535-4971-9946-389db55a75f8");

            migrationBuilder.DeleteData(
                table: "Rolse",
                keyColumn: "Id",
                keyValue: "6c972e57-e696-4bcf-afb3-09efcd8e626d");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BrowserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Finished = table.Column<bool>(type: "bit", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CartId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Rolse",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "InsertTime", "IsRemoved", "MyTitle", "Name", "NormalizedName", "RemoveTime", "UpdateTime" },
                values: new object[] { "0301d1d8-e124-4da0-843a-e1dddbacbed8", null, null, "Role", null, false, null, "Customer", "CUSTOMER", null, null });

            migrationBuilder.InsertData(
                table: "Rolse",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "InsertTime", "IsRemoved", "MyTitle", "Name", "NormalizedName", "RemoveTime", "UpdateTime" },
                values: new object[] { "21d0cf74-ce83-4e4e-872f-14c630f20f4f", null, null, "Role", null, false, null, "Admin", "ADMIN", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_UserId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DeleteData(
                table: "Rolse",
                keyColumn: "Id",
                keyValue: "0301d1d8-e124-4da0-843a-e1dddbacbed8");

            migrationBuilder.DeleteData(
                table: "Rolse",
                keyColumn: "Id",
                keyValue: "21d0cf74-ce83-4e4e-872f-14c630f20f4f");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Rolse",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "InsertTime", "IsRemoved", "MyTitle", "Name", "NormalizedName", "RemoveTime", "UpdateTime" },
                values: new object[] { "2cddcf97-6535-4971-9946-389db55a75f8", null, null, "Role", null, false, null, "Admin", "ADMIN", null, null });

            migrationBuilder.InsertData(
                table: "Rolse",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "InsertTime", "IsRemoved", "MyTitle", "Name", "NormalizedName", "RemoveTime", "UpdateTime" },
                values: new object[] { "6c972e57-e696-4bcf-afb3-09efcd8e626d", null, null, "Role", null, false, null, "Customer", "CUSTOMER", null, null });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
