using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repsotiry.Migrations
{
    public partial class haha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_episode_episodeId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_episodeId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "episodeId",
                table: "Reviews");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "episodeId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_episodeId",
                table: "Reviews",
                column: "episodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_episode_episodeId",
                table: "Reviews",
                column: "episodeId",
                principalTable: "episode",
                principalColumn: "Id");
        }
    }
}
