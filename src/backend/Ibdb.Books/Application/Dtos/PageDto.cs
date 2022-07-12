namespace Ibdb.Books.Application.Dtos
{
    public record PageDto<T>(T[] Items, int TotalCount);
}
