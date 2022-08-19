namespace Ibdb.Reviews.Application.Dtos
{
    public record ReviewEditedEventDataDto(
        Guid BookId,
        string Text,
        float Score);
}
