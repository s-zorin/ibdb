using Ibdb.Reviews.Application.Dtos;
using Ibdb.Reviews.Application.Queries;
using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Dtos;

namespace Ibdb.Reviews.Application.Handlers
{
    public class BookEditedIntegrationEventHandler : IIntegrationEventHandler<BookEditedEventDataDto>
    {
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ILocalEventBus _localEventBus;

        public BookEditedIntegrationEventHandler(IJsonSerializer jsonSerializer, ILocalEventBus localEventBus)
        {
            _jsonSerializer = jsonSerializer;
            _localEventBus = localEventBus;
        }

        public string Name => EventNames.Books.BookEdited;

        public int DataVersion => 1;

        public async IAsyncEnumerable<EventDto> Handle(Guid entityId, BookEditedEventDataDto eventData)
        {
            var query = new GetBookReviewsQuery { BookId = entityId };
            var reviews = await _localEventBus.Execute(query);

            foreach (var review in reviews)
            {
                yield return new EventDto(
                    Id: Guid.NewGuid(),
                    EntityId: review.Id,
                    Name: EventNames.Reviews.ReviewBookEdited,
                    DataVersion: 1,
                    Data: await _jsonSerializer.Serialize(new ReviewBookEditedEventData(eventData.Title, eventData.Description)));
            }

            yield return new EventDto(
                Id: Guid.NewGuid(),
                EntityId: entityId,
                Name: EventNames.Reviews.BookEdited,
                DataVersion: 1,
                Data: await _jsonSerializer.Serialize(new BookEditedEventDataDto(eventData.Title, eventData.Description)));
        }
    }
}
