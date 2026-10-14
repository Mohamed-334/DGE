using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DGE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductHomeFieldsAndContactUsIsRead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ShowOnHome",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Products",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "ContactUsMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            // Existing products get a unique placeholder slug so they are reachable by slug; it can be renamed through Update
            migrationBuilder.Sql("UPDATE [Products] SET [Slug] = CONCAT('product-', [Id]) WHERE [Slug] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Slug",
                table: "Products",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Slug",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShowOnHome",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "ContactUsMessages");
        }
    }
}
