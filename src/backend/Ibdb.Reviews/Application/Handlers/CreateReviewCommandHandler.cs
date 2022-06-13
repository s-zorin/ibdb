using Ibdb.Reviews.Application.Commands;
using Ibdb.Reviews.Application.Dtos;
using Ibdb.Shared.Application;

namespace Ibdb.Reviews.Application.Handlers
{
    public class CreateReviewCommandHandler : ICommandHandler<CreateReviewCommand>
    {
        private readonly IEventStore _eventStore;
        private readonly IJsonSerializer _jsonSerializer;

        public CreateReviewCommandHandler(IEventStore eventStore, IJsonSerializer jsonSerializer)
        {
            _eventStore = eventStore;
            _jsonSerializer = jsonSerializer;
        }

        public async Task<ICommandResult> Handle(CreateReviewCommand command)
        {
            if (command.Text is null)
                return new CommandResult(default, false);

            var entityId = Guid.NewGuid();
            var eventData = await _jsonSerializer.Serialize<ReviewCreatedEventDataDto>(x =>
            {
                x.BookId = command.BookId;
                x.Text = command.Text;
                x.Score = command.Score;
            });

            await _eventStore.AddEvent(entityId, "ReviewCreated", 1, eventData);

            return new CommandResult(entityId, true);
        }
    }
}
