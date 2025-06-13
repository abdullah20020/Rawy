using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repsotiry.Migrations
{
    public partial class asa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_episode_prodcasts_ProdcastId",
                table: "episode");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_episode_episodeId",
                table: "Records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_episode",
                table: "episode");

            migrationBuilder.RenameTable(
                name: "episode",
                newName: "episodes");

            migrationBuilder.RenameIndex(
                name: "IX_episode_ProdcastId",
                table: "episodes",
                newName: "IX_episodes_ProdcastId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_episodes",
                table: "episodes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_episodes_prodcasts_ProdcastId",
                table: "episodes",
                column: "ProdcastId",
                principalTable: "prodcasts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_episodes_episodeId",
                table: "Records",
                column: "episodeId",
                principalTable: "episodes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_episodes_prodcasts_ProdcastId",
                table: "episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_episodes_episodeId",
                table: "Records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_episodes",
                table: "episodes");

            migrationBuilder.RenameTable(
                name: "episodes",
                newName: "episode");

            migrationBuilder.RenameIndex(
                name: "IX_episodes_ProdcastId",
                table: "episode",
                newName: "IX_episode_ProdcastId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_episode",
                table: "episode",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_episode_prodcasts_ProdcastId",
                table: "episode",
                column: "ProdcastId",
                principalTable: "prodcasts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_episode_episodeId",
                table: "Records",
                column: "episodeId",
                principalTable: "episode",
                principalColumn: "Id");
        }
    }
}
