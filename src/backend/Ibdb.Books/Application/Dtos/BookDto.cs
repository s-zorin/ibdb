namespace Ibdb.Books.Application.Dtos
{
    public record BookDto(Guid Id, string Title, string? Description, float Rating);
}
