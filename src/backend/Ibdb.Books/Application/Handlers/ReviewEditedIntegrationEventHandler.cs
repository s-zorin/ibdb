using Ibdb.Books.Application.Dtos;
using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Dtos;

namespace Ibdb.Books.Application.Handlers
{
    public class ReviewEditedIntegrationEventHandler : IIntegrationEventHandler<ReviewEditedEventDataDto>
    {
        private readonly IJsonSerializer _jsonSerializer;

        public ReviewEditedIntegrationEventHandler(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public string Name => EventNames.Reviews.ReviewEdited;

        public int DataVersion => 1;

        public async IAsyncEnumerable<EventDto> Handle(Guid entityId, ReviewEditedEventDataDto eventData)
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
