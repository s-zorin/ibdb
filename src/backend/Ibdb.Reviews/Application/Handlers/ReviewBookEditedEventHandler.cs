using Ibdb.Reviews.Application.Dtos;
using Ibdb.Reviews.Domain;
using Ibdb.Shared.Application;

namespace Ibdb.Reviews.Application.Handlers
{
    public class ReviewBookEditedEventHandler : IEventHandler<Review, ReviewBookEditedEventDataDto>
    {
        public string Name => EventNames.Reviews.ReviewBookEdited;

        public int DataVersion => 1;

        public Review Handle(Guid entityId, Review? entity, ReviewBookEditedEventDataDto data)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            entity.BookTitle = data.Title;

            return entity;
        }
    }
}
