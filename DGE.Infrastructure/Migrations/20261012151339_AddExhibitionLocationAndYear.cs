using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DGE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExhibitionLocationAndYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Exhibitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "Exhibitions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Exhibitions");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Exhibitions");
        }
    }
}
