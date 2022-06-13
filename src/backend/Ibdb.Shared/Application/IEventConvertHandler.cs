using Ibdb.Shared.Application.Dtos;

namespace Ibdb.Shared.Application
{
    public interface IEventConvertHandler
    {
        string Name { get; }

        int DataVersion { get; }

        Task<EventDto?> Convert(Guid entityId, object eventData);
    }

    public interface IEventConvertHandler<TEventData> : IEventConvertHandler
    {
        Task<EventDto?> IEventConvertHandler.Convert(Guid entityId, object eventData) => Convert(entityId, (TEventData)eventData);

        Task<EventDto?> Convert(Guid entityId, TEventData eventData);
    }
}
