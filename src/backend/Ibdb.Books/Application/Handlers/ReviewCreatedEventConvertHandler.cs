using Ibdb.Books.Application.Dtos;
using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Dtos;

namespace Ibdb.Books.Application.Handlers
{
    public class ReviewCreatedEventConvertHandler : IEventConvertHandler<ReviewCreatedEventDataDto>
    {
        private readonly IJsonSerializer _jsonSerializer;

        public ReviewCreatedEventConvertHandler(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public string Name => "ReviewCreated";

        public int DataVersion => 1;

        public async Task<ConvertedEventDto> Convert(Guid entityId, ReviewCreatedEventDataDto eventData)
        {
            return new ConvertedEventDto(
                EntityId: eventData.BookId,
                Name: "BookReviewed",
                DataVersion: 1,
                Data: await _jsonSerializer.Serialize(new BookReviewedEventDataDto(entityId, eventData.Score)));
        }
    }
}
