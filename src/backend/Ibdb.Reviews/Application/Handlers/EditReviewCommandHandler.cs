using Ibdb.Reviews.Application.Commands;
using Ibdb.Reviews.Application.Dtos;
using Ibdb.Reviews.Application.Queries;
using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Exceptions;

namespace Ibdb.Reviews.Application.Handlers
{
    public class EditReviewCommandHandler : ICommandHandler<EditReviewCommand>
    {
        private readonly IEventStore _eventStore;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ILocalEventBus _localEventBus;

        public EditReviewCommandHandler(IEventStore eventStore, IJsonSerializer jsonSerializer, ILocalEventBus localEventBus)
        {
            _eventStore = eventStore;
            _jsonSerializer = jsonSerializer;
            _localEventBus = localEventBus;
        }

        public async Task Handle(EditReviewCommand command)
        {
            var review = await _localEventBus.Execute(new FindReviewQuery { Id = command.Id });

            if (review == null)
                throw new ValidationException($"No review with with Id {command.Id} was found.", "REVIEW_REVIEW_NOT_FOUND");

            var eventData = await _jsonSerializer.Serialize(
                new ReviewEditedEventDataDto(
                    BookId: review.BookId,
                    Text: command.Text,
                    Score: command.Score));

            await _eventStore.AddEvent(command.Id, EventNames.Reviews.ReviewEdited, 1, eventData);
        }
    }
}
