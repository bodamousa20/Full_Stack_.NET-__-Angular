using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommeraceWebApiPractice.Migrations
{
    /// <inheritdoc />
    public partial class addingImageUrlCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "photos");
        }
    }
}
