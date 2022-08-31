using Ibdb.Reviews.Application.Dtos;
using Ibdb.Reviews.Application.Queries;
using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Dtos;

namespace Ibdb.Reviews.Application.Handlers
{
    public class BookDeletedIntegrationEventHandler : IIntegrationEventHandler<BookDeletedEventDataDto>
    {
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ILocalEventBus _localEventBus;

        public BookDeletedIntegrationEventHandler(IJsonSerializer jsonSerializer, ILocalEventBus localEventBus)
        {
            _jsonSerializer = jsonSerializer;
            _localEventBus = localEventBus;
        }

        public string Name => EventNames.Books.BookDeleted;

        public int DataVersion => 1;

        public async IAsyncEnumerable<EventDto> Handle(Guid entityId, BookDeletedEventDataDto eventData)
        {
            var query = new GetBookReviewsQuery { BookId = entityId };
            var reviews = await _localEventBus.Execute(query);

            yield return new EventDto(
                Id: Guid.NewGuid(),
                EntityId: entityId,
                Name: EventNames.Reviews.BookDeleted,
                DataVersion: 1,
                Data: await _jsonSerializer.Serialize(new BookDeletedEventDataDto()));

            foreach (var review in reviews)
            {
                yield return new EventDto(
                    Id: Guid.NewGuid(),
                    EntityId: review.Id,
                    Name: EventNames.Reviews.ReviewDeleted,
                    DataVersion: 1,
                    Data: await _jsonSerializer.Serialize(new ReviewDeletedEventDataDto(entityId)));
            }
        }
    }
}
