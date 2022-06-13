using Ibdb.Shared.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ibdb.Shared.Infrastructure
{
    internal class RabbitMqBackgroundService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceCollection _serviceCollection;

        private readonly List<IDisposable> _disposables = new();

        public RabbitMqBackgroundService(IServiceProvider serviceProvider, IServiceCollection serviceCollection)
        {
            _serviceProvider = serviceProvider;
            _serviceCollection = serviceCollection;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            SubscribeToNotifications();
            SubscribeToRequests();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            return Task.CompletedTask;
        }

        private void SubscribeToNotifications()
        {
            var notificationHandlerTypes = _serviceCollection
                .Where(IsNotificationHandler)
                .Select(d => d.ServiceType)
                .ToList();

            foreach (var handlerType in notificationHandlerTypes)
            {
                var notificationType = handlerType.GetGenericArguments().Last();
                var subscriptionType = typeof(RabbitMqNotificationSubscription<,>).MakeGenericType(handlerType, notificationType);
                var subscription = (IDisposable)ActivatorUtilities.CreateInstance(_serviceProvider, subscriptionType);

                _disposables.Add(subscription);
            }
        }

        private void SubscribeToRequests()
        {
            var requestHandlerTypes = _serviceCollection
                .Where(IsRequestHandler)
                .Select(d => d.ServiceType)
                .ToList();

            foreach (var handlerType in requestHandlerTypes)
            {
                var requestType = handlerType.GetGenericArguments().First();
                var responseType = handlerType.GetGenericArguments().Last();
                var subscriptionType = typeof(RabbitMqRpcSubscription<,,>).MakeGenericType(handlerType, requestType, responseType);
                var subscription = (IDisposable)ActivatorUtilities.CreateInstance(_serviceProvider, subscriptionType);

                _disposables.Add(subscription);
            }
        }

        private static bool IsNotificationHandler(ServiceDescriptor descriptor) =>
            descriptor.ServiceType.IsGenericType && descriptor.ServiceType.GetGenericTypeDefinition() == typeof(INotificationHandler<>);

        private static bool IsRequestHandler(ServiceDescriptor descriptor) =>
            descriptor.ServiceType.IsGenericType && descriptor.ServiceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>);
    }
}
