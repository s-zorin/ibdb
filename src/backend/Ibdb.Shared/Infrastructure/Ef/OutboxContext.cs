using Ibdb.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ibdb.Shared.Infrastructure.Ef
{
    public class OutboxContext : DbContext
    {
        public OutboxContext(DbContextOptions<OutboxContext> options) : base(options)
        {
        }

        public DbSet<OutboxEvent> Events => Set<OutboxEvent>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new OutboxEventConfiguration());
        }
    }
}
