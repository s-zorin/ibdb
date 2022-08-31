using Ibdb.Reviews.Application.Commands;
using Ibdb.Reviews.Application.Dtos;
using Ibdb.Reviews.Application.Queries;
using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Exceptions;

namespace Ibdb.Reviews.Application.Handlers
{
    public class DeleteReviewCommandHandler : ICommandHandler<DeleteReviewCommand>
    {
        private readonly IEventStore _eventStore;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ILocalEventBus _localEventBus;

        public DeleteReviewCommandHandler(IEventStore eventStore, IJsonSerializer jsonSerializer, ILocalEventBus localEventBus)
        {
            _eventStore = eventStore;
            _jsonSerializer = jsonSerializer;
            _localEventBus = localEventBus;
        }

        public async Task Handle(DeleteReviewCommand command)
        {
            var review = await _localEventBus.Execute(new FindReviewQuery { Id = command.Id });

            if (review == null)
                throw new ValidationException($"No review with with Id {command.Id} was found.", "REVIEW_REVIEW_NOT_FOUND");

            var eventData = await _jsonSerializer.Serialize(
                new ReviewDeletedEventDataDto(
                    BookId: review.BookId));

            await _eventStore.AddEvent(command.Id, EventNames.Reviews.ReviewDeleted, 1, eventData);
        }
    }
}
