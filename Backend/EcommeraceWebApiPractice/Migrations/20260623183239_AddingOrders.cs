using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommeraceWebApiPractice.Migrations
{
    /// <inheritdoc />
    public partial class AddingOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderId",
                table: "cartItem",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cartItem_OrderId",
                table: "cartItem",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_cartItem_Orders_OrderId",
                table: "cartItem",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartItem_Orders_OrderId",
                table: "cartItem");

            migrationBuilder.DropIndex(
                name: "IX_cartItem_OrderId",
                table: "cartItem");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "cartItem");
        }
    }
}
