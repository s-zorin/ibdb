using Ibdb.Books.Application.Dtos;
using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Dtos;

namespace Ibdb.Books.Application.Handlers
{
    public class ReviewCreatedIntegrationEventHandler : IIntegrationEventHandler<ReviewCreatedEventDataDto>
    {
        private readonly IJsonSerializer _jsonSerializer;

        public ReviewCreatedIntegrationEventHandler(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public string Name => EventNames.Reviews.ReviewCreated;

        public int DataVersion => 1;

        public async IAsyncEnumerable<EventDto> Handle(Guid entityId, ReviewCreatedEventDataDto eventData)
        {
            yield return new EventDto(
                Id: Guid.NewGuid(),
                EntityId: eventData.BookId,
                Name: EventNames.Books.BookReviewed,
                DataVersion: 1,
                Data: await _jsonSerializer.Serialize(new BookReviewedEventDataDto(entityId, eventData.Score)));
        }
    }
}
