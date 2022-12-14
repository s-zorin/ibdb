using Ibdb.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ibdb.Shared.Infrastructure.Ef
{
    public class EventStoreContext : DbContext
    {
        public EventStoreContext(DbContextOptions<EventStoreContext> options) : base(options)
        {
        }

        public DbSet<Event> Events => Set<Event>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EventConfiguration());
        }
    }
}
