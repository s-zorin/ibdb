namespace Ibdb.Reviews.Application.Dtos
{
    public record CreateReviewDto(Guid Id, Guid BookId, string Text, float Score);
}
