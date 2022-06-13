using Ibdb.Shared.Application.Dtos;

namespace Ibdb.Shared.Application
{
    public interface IEventConverter
    {
        Task<EventDto?> Convert(EventDto e);
    }
}
