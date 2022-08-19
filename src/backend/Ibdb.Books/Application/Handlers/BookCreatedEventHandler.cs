using Ibdb.Books.Application.Dtos;
using Ibdb.Books.Domain;
using Ibdb.Shared.Application;

namespace Ibdb.Books.Application.Handlers
{
    public class BookCreatedEventHandler : IEventHandler<Book, BookCreatedEventDataDto>
    {
        public string Name => EventNames.Books.BookCreated;

        public int DataVersion => 1;

        public Book Handle(Guid entityId, Book? entity, BookCreatedEventDataDto data)
        {
            return new Book { Id = entityId, Title = data.Title, Description = data.Description, Rating = 0 };
        }
    }
}
