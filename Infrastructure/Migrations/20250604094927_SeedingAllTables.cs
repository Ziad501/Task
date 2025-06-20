using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShop.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4206c3a4-1f96-4d05-ad34-2368691de567");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab036215-a8ae-49c4-bb77-a4bdc5e54932");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0f4ea50-18be-4e67-b639-71c2c114f765");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4c488a26-11ba-45d2-9820-f07e7b23d7e6", "89907a64-75a7-4940-a776-7f96cfbe3f46", "Client", "client" },
                    { "86cef419-8767-4e98-bf70-5e545e4828db", "0d193972-2faa-4ad6-ae29-9346c2273879", "SalesManagers", "sales_managers" },
                    { "a44c86f2-e782-483a-afde-5430be942ad3", "1ddc4b35-0cd8-4eaa-8979-dd4f3e86a0d2", "InventoryManager", "inventory_manager" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Doors" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Paints" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Title" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("11111111-1111-1111-1111-111111111111"), "High-quality steel door for security", "images/door1.jpg", "Steel Security Door" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new Guid("22222222-2222-2222-2222-222222222222"), "Matte white paint for walls", "images/paint1.jpg", "Interior White Paint" }
                });

            migrationBuilder.InsertData(
                table: "ProductOptions",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), 3500m, new Guid("33333333-3333-3333-3333-333333333333"), "100x210 cm" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), 750m, new Guid("44444444-4444-4444-4444-444444444444"), "5 Litre" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c488a26-11ba-45d2-9820-f07e7b23d7e6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86cef419-8767-4e98-bf70-5e545e4828db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a44c86f2-e782-483a-afde-5430be942ad3");

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4206c3a4-1f96-4d05-ad34-2368691de567", "66e323c1-fe75-4610-a768-c0d1b4972c6b", "Client", "client" },
                    { "ab036215-a8ae-49c4-bb77-a4bdc5e54932", "69a5616f-1435-44c2-a390-84a2e6b2935c", "SalesManagers", "sales_managers" },
                    { "d0f4ea50-18be-4e67-b639-71c2c114f765", "1647cd17-8ede-4ffd-adbc-e0bc90438573", "InventoryManager", "inventory_manager" }
                });
        }
    }
}
