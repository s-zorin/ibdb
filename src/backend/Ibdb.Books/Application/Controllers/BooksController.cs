using Ibdb.Books.Application.Commands;
using Ibdb.Books.Application.Dtos;
using Ibdb.Books.Application.Queries;
using Ibdb.Shared.Application;
using Microsoft.AspNetCore.Mvc;

namespace Ibdb.Books.Application.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILocalEventBus _localEventBus;

        public BooksController(ILocalEventBus localEventBus)
        {
            _localEventBus = localEventBus;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto dto)
        {
            var result = await _localEventBus.Send(new CreateBookCommand { Title = dto.Title, Description = dto.Description });

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EditBookDto dto)
        {
            var result = await _localEventBus.Send(new EditBookCommand { Id = dto.Id, Title = dto.Title, Description = dto.Description });

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetBooksDto dto)
        {
            var result = await _localEventBus.Execute(new GetBooksQuery { Skip = dto.Skip, Take = dto.Take });

            return Ok(result);
        }
    }
}
