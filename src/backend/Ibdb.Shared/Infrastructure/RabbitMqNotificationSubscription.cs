using EasyNetQ;
using Ibdb.Shared.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Ibdb.Shared.Infrastructure
{
    internal class RabbitMqNotificationSubscription<TNotificationHandler, TNotification> : IDisposable
        where TNotificationHandler : INotificationHandler<TNotification>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDisposable _disposable;

        public RabbitMqNotificationSubscription(IBus bus, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _disposable = bus.PubSub.Subscribe<TNotification>(Guid.NewGuid().ToString(), Handle);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private async Task Handle(TNotification obj)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            var notificationHandler = (TNotificationHandler)scope.ServiceProvider.GetRequiredService(typeof(TNotificationHandler));

            await notificationHandler.Handle(obj);
        }
    }
}
