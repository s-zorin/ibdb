using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ibdb.Reviews.Migrations
{
    public partial class Reviews_Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Book Id."),
                    title = table.Column<string>(type: "text", nullable: false, comment: "Book title."),
                    description = table.Column<string>(type: "text", nullable: true, comment: "Book description."),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "If true the book is considered to be deleted.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Review Id."),
                    book_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Book Id."),
                    book_title = table.Column<string>(type: "text", nullable: false, comment: "Book title."),
                    text = table.Column<string>(type: "text", nullable: false, comment: "Review text."),
                    score = table.Column<float>(type: "real", nullable: false, comment: "Score. 0.0 - lowest possible score, 1.0 - highest possible score."),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "reviews");
        }
    }
}
