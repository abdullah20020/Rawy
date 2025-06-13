using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repsotiry.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInterests_Categories_CategoryId",
                table: "UserInterests");

            migrationBuilder.DropIndex(
                name: "IX_UserInterests_CategoryId",
                table: "UserInterests");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "UserInterests",
                newName: "BookyId");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "UserInterests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserInterests_BookId",
                table: "UserInterests",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterests_Books_BookId",
                table: "UserInterests",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInterests_Books_BookId",
                table: "UserInterests");

            migrationBuilder.DropIndex(
                name: "IX_UserInterests_BookId",
                table: "UserInterests");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "UserInterests");

            migrationBuilder.RenameColumn(
                name: "BookyId",
                table: "UserInterests",
                newName: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInterests_CategoryId",
                table: "UserInterests",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterests_Categories_CategoryId",
                table: "UserInterests",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
