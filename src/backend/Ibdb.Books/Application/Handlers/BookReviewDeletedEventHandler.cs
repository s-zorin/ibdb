using Ibdb.Books.Application.Dtos;
using Ibdb.Books.Domain;
using Ibdb.Shared.Application;

namespace Ibdb.Books.Application.Handlers
{
    public class BookReviewDeletedEventHandler : IEventHandler<Book, BookReviewDeletedEventDataDto>
    {
        public string Name => EventNames.Books.BookReviewDeleted;

        public int DataVersion => 1;

        public Book Handle(Guid entityId, Book? entity, BookReviewDeletedEventDataDto data)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.ReviewScores.ContainsKey(data.ReviewId))
            {
                entity.ReviewScores.Remove(data.ReviewId);
            }

            entity.Rating = entity.ReviewScores
                .Select(p => p.Value)
                .DefaultIfEmpty(0)
                .Average();

            return entity;
        }
    }
}
