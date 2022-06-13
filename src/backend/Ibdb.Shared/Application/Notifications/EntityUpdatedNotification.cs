using Ibdb.Shared.Application.Dtos;

namespace Ibdb.Shared.Application.Notifications
{
    public class IntegrationEventNotification
    {
        public EventDto Event { get; set; } = new EventDto();
    }

    public class EntityUpdateNotification
    {
        public Guid EntityId { get; set; }
    }
}
