namespace Ibdb.Shared.Application.Dtos
{
    public record PageDto<T>(T[] Items, int TotalCount);
}
