using Ibdb.Books.Infrastructure.Ef;
using Ibdb.Reviews.Infrastructure.Ef;
using Ibdb.Shared.Infrastructure.Ef;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ibdb.Shared.Migrator
{
    internal static class Program
    {
        static void Main()
        {
            Console.WriteLine("Applying migrations.");

            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            var webHost = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            Migrate<EventStoreContext>(webHost.Services, configuration.GetConnectionString("EventStore_Books"));
            Migrate<EventStoreContext>(webHost.Services, configuration.GetConnectionString("EventStore_Reviews"));
            Migrate<OutboxContext>(webHost.Services, configuration.GetConnectionString("Outbox_Books"));
            Migrate<OutboxContext>(webHost.Services, configuration.GetConnectionString("Outbox_Reviews"));
            Migrate<BooksContext>(webHost.Services, configuration.GetConnectionString("Books"));
            Migrate<ReviewsContext>(webHost.Services, configuration.GetConnectionString("Reviews"));

            Console.WriteLine("Done.");

            var hostApplicationLifetime = webHost.Services.GetRequiredService<IHostApplicationLifetime>();
            hostApplicationLifetime.StopApplication();
        }

        static void Migrate<T>(IServiceProvider serviceProvider, string connectionString) where T : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<T>();
            optionsBuilder.UseNpgsql(connectionString);
            using var dbContext = ActivatorUtilities.CreateInstance<T>(serviceProvider, optionsBuilder.Options);
            dbContext.Database.Migrate();
        }
    }
}