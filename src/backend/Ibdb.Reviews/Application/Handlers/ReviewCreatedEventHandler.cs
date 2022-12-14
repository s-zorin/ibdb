using Ibdb.Reviews.Application.Dtos;
using Ibdb.Reviews.Domain;
using Ibdb.Shared.Application;

namespace Ibdb.Reviews.Application.Handlers
{
    public class ReviewCreatedEventHandler : IEventHandler<Review, ReviewCreatedEventDataDto>
    {
        public string Name => EventNames.Reviews.ReviewCreated;

        public int DataVersion => 1;

        public Review Handle(Guid entityId, Review? entity, ReviewCreatedEventDataDto data)
        {
            return new Review
            {
                Id = entityId,
                BookId = data.BookId,
                BookTitle = data.BookTitle,
                Text = data.Text,
                Score = data.Score
            };
        }
    }
}
