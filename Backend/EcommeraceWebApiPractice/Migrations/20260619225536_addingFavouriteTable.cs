using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommeraceWebApiPractice.Migrations
{
    /// <inheritdoc />
    public partial class addingFavouriteTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "cart");

            migrationBuilder.CreateTable(
                name: "Favourite",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourite", x => x.id);
                    table.ForeignKey(
                        name: "FK_Favourite_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteProduct",
                columns: table => new
                {
                    Favouritesid = table.Column<int>(type: "int", nullable: false),
                    productsid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteProduct", x => new { x.Favouritesid, x.productsid });
                    table.ForeignKey(
                        name: "FK_FavouriteProduct_Favourite_Favouritesid",
                        column: x => x.Favouritesid,
                        principalTable: "Favourite",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouriteProduct_products_productsid",
                        column: x => x.productsid,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favourite_userId",
                table: "Favourite",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteProduct_productsid",
                table: "FavouriteProduct",
                column: "productsid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavouriteProduct");

            migrationBuilder.DropTable(
                name: "Favourite");

            migrationBuilder.AddColumn<long>(
                name: "Quantity",
                table: "cart",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
