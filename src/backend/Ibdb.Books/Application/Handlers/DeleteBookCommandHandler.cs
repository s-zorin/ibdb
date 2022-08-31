using Ibdb.Books.Application.Commands;
using Ibdb.Books.Application.Dtos;
using Ibdb.Shared.Application;

namespace Ibdb.Books.Application.Handlers
{
    public class DeleteBookCommandHandler : ICommandHandler<DeleteBookCommand>
    {
        private readonly IEventStore _eventStore;
        private readonly IMapper _mapper;
        private readonly IJsonSerializer _jsonSerializer;

        public DeleteBookCommandHandler(IEventStore eventStore, IMapper mapper, IJsonSerializer jsonSerializer)
        {
            _eventStore = eventStore;
            _mapper = mapper;
            _jsonSerializer = jsonSerializer;
        }

        public async Task Handle(DeleteBookCommand command)
        {
            var eventData = await _jsonSerializer.Serialize(_mapper.Map<BookDeletedEventDataDto>(command));

            await _eventStore.AddEvent(command.Id, EventNames.Books.BookDeleted, 1, eventData);
        }
    }
}
