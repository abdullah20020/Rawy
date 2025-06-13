using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repsotiry.Migrations
{
    public partial class episce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_prodcasts_ProdcastId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_ProdcastId",
                table: "Records");

            migrationBuilder.RenameColumn(
                name: "p",
                table: "Reviews",
                newName: "episodeId");

            migrationBuilder.RenameColumn(
                name: "ProdcastId",
                table: "Records",
                newName: "episodeId");

            migrationBuilder.CreateTable(
                name: "episode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProdcastId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_episode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_episode_prodcasts_ProdcastId",
                        column: x => x.ProdcastId,
                        principalTable: "prodcasts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_episodeId",
                table: "Reviews",
                column: "episodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_episodeId",
                table: "Records",
                column: "episodeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_episode_ProdcastId",
                table: "episode",
                column: "ProdcastId");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_episode_episodeId",
                table: "Records",
                column: "episodeId",
                principalTable: "episode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_episode_episodeId",
                table: "Reviews",
                column: "episodeId",
                principalTable: "episode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_episode_episodeId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_episode_episodeId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "episode");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_episodeId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Records_episodeId",
                table: "Records");

            migrationBuilder.RenameColumn(
                name: "episodeId",
                table: "Reviews",
                newName: "p");

            migrationBuilder.RenameColumn(
                name: "episodeId",
                table: "Records",
                newName: "ProdcastId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_ProdcastId",
                table: "Records",
                column: "ProdcastId");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_prodcasts_ProdcastId",
                table: "Records",
                column: "ProdcastId",
                principalTable: "prodcasts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
