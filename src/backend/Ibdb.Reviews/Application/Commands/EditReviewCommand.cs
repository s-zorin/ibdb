namespace Ibdb.Reviews.Application.Commands
{
    public record EditReviewCommand(Guid Id, string Text, float Score);
}
