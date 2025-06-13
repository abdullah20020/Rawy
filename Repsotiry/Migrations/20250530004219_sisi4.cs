using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repsotiry.Migrations
{
    public partial class sisi4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_episodes_prodcasts_ProdcastId",
                table: "episodes");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_BaseUserId",
                table: "Favorites");

            migrationBuilder.AlterColumn<int>(
                name: "ProdcastId",
                table: "episodes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FavoriteId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_BaseUserId",
                table: "Favorites",
                column: "BaseUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_episodes_prodcasts_ProdcastId",
                table: "episodes",
                column: "ProdcastId",
                principalTable: "prodcasts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_episodes_prodcasts_ProdcastId",
                table: "episodes");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_BaseUserId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "FavoriteId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "ProdcastId",
                table: "episodes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_BaseUserId",
                table: "Favorites",
                column: "BaseUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_episodes_prodcasts_ProdcastId",
                table: "episodes",
                column: "ProdcastId",
                principalTable: "prodcasts",
                principalColumn: "Id");
        }
    }
}
