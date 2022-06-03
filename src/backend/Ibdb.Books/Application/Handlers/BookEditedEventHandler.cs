using Ibdb.Books.Application.Dtos;
using Ibdb.Books.Domain;
using Ibdb.Shared.Application;

namespace Ibdb.Books.Application.Handlers
{
    public class BookEditedEventHandler : IEventHandler<Book, BookEditedEventDataDto>
    {
        public string Name => "BookEdited";

        public int DataVersion => 1;

        public Book Handle(Guid entityId, Book? entity, BookEditedEventDataDto data)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            entity.Title = data.Title!;
            entity.Description = data.Description;

            return entity;
        }
    }
}
