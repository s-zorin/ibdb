using Ibdb.Reviews.Application.Commands;
using Ibdb.Reviews.Application.Dtos;
using Ibdb.Reviews.Application.Queries;
using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Exceptions;

namespace Ibdb.Reviews.Application.Handlers
{
    public class CreateReviewCommandHandler : ICommandHandler<CreateReviewCommand>
    {
        private readonly IEventStore _eventStore;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ILocalEventBus _localEventBus;

        public CreateReviewCommandHandler(
            IEventStore eventStore,
            IJsonSerializer jsonSerializer,
            ILocalEventBus localEventBus)
        {
            _eventStore = eventStore;
            _jsonSerializer = jsonSerializer;
            _localEventBus = localEventBus;
        }

        public async Task Handle(CreateReviewCommand command)
        {
            var book = await _localEventBus.Execute(new FindBookQuery { BookId = command.BookId });

            if (book == null)
                throw new ValidationException($"No book with with Id {command.BookId} was found.", "REVIEW_BOOK_NOT_FOUND");

            var eventData = await _jsonSerializer.Serialize(
                new ReviewCreatedEventDataDto(
                    BookId: command.BookId,
                    BookTitle: book.Title,
                    BookDescription: book.Description,
                    Text: command.Text,
                    Score: command.Score));

            await _eventStore.AddEvent(command.Id, EventNames.Reviews.ReviewCreated, 1, eventData);
        }
    }
}
