using Ibdb.Books.Application.Commands;
using Ibdb.Books.Application.Dtos;
using Ibdb.Shared.Application;

namespace Ibdb.Books.Application.Handlers
{
    public class CreateBookCommandHandler : ICommandHandler<CreateBookCommand>
    {
        private readonly IEventStore _eventStore;
        private readonly IJsonSerializer _jsonSerializer;

        public CreateBookCommandHandler(IEventStore eventStore, IJsonSerializer jsonSerializer)
        {
            _eventStore = eventStore;
            _jsonSerializer = jsonSerializer;
        }

        public async Task<ICommandResult> Handle(CreateBookCommand command)
        {
            if (command.Title is null)
                return new CommandResult(default, false);

            var entityId = Guid.NewGuid();
            var eventData = await _jsonSerializer.Serialize<BookCreatedEventDataDto>(x =>
            {
                x.Title = command.Title;
                x.Description = command.Description;
            });

            await _eventStore.AddEvent(entityId, "BookCreated", 1, eventData);

            return new CommandResult(entityId, true);
        }
    }
}
