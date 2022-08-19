using Ibdb.Reviews.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ibdb.Reviews.Infrastructure.Ef
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder
                .ToTable("reviews");

            builder
                .Property(e => e.Id)
                .HasColumnName("id")
                .HasComment("Review Id.")
                .ValueGeneratedNever()
                .IsRequired();

            builder
                .Property(e => e.BookId)
                .HasColumnName("book_id")
                .HasComment("Book Id.");

            builder
                .Property(e => e.Text)
                .HasColumnName("text")
                .HasComment("Review text.")
                .IsRequired();

            builder
                .Property(e => e.BookTitle)
                .HasColumnName("book_title")
                .HasComment("Book title.")
                .IsRequired();

            builder
                .Property(e => e.Score)
                .HasColumnName("score")
                .HasComment("Score. 0.0 - lowest possible score, 1.0 - highest possible score.")
                .IsRequired();
        }
    }
}
