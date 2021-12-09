using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Persistence.Migrations
{
    public partial class AddMarks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_AspNetUsers_ApplicationUserId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Books_BookId",
                table: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rating",
                table: "Rating");

            migrationBuilder.RenameTable(
                name: "Rating",
                newName: "Marks");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_BookId",
                table: "Marks",
                newName: "IX_Marks_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_ApplicationUserId",
                table: "Marks",
                newName: "IX_Marks_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marks",
                table: "Marks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_AspNetUsers_ApplicationUserId",
                table: "Marks",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Books_BookId",
                table: "Marks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marks_AspNetUsers_ApplicationUserId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Books_BookId",
                table: "Marks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marks",
                table: "Marks");

            migrationBuilder.RenameTable(
                name: "Marks",
                newName: "Rating");

            migrationBuilder.RenameIndex(
                name: "IX_Marks_BookId",
                table: "Rating",
                newName: "IX_Rating_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Marks_ApplicationUserId",
                table: "Rating",
                newName: "IX_Rating_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rating",
                table: "Rating",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_AspNetUsers_ApplicationUserId",
                table: "Rating",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Books_BookId",
                table: "Rating",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
