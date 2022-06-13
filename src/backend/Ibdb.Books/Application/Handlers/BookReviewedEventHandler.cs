using Ibdb.Books.Application.Dtos;
using Ibdb.Books.Domain;
using Ibdb.Shared.Application;

namespace Ibdb.Books.Application.Handlers
{
    public class BookReviewedEventHandler : IEventHandler<Book, BookReviewedEventDataDto>
    {
        public string Name => "BookReviewed";

        public int DataVersion => 1;

        public Book Handle(Guid entityId, Book? entity, BookReviewedEventDataDto data)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            entity.ReviewScores[data.ReviewId] = data.Score;
            entity.Rating = entity.ReviewScores.Average(pair => pair.Value);

            return entity;
        }
    }
}
