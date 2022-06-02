using Ibdb.Shared.Application.Dtos;

namespace Ibdb.Shared.Application
{
    public interface IEntityFactory
    {
        Task<object> Create(IEnumerable<EventDto> events);

        Task<TEntity> Create<TEntity>(IEnumerable<EventDto> events);
    }
}
