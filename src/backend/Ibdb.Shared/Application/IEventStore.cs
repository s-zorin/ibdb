using Ibdb.Shared.Application.Dtos;
using Ibdb.Shared.Domain;

namespace Ibdb.Shared.Application
{
    public interface IEventStore
    {
        Task AddEvent(Guid entityId, string eventName, int eventDataVersion, string eventData);

        public Task<ICollection<EventDto>> GetEvents(Guid entityId);
    }
}
