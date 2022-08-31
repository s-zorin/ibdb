using Ibdb.Books.Application.Dtos;
using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Dtos;

namespace Ibdb.Books.Application.Handlers
{
    public class ReviewDeletedIntegrationEventHandler : IIntegrationEventHandler<ReviewDeletedEventDataDto>
    {
        private readonly IJsonSerializer _jsonSerializer;

        public ReviewDeletedIntegrationEventHandler(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public string Name => EventNames.Reviews.ReviewDeleted;

        public int DataVersion => 1;

        public async IAsyncEnumerable<EventDto> Handle(Guid entityId, ReviewDeletedEventDataDto eventData)
        {
            yield return new EventDto(
                Id: Guid.NewGuid(),
                EntityId: eventData.BookId,
                Name: EventNames.Books.BookReviewDeleted,
                DataVersion: 1,
                Data: await _jsonSerializer.Serialize(new BookReviewDeletedEventDataDto(entityId)));
        }
    }
}
