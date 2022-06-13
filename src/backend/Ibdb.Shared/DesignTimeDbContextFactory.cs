using Ibdb.Shared.Infrastructure.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ibdb.Shared
{
    public class DesignTimeOutboxContextFactory : IDesignTimeDbContextFactory<OutboxContext>
    {
        public OutboxContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OutboxContext>();
            optionsBuilder.UseNpgsql("???");
            return new OutboxContext(optionsBuilder.Options);
        }
    }

    public class DesignTimeEventStoreContextFactory : IDesignTimeDbContextFactory<EventStoreContext>
    {
        public EventStoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EventStoreContext>();
            optionsBuilder.UseNpgsql("???");
            return new EventStoreContext(optionsBuilder.Options);
        }
    }
}
