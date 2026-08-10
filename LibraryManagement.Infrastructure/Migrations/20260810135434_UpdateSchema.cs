using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLoans_AspNetUsers_ApplicationUserId",
                table: "BookLoans");

            migrationBuilder.DropColumn(
                name: "IsReservation",
                table: "BookLoans");

            migrationBuilder.RenameColumn(
                name: "ReturnDate",
                table: "BookLoans",
                newName: "ReturnedAt");

            migrationBuilder.RenameColumn(
                name: "ReservationDate",
                table: "BookLoans",
                newName: "DueAt");

            migrationBuilder.RenameColumn(
                name: "LoanDate",
                table: "BookLoans",
                newName: "CheckedOutAt");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "BookLoans",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BookLoans_ApplicationUserId",
                table: "BookLoans",
                newName: "IX_BookLoans_UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservedAt",
                table: "BookLoans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BookLoans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_BookLoans_AspNetUsers_UserId",
                table: "BookLoans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLoans_AspNetUsers_UserId",
                table: "BookLoans");

            migrationBuilder.DropColumn(
                name: "ReservedAt",
                table: "BookLoans");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BookLoans");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BookLoans",
                newName: "ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "ReturnedAt",
                table: "BookLoans",
                newName: "ReturnDate");

            migrationBuilder.RenameColumn(
                name: "DueAt",
                table: "BookLoans",
                newName: "ReservationDate");

            migrationBuilder.RenameColumn(
                name: "CheckedOutAt",
                table: "BookLoans",
                newName: "LoanDate");

            migrationBuilder.RenameIndex(
                name: "IX_BookLoans_UserId",
                table: "BookLoans",
                newName: "IX_BookLoans_ApplicationUserId");

            migrationBuilder.AddColumn<bool>(
                name: "IsReservation",
                table: "BookLoans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_BookLoans_AspNetUsers_ApplicationUserId",
                table: "BookLoans",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
