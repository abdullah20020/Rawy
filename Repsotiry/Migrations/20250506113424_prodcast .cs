using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repsotiry.Migrations
{
    public partial class prodcast : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProdcastId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "p",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProdcastId",
                table: "Records",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProdcastId",
                table: "Playlists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "prodcasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prodcastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prodcastimage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prodcasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_prodcasts_AspNetUsers_BaseUserId",
                        column: x => x.BaseUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProdcastId",
                table: "Reviews",
                column: "ProdcastId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_ProdcastId",
                table: "Records",
                column: "ProdcastId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_ProdcastId",
                table: "Playlists",
                column: "ProdcastId");

            migrationBuilder.CreateIndex(
                name: "IX_prodcasts_BaseUserId",
                table: "prodcasts",
                column: "BaseUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_prodcasts_ProdcastId",
                table: "Playlists",
                column: "ProdcastId",
                principalTable: "prodcasts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_prodcasts_ProdcastId",
                table: "Records",
                column: "ProdcastId",
                principalTable: "prodcasts",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_prodcasts_ProdcastId",
                table: "Playlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_prodcasts_ProdcastId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_prodcasts_ProdcastId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "prodcasts");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ProdcastId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Records_ProdcastId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Playlists_ProdcastId",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "ProdcastId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "p",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ProdcastId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "ProdcastId",
                table: "Playlists");
        }
    }
}
