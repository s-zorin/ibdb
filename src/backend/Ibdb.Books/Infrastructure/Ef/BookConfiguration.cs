using Ibdb.Books.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ibdb.Books.Infrastructure.Ef
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .ToTable("books");

            builder
                .Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("Book Id.")
                .ValueGeneratedNever()
                .IsRequired();

            builder
                .Property(e => e.Title)
                .HasColumnName("title")
                .HasComment("Book title.")
                .IsRequired();

            builder
                .Property(e => e.Description)
                .HasColumnName("description")
                .HasComment("Book description.");

            builder
                .Property(e => e.Rating)
                .HasColumnName("rating")
                .HasComment("Book rating. 0.0 - lowest possible rating, 1.0 highest possible rating.")
                .IsRequired();
        }
    }
}
