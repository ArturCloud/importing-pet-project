using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataImportProj.Migrations
{
    /// <inheritdoc />
    public partial class addingOldIds2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OldLanguageCode",
                table: "ProductTranslations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTranslations_OldProductId_OldLanguageCode",
                table: "ProductTranslations",
                columns: new[] { "OldProductId", "OldLanguageCode" },
                unique: true,
                filter: "[OldProductId] IS NOT NULL AND [OldLanguageCode] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductTranslations_OldProductId_OldLanguageCode",
                table: "ProductTranslations");

            migrationBuilder.AlterColumn<string>(
                name: "OldLanguageCode",
                table: "ProductTranslations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
