using Ibdb.Shared.Application;

namespace Ibdb.Reviews.Application.Commands
{
    public class CreateReviewCommand : ICommand
    {
        public Guid BookId { get; set; }

        public string? Text { get; set; }

        public float Score { get; set; }
    }
}
