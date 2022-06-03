using Ibdb.Books.Application.Commands;
using Ibdb.Books.Application.Dtos;
using Ibdb.Shared.Application;

namespace Ibdb.Books.Application.Handlers
{
    public class EditBookCommandHandler : ICommandHandler<EditBookCommand>
    {
        private readonly IEventStore _eventStore;
        private readonly IJsonSerializer _jsonSerializer;

        public EditBookCommandHandler(IEventStore eventStore, IJsonSerializer jsonSerializer)
        {
            _eventStore = eventStore;
            _jsonSerializer = jsonSerializer;
        }

        public async Task<ICommandResult> Handle(EditBookCommand command)
        {
            if (command.Id is null)
                return new CommandResult(default, false);

            if (command.Title is null)
                return new CommandResult(default, false);

            var eventData = await _jsonSerializer.Serialize<BookEditedEventDataDto>(x =>
            {
                x.Title = command.Title;
                x.Description = command.Description;
            });

            await _eventStore.AddEvent(command.Id.Value, "BookEdited", 1, eventData);

            return new CommandResult(command.Id.Value, true);
        }
    }
}
