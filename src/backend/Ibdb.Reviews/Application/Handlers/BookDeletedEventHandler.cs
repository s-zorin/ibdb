using Ibdb.Reviews.Application.Dtos;
using Ibdb.Reviews.Domain;
using Ibdb.Shared.Application;

namespace Ibdb.Reviews.Application.Handlers
{
    public class BookDeletedEventHandler : IEventHandler<Book, BookDeletedEventDataDto>
    {
        public string Name => EventNames.Reviews.BookDeleted;

        public int DataVersion => 1;

        public Book Handle(Guid entityId, Book? entity, BookDeletedEventDataDto data)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.IsDeleted = true;

            return entity;
        }
    }
}
