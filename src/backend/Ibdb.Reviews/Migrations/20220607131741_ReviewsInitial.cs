using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ibdb.Reviews.Migrations
{
    public partial class ReviewsInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Review Id."),
                    book_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Book Id."),
                    text = table.Column<string>(type: "text", nullable: false, comment: "Review text."),
                    score = table.Column<float>(type: "real", nullable: false, comment: "Score. 0.0 - lowest possible score, 1.0 highest possible score.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reviews");
        }
    }
}
