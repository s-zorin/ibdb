using Ibdb.Books.Application.Commands;
using Ibdb.Books.Application.Dtos;
using Ibdb.Books.Application.Queries;
using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Ibdb.Books.Application.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILocalEventBus _localEventBus;
        private readonly IMapper _mapper;
        private readonly IOperationTracker _operationTracker;

        public BooksController(ILocalEventBus localEventBus, IMapper mapper, IOperationTracker operationTracker)
        {
            _localEventBus = localEventBus;
            _mapper = mapper;
            _operationTracker = operationTracker;
        }

        [HttpPost]
        public IActionResult Create(Guid operationId, CreateBookDto dto)
        {
            var command = _mapper.Map<CreateBookCommand>(dto);

            _ = _localEventBus.Send(command).ContinueWith(t => _operationTracker.Completed(t, operationId));

            return Accepted();
        }

        [HttpPut]
        public IActionResult Edit(Guid operationId, EditBookDto dto)
        {
            var command = _mapper.Map<EditBookCommand>(dto);

            _ = _localEventBus.Send(command).ContinueWith(t => _operationTracker.Completed(t, operationId));

            return Accepted();
        }

        [HttpGet]
        public async Task<CommonResultDto<BookDto[]>> Get([FromQuery] GetBooksDto dto)
        {
            var query = _mapper.Map<GetBooksQuery>(dto);

            var result = await _localEventBus.Execute(query);

            // TODO : Error handling. Filter?
            return new CommonResultDto<BookDto[]>(result.ToArray(), Array.Empty<ErrorDto>());
        }
    }
}
