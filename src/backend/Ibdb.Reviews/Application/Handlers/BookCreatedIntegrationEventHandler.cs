using Ibdb.Reviews.Application.Dtos;
using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Dtos;

namespace Ibdb.Reviews.Application.Handlers
{
    public class BookCreatedIntegrationEventHandler : IIntegrationEventHandler<BookCreatedEventDataDto>
    {
        private readonly IJsonSerializer _jsonSerializer;

        public BookCreatedIntegrationEventHandler(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public string Name => EventNames.Books.BookCreated;

        public int DataVersion => 1;

        public async IAsyncEnumerable<EventDto> Handle(Guid entityId, BookCreatedEventDataDto eventData)
        {
            yield return new EventDto(
                Id: Guid.NewGuid(),
                EntityId: entityId,
                Name: EventNames.Reviews.BookCreated,
                DataVersion: 1,
                Data: await _jsonSerializer.Serialize(new BookCreatedEventDataDto(eventData.Title, eventData.Description)));
        }
    }
}
