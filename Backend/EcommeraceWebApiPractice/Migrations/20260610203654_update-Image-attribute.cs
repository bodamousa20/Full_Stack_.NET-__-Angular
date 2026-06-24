using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommeraceWebApiPractice.Migrations
{
    /// <inheritdoc />
    public partial class updateImageattribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "photos",
                newName: "imageUrl");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "photos",
                newName: "fileName");

            migrationBuilder.AddColumn<string>(
                name: "fileExtention",
                table: "photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "fileSize",
                table: "photos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fileExtention",
                table: "photos");

            migrationBuilder.DropColumn(
                name: "fileSize",
                table: "photos");

            migrationBuilder.RenameColumn(
                name: "imageUrl",
                table: "photos",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "fileName",
                table: "photos",
                newName: "Name");
        }
    }
}
