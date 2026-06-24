using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommeraceWebApiPractice.Migrations
{
    /// <inheritdoc />
    public partial class addingFavouriteTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favourite_AspNetUsers_userId",
                table: "Favourite");

            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteProduct_Favourite_Favouritesid",
                table: "FavouriteProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favourite",
                table: "Favourite");

            migrationBuilder.RenameTable(
                name: "Favourite",
                newName: "favourites");

            migrationBuilder.RenameIndex(
                name: "IX_Favourite_userId",
                table: "favourites",
                newName: "IX_favourites_userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_favourites",
                table: "favourites",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteProduct_favourites_Favouritesid",
                table: "FavouriteProduct",
                column: "Favouritesid",
                principalTable: "favourites",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_favourites_AspNetUsers_userId",
                table: "favourites",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteProduct_favourites_Favouritesid",
                table: "FavouriteProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_favourites_AspNetUsers_userId",
                table: "favourites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_favourites",
                table: "favourites");

            migrationBuilder.RenameTable(
                name: "favourites",
                newName: "Favourite");

            migrationBuilder.RenameIndex(
                name: "IX_favourites_userId",
                table: "Favourite",
                newName: "IX_Favourite_userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favourite",
                table: "Favourite",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourite_AspNetUsers_userId",
                table: "Favourite",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteProduct_Favourite_Favouritesid",
                table: "FavouriteProduct",
                column: "Favouritesid",
                principalTable: "Favourite",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
