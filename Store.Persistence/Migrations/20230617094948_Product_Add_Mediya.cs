using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Persistence.Migrations
{
    public partial class Product_Add_Mediya : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Rolse",
                keyColumn: "Id",
                keyValue: "605b0e51-0317-4c03-b382-ceb275be5739");

            migrationBuilder.DeleteData(
                table: "Rolse",
                keyColumn: "Id",
                keyValue: "90a2fab7-1b90-42da-ac78-4d02301c9b3d");

            migrationBuilder.RenameColumn(
                name: "Slag",
                table: "Products",
                newName: "Slug");

            migrationBuilder.AlterColumn<string>(
                name: "BrandId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaType = table.Column<int>(type: "int", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medias_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Rolse",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "InsertTime", "IsRemoved", "MyTitle", "Name", "NormalizedName", "RemoveTime", "UpdateTime" },
                values: new object[] { "322f4ac3-9e1c-4f53-919b-4e7d43c30065", null, null, "Role", null, false, null, "Customer", "CUSTOMER", null, null });

            migrationBuilder.InsertData(
                table: "Rolse",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "InsertTime", "IsRemoved", "MyTitle", "Name", "NormalizedName", "RemoveTime", "UpdateTime" },
                values: new object[] { "5fb47c1d-3957-4922-a8b5-8c07cf00d9e3", null, null, "Role", null, false, null, "Admin", "ADMIN", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Medias_ProductId",
                table: "Medias",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DeleteData(
                table: "Rolse",
                keyColumn: "Id",
                keyValue: "322f4ac3-9e1c-4f53-919b-4e7d43c30065");

            migrationBuilder.DeleteData(
                table: "Rolse",
                keyColumn: "Id",
                keyValue: "5fb47c1d-3957-4922-a8b5-8c07cf00d9e3");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "Products",
                newName: "Slag");

            migrationBuilder.AlterColumn<string>(
                name: "BrandId",
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
                values: new object[] { "605b0e51-0317-4c03-b382-ceb275be5739", null, null, "Role", null, false, null, "Customer", "CUSTOMER", null, null });

            migrationBuilder.InsertData(
                table: "Rolse",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "InsertTime", "IsRemoved", "MyTitle", "Name", "NormalizedName", "RemoveTime", "UpdateTime" },
                values: new object[] { "90a2fab7-1b90-42da-ac78-4d02301c9b3d", null, null, "Role", null, false, null, "Admin", "ADMIN", null, null });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
