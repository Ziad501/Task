using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShop.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
