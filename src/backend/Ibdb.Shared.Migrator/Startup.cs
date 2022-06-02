using Ibdb.Books.Infrastructure.Ef;
using Ibdb.Shared.Infrastructure.Ef;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ibdb.Shared.Migrator
{
    public class Startup
    {
        public Startup()
        {
            Configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EventStoreContext>(options => options.UseNpgsql(Configuration.GetConnectionString("EventStore")));
            services.AddDbContext<BooksContext>(options => options.UseNpgsql(Configuration.GetConnectionString("Books")));
            services.AddLogging(logger => logger.AddConsole());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}