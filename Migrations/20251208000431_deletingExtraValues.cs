using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataImportProj.Migrations
{
    /// <inheritdoc />
    public partial class deletingExtraValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductTranslations_OldProductId_OldLanguageCode",
                table: "ProductTranslations");

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
                name: "OldCustomerId",
                table: "ProductCustomers");

            migrationBuilder.DropColumn(
                name: "OldProductId",
                table: "ProductCustomers");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "BestBefore",
                table: "Products",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductCustomers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Products_OldId",
                table: "Products",
                column: "OldId",
                unique: true,
                filter: "[OldId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_OldId",
                table: "Customers",
                column: "OldId",
                unique: true,
                filter: "[OldId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_OldId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Customers_OldId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProductCustomers");

            migrationBuilder.AddColumn<string>(
                name: "OldLanguageCode",
                table: "ProductTranslations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OldProductId",
                table: "ProductTranslations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BestBefore",
                table: "Products",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductTranslations_OldProductId_OldLanguageCode",
                table: "ProductTranslations",
                columns: new[] { "OldProductId", "OldLanguageCode" },
                unique: true,
                filter: "[OldProductId] IS NOT NULL AND [OldLanguageCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCustomers_OldCustomerId_OldProductId",
                table: "ProductCustomers",
                columns: new[] { "OldCustomerId", "OldProductId" },
                unique: true,
                filter: "[OldCustomerId] IS NOT NULL AND [OldProductId] IS NOT NULL");
        }
    }
}
