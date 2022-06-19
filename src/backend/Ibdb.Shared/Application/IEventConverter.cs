using Ibdb.Shared.Application.Dtos;

namespace Ibdb.Shared.Application
{
    public interface IEventConverter
    {
        Task<ConvertedEventDto?> Convert(EventDto e);
    }
}
