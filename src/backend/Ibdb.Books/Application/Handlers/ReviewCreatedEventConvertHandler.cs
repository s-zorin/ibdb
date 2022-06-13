using Ibdb.Books.Application.Dtos;
using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Dtos;

namespace Ibdb.Books.Application.Handlers
{
    public class ReviewCreatedEventConvertHandler : IEventConvertHandler<ReviewCreatedEventDataDto>
    {
        private readonly IEventStore _eventStore;
        private readonly IJsonSerializer _jsonSerializer;

        public ReviewCreatedEventConvertHandler(IEventStore eventStore, IJsonSerializer jsonSerializer)
        {
            _eventStore = eventStore;
            _jsonSerializer = jsonSerializer;
        }

        public string Name => "ReviewCreated";

        public int DataVersion => 1;

        public async Task<EventDto?> Convert(Guid entityId, ReviewCreatedEventDataDto eventData)
        {
            var events = await _eventStore.GetEvents(eventData.BookId);

            if (events.Count == 0)
                return null;

            return new EventDto
            {
                EntityId = eventData.BookId,
                Name = "BookReviewed",
                DataVersion = 1,
                Data = await _jsonSerializer.Serialize<BookReviewedEventDataDto>(x =>
                {
                    x.ReviewId = entityId;
                    x.Score = eventData.Score;
                })
            };
        }
    }
}
