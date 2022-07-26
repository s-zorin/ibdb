
namespace Ibdb.Shared.Application
{
    public interface IMapper : IScopedService
    {
        TDestination Map<TSource, TDestination>(TSource source);

        TDestination Map<TDestination>(object? source);
    }
}
