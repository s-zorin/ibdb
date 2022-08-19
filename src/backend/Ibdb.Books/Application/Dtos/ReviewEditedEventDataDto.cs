namespace Ibdb.Books.Application.Dtos
{
    public record ReviewEditedEventDataDto(Guid BookId, string Text, float Score);
}
