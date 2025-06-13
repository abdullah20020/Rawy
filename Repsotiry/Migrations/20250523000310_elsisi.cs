using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repsotiry.Migrations
{
    public partial class elsisi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_episode_episodeId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_prodcasts_ProdcastId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "episodeId",
                table: "Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProdcastId",
                table: "Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_episode_episodeId",
                table: "Reviews",
                column: "episodeId",
                principalTable: "episode",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_prodcasts_ProdcastId",
                table: "Reviews",
                column: "ProdcastId",
                principalTable: "prodcasts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_episode_episodeId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_prodcasts_ProdcastId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "episodeId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProdcastId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_episode_episodeId",
                table: "Reviews",
                column: "episodeId",
                principalTable: "episode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_prodcasts_ProdcastId",
                table: "Reviews",
                column: "ProdcastId",
                principalTable: "prodcasts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
