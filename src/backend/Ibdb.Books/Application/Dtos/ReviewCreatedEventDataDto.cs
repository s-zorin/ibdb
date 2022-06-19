namespace Ibdb.Books.Application.Dtos
{
    public record ReviewCreatedEventDataDto(Guid BookId, string Text, float Score);
}
