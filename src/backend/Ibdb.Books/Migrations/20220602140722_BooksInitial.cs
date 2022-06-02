using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ibdb.Books.Migrations
{
    public partial class BooksInitial : Migration
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
                    rating = table.Column<float>(type: "real", nullable: false, comment: "Book rating. 0.0 - lowest possible rating, 1.0 highest possible rating.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "books");
        }
    }
}
