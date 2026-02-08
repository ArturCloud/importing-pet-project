using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataImportProj.Migrations
{
    /// <inheritdoc />
    public partial class addingOldIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OldLanguageCode",
                table: "ProductTranslations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OldProductId",
                table: "ProductTranslations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OldId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OldCustomerId",
                table: "ProductCustomers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OldProductId",
                table: "ProductCustomers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OldId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCustomers_OldCustomerId_OldProductId",
                table: "ProductCustomers",
                columns: new[] { "OldCustomerId", "OldProductId" },
                unique: true,
                filter: "[OldCustomerId] IS NOT NULL AND [OldProductId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductCustomers_OldCustomerId_OldProductId",
                table: "ProductCustomers");

            migrationBuilder.DropColumn(
                name: "OldLanguageCode",
                table: "ProductTranslations");

            migrationBuilder.DropColumn(
                name: "OldProductId",
                table: "ProductTranslations");

            migrationBuilder.DropColumn(
                name: "OldId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OldCustomerId",
                table: "ProductCustomers");

            migrationBuilder.DropColumn(
                name: "OldProductId",
                table: "ProductCustomers");

            migrationBuilder.DropColumn(
                name: "OldId",
                table: "Customers");
        }
    }
}
