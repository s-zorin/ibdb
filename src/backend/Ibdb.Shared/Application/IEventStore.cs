using Ibdb.Shared.Application.Dtos;

namespace Ibdb.Shared.Application
{
    public interface IEventStore
    {
        Task AddEvent(Guid entityId, string eventName, int eventDataVersion, string eventData);

        Task<ICollection<EventDto>> GetEvents(Guid entityId);
    }
}
