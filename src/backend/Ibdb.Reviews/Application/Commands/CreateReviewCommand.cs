
namespace Ibdb.Reviews.Application.Commands
{
    public record CreateReviewCommand(Guid Id, Guid BookId, string Text, float Score);
}
