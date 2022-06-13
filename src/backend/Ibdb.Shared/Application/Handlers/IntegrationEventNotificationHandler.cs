using Ibdb.Shared.Application.Notifications;

namespace Ibdb.Shared.Application.Handlers
{
    internal class IntegrationEventNotificationHandler : INotificationHandler<IntegrationEventNotification>
    {
        private readonly IEventStore _eventStore;
        private readonly IEventConverter _eventConverter;

        public IntegrationEventNotificationHandler(IEventStore eventStore, IEventConverter eventConverter)
        {
            _eventStore = eventStore;
            _eventConverter = eventConverter;
        }

        public async Task Handle(IntegrationEventNotification notification)
        {
            var convertedEvent = await _eventConverter.Convert(notification.Event);

            if (convertedEvent is null)
                return;

            await _eventStore.AddEvent(convertedEvent.EntityId, convertedEvent.Name, convertedEvent.DataVersion, convertedEvent.Data);
        }
    }
}
