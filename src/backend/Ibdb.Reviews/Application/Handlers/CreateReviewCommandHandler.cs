using Ibdb.Reviews.Application.Commands;
using Ibdb.Reviews.Application.Dtos;
using Ibdb.Shared.Application;

namespace Ibdb.Reviews.Application.Handlers
{
    public class CreateReviewCommandHandler : ICommandHandler<CreateReviewCommand>
    {
        private readonly IEventStore _eventStore;
        private readonly IMapper _mapper;
        private readonly IJsonSerializer _jsonSerializer;

        public CreateReviewCommandHandler(IEventStore eventStore, IMapper mapper, IJsonSerializer jsonSerializer)
        {
            _eventStore = eventStore;
            _mapper = mapper;
            _jsonSerializer = jsonSerializer;
        }

        public async Task Handle(CreateReviewCommand command)
        {
            var eventData = await _jsonSerializer.Serialize(_mapper.Map<ReviewCreatedEventDataDto>(command));

            await _eventStore.AddEvent(command.Id, "ReviewCreated", 1, eventData);
        }
    }
}
