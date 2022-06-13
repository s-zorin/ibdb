using Ibdb.Reviews.Application.Commands;
using Ibdb.Reviews.Application.Dtos;
using Ibdb.Shared.Application;
using Microsoft.AspNetCore.Mvc;

namespace Ibdb.Reviews.Application.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ILocalEventBus _localEventBus;

        public ReviewsController(ILocalEventBus localEventBus)
        {
            _localEventBus = localEventBus;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateReviewDto dto)
        {
            var result = await _localEventBus.Send(new CreateReviewCommand { BookId = dto.BookId, Text = dto.Text, Score = dto.Score });

            return Ok(result);
        }
    }
}
