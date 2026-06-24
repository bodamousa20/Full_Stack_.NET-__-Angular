using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommeraceWebApiPractice.Migrations
{
    /// <inheritdoc />
    public partial class addingUserIDcol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "Orders");
        }
    }
}
