using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repsotiry.Migrations
{
    public partial class nullabletype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Books_BookId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_episode_episodeId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_episodeId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Records");

            migrationBuilder.AlterColumn<int>(
                name: "episodeId",
                table: "Records",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "Records",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Records_episodeId",
                table: "Records",
                column: "episodeId",
                unique: true,
                filter: "[episodeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Books_BookId",
                table: "Records",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_episode_episodeId",
                table: "Records",
                column: "episodeId",
                principalTable: "episode",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Books_BookId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_episode_episodeId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_episodeId",
                table: "Records");

            migrationBuilder.AlterColumn<int>(
                name: "episodeId",
                table: "Records",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "Records",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Records",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Records_episodeId",
                table: "Records",
                column: "episodeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Books_BookId",
                table: "Records",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_episode_episodeId",
                table: "Records",
                column: "episodeId",
                principalTable: "episode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
