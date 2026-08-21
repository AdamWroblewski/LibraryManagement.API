using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookReviews_AspNetUsers_ApplicationUserId",
                table: "BookReviews");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "BookReviews",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BookReviews_ApplicationUserId",
                table: "BookReviews",
                newName: "IX_BookReviews_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookReviews_AspNetUsers_UserId",
                table: "BookReviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookReviews_AspNetUsers_UserId",
                table: "BookReviews");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BookReviews",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_BookReviews_UserId",
                table: "BookReviews",
                newName: "IX_BookReviews_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookReviews_AspNetUsers_ApplicationUserId",
                table: "BookReviews",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
