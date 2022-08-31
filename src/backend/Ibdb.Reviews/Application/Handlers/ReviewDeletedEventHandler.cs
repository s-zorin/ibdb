using Ibdb.Reviews.Application.Dtos;
using Ibdb.Reviews.Domain;
using Ibdb.Shared.Application;

namespace Ibdb.Reviews.Application.Handlers
{
    public class ReviewDeletedEventHandler : IEventHandler<Review, ReviewDeletedEventDataDto>
    {
        public string Name => EventNames.Reviews.ReviewDeleted;

        public int DataVersion => 1;

        public Review Handle(Guid entityId, Review? entity, ReviewDeletedEventDataDto data)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            entity.IsDeleted = true;

            return entity;
        }
    }
}
