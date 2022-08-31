using Ibdb.Reviews.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ibdb.Reviews.Infrastructure.Ef
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
                .Property(e => e.IsDeleted)
                .HasColumnName("is_deleted")
                .HasComment("If true the book is considered to be deleted.")
                .IsRequired();
        }
    }
}
