namespace Ibdb.Reviews.Application.Dtos
{
    public record ReviewDto(Guid Id, Guid BookId, string BookTitle, string? Text, float Score);
}
