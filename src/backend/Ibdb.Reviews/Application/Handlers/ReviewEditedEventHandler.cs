using Ibdb.Reviews.Application.Dtos;
using Ibdb.Reviews.Domain;
using Ibdb.Shared.Application;

namespace Ibdb.Reviews.Application.Handlers
{
    public class ReviewEditedEventHandler : IEventHandler<Review, ReviewEditedEventDataDto>
    {
        public string Name => EventNames.Reviews.ReviewEdited;

        public int DataVersion => 1;

        public Review Handle(Guid entityId, Review? entity, ReviewEditedEventDataDto data)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            entity.Text = data.Text;
            entity.Score = data.Score;

            return entity;
        }
    }
}
