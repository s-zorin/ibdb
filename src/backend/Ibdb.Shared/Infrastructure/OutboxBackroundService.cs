using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Dtos;
using Ibdb.Shared.Application.Notifications;
using Ibdb.Shared.Infrastructure.Ef;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ibdb.Shared.Infrastructure
{
    internal class OutboxBackroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OutboxBackroundService> _logger;

        public OutboxBackroundService(IServiceProvider serviceProvider, ILogger<OutboxBackroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            var context = scope.ServiceProvider.GetRequiredService<OutboxContext>();
            var distributedEventBus = scope.ServiceProvider.GetRequiredService<IDistributedEventBus>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
                await ProcessPage();
            }

            async Task ProcessPage()
            {
                if (stoppingToken.IsCancellationRequested)
                    return;

                foreach (var e in context.Events.OrderBy(e => e.Timestamp).Take(100).ToList())
                {
                    if (stoppingToken.IsCancellationRequested)
                        break;

                    try
                    {
                        await distributedEventBus.Publish(new IntegrationEventNotification(mapper.Map<EventDto>(e)));
                        context.Events.Remove(e);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to publish integration event {eventName} with id {eventId}", e.Name, e.Id);
                        break;
                    }
                }

                await context.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}
