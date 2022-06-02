using Ibdb.Books.Application.Commands;
using Ibdb.Books.Application.Dtos;
using Ibdb.Shared.Application;
using Microsoft.AspNetCore.Mvc;

namespace Ibdb.Books.Application.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILocalEventBus _localEventBus;

        public BooksController(ILocalEventBus localEventBus)
        {
            _localEventBus = localEventBus;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateBookDto dto)
        {
            var result = await _localEventBus.Send(new CreateBookCommand { Title = dto.Title, Description = dto.Description });

            return Ok(result);
        }
    }
}
