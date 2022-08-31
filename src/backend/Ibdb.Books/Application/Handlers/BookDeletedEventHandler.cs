using Ibdb.Books.Application.Dtos;
using Ibdb.Books.Domain;
using Ibdb.Shared.Application;

namespace Ibdb.Books.Application.Handlers
{
    public class BookDeletedEventHandler : IEventHandler<Book, BookDeletedEventDataDto>
    {
        public string Name => EventNames.Books.BookDeleted;

        public int DataVersion => 1;

        public Book Handle(Guid entityId, Book? entity, BookDeletedEventDataDto data)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            entity.IsDeleted = true;

            return entity;
        }
    }
}
