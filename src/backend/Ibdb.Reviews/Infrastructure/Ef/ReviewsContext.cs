using Ibdb.Reviews.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ibdb.Reviews.Infrastructure.Ef
{
    public class ReviewsContext : DbContext
    {
        public ReviewsContext(DbContextOptions<ReviewsContext> options) : base(options)
        {
        }

        public DbSet<Review> Reviews => Set<Review>();

        public DbSet<Book> Books => Set<Book>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
