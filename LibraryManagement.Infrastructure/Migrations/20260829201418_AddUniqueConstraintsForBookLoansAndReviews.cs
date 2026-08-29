using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintsForBookLoansAndReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookReviews_BookId",
                table: "BookReviews");

            migrationBuilder.DropIndex(
                name: "IX_BookLoans_BookId",
                table: "BookLoans");

            migrationBuilder.CreateIndex(
                name: "IX_BookReviews_BookId_UserId",
                table: "BookReviews",
                columns: new[] { "BookId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookLoans_BookId_Active",
                table: "BookLoans",
                column: "BookId",
                unique: true,
                filter: "[Status] IN (0, 1, 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookReviews_BookId_UserId",
                table: "BookReviews");

            migrationBuilder.DropIndex(
                name: "IX_BookLoans_BookId_Active",
                table: "BookLoans");

            migrationBuilder.CreateIndex(
                name: "IX_BookReviews_BookId",
                table: "BookReviews",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookLoans_BookId",
                table: "BookLoans",
                column: "BookId");
        }
    }
}
