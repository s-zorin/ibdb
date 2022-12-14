namespace Ibdb.Reviews.Application.Dtos
{
    public record ReviewCreatedEventDataDto(
        Guid BookId,
        string BookTitle,
        string? BookDescription,
        string Text,
        float Score);
}
