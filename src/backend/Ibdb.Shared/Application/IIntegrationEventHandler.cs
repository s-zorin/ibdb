using Ibdb.Shared.Application.Dtos;

namespace Ibdb.Shared.Application
{
    public interface IIntegrationEventHandler
    {
        string Name { get; }

        int DataVersion { get; }

        IAsyncEnumerable<EventDto> Handle(Guid entityId, object eventData);
    }

    public interface IIntegrationEventHandler<TEventData> : IIntegrationEventHandler
    {
        IAsyncEnumerable<EventDto> IIntegrationEventHandler.Handle(Guid entityId, object eventData) => Handle(entityId, (TEventData)eventData);

        IAsyncEnumerable<EventDto> Handle(Guid entityId, TEventData eventData);
    }
}
