using Ibdb.Reviews.Application.Dtos;
using Ibdb.Reviews.Domain;
using Ibdb.Shared.Application;

namespace Ibdb.Reviews.Application.Handlers
{
    public class BookCreatedEventHandler : IEventHandler<Book, BookCreatedEventDataDto>
    {
        public string Name => EventNames.Reviews.BookCreated;

        public int DataVersion => 1;

        public Book Handle(Guid entityId, Book? entity, BookCreatedEventDataDto data)
        {
            return new Book
            {
                Id = entityId,
                Title = data.Title,
                Description = data.Description
            };
        }
    }
}
