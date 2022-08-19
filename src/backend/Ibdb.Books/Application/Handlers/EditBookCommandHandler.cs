using Ibdb.Books.Application.Commands;
using Ibdb.Books.Application.Dtos;
using Ibdb.Shared.Application;

namespace Ibdb.Books.Application.Handlers
{
    public class EditBookCommandHandler : ICommandHandler<EditBookCommand>
    {
        private readonly IEventStore _eventStore;
        private readonly IMapper _mapper;
        private readonly IJsonSerializer _jsonSerializer;

        public EditBookCommandHandler(IEventStore eventStore, IMapper mapper, IJsonSerializer jsonSerializer)
        {
            _eventStore = eventStore;
            _mapper = mapper;
            _jsonSerializer = jsonSerializer;
        }

        public async Task Handle(EditBookCommand command)
        {
            var eventData = await _jsonSerializer.Serialize(_mapper.Map<BookEditedEventDataDto>(command));

            await _eventStore.AddEvent(command.Id, EventNames.Books.BookEdited, 1, eventData);
        }
    }
}
