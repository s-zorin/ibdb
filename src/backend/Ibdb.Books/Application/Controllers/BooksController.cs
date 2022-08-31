using Ibdb.Books.Application.Commands;
using Ibdb.Books.Application.Dtos;
using Ibdb.Books.Application.Queries;
using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Ibdb.Books.Application.Controllers
{
    [Route("api/books")]
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
        [ProducesResponseType(202)]
        public IActionResult Create(Guid operationId, CreateBookDto dto)
        {
            var command = _mapper.Map<CreateBookCommand>(dto);

            _ = _localEventBus.Send(command).ContinueWith(t => _operationTracker.Completed(t, operationId));

            return Accepted();
        }

        [HttpPut]
        [ProducesResponseType(202)]
        public IActionResult Edit(Guid operationId, EditBookDto dto)
        {
            var command = _mapper.Map<EditBookCommand>(dto);

            _ = _localEventBus.Send(command).ContinueWith(t => _operationTracker.Completed(t, operationId));

            return Accepted();
        }

        [HttpDelete]
        [ProducesResponseType(202)]
        public IActionResult Delete(Guid operationId, DeleteBookDto dto)
        {
            var command = _mapper.Map<DeleteBookCommand>(dto);

            _ = _localEventBus.Send(command).ContinueWith(t => _operationTracker.Completed(t, operationId));

            return Accepted();
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<PageDto<BookDto>> Get([FromQuery] GetBooksDto dto)
        {
            var query = _mapper.Map<GetBooksQuery>(dto);

            var result = await _localEventBus.Execute(query);

            return new PageDto<BookDto>(result.Books.ToArray(), result.TotalCount);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<BookDto?> FindBook(Guid id)
        {
            var query = new FindBookQuery { Id = id };

            return await _localEventBus.Execute(query);
        }
    }
}
