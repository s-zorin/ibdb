using Ibdb.Books.Application.Commands;
using Ibdb.Books.Application.Dtos;
using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Exceptions;

namespace Ibdb.Books.Application.Handlers
{
    public class CreateBookCommandHandler : ICommandHandler<CreateBookCommand>
    {
        private readonly IEventStore _eventStore;
        private readonly IMapper _mapper;
        private readonly IJsonSerializer _jsonSerializer;

        public CreateBookCommandHandler(IEventStore eventStore, IMapper mapper, IJsonSerializer jsonSerializer)
        {
            _eventStore = eventStore;
            _mapper = mapper;
            _jsonSerializer = jsonSerializer;
        }

        public async Task Handle(CreateBookCommand command)
        {
            if (command.Title is null)
                throw new ValidationException("Book title is required.", "BOOK_TITLE_REQUIRED");

            var eventData = await _jsonSerializer.Serialize(_mapper.Map<BookCreatedEventDataDto>(command));

            await _eventStore.AddEvent(command.Id, "BookCreated", 1, eventData);
        }
    }
}
