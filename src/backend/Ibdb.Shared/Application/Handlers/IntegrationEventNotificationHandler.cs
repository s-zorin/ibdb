using Ibdb.Shared.Application.Dtos;
using Ibdb.Shared.Application.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace Ibdb.Shared.Application.Handlers
{
    internal class IntegrationEventNotificationHandler : INotificationHandler<IntegrationEventNotification>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IEventStore _eventStore;

        public IntegrationEventNotificationHandler(
            IServiceProvider serviceProvider,
            IJsonSerializer jsonSerializer,
            IEventStore eventStore)
        {
            _serviceProvider = serviceProvider;
            _jsonSerializer = jsonSerializer;
            _eventStore = eventStore;
        }

        public async Task Handle(IntegrationEventNotification notification)
        {
            await foreach (var generatedEvent in Handle(notification.Event))
            {
                await _eventStore.AddEvent(generatedEvent.EntityId, generatedEvent.Name, generatedEvent.DataVersion, generatedEvent.Data);
            }
        }

        private async IAsyncEnumerable<EventDto> Handle(EventDto integrationEvent)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            var integrationEventHandler = scope.ServiceProvider
                .GetServices<IIntegrationEventHandler>()
                .SingleOrDefault(h => h.Name == integrationEvent.Name && h.DataVersion == integrationEvent.DataVersion);

            if (integrationEventHandler is null)
                yield break;

            var eventDataType = integrationEventHandler
                .GetType()
                .FindInterfaces((t, c) => t.IsGenericType && t.GetGenericTypeDefinition() == c as Type, typeof(IIntegrationEventHandler<>))
                .Single()
                .GetGenericArguments()
                .Last();

            var eventData = await _jsonSerializer.Deserialize(eventDataType, integrationEvent.Data);

            await foreach (var generatedEvents in integrationEventHandler.Handle(integrationEvent.EntityId, eventData))
            {
                yield return generatedEvents;
            }
        }
    }
}
