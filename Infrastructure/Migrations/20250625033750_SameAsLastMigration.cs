using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShop.API.Migrations
{
    /// <inheritdoc />
    public partial class SameAsLastMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4c488a26-11ba-45d2-9820-f07e7b23d7e6", "89907a64-75a7-4940-a776-7f96cfbe3f46", "Client", "client" },
                    { "86cef419-8767-4e98-bf70-5e545e4828db", "0d193972-2faa-4ad6-ae29-9346c2273879", "SalesManagers", "sales_managers" },
                    { "a44c86f2-e782-483a-afde-5430be942ad3", "1ddc4b35-0cd8-4eaa-8979-dd4f3e86a0d2", "InventoryManager", "inventory_manager" }
                });
        }
    }
}
