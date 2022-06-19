using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(logger => logger.AddConsole());
        }

#pragma warning disable RCS1163 // Unused parameter.
#pragma warning disable IDE0060 // Remove unused parameter
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
#pragma warning restore RCS1163 // Unused parameter.
#pragma warning restore IDE0060 // Remove unused parameter
    }
}