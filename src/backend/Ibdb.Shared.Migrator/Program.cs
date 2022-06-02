using Ibdb.Books.Infrastructure.Ef;
using Ibdb.Shared.Infrastructure.Ef;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ibdb.Shared.Migrator
{
    internal static class Program
    {
        static void Main()
        {
            Console.WriteLine("Applying migrations.");

            var webHost = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            using (var context = (EventStoreContext)webHost.Services.GetRequiredService(typeof(EventStoreContext)))
            {
                context.Database.Migrate();
            }

            using (var context = (BooksContext)webHost.Services.GetRequiredService(typeof(BooksContext)))
            {
                context.Database.Migrate();
            }

            Console.WriteLine("Done.");

            var hostApplicationLifetime = webHost.Services.GetRequiredService<IHostApplicationLifetime>();
            hostApplicationLifetime.StopApplication();
        }
    }
}