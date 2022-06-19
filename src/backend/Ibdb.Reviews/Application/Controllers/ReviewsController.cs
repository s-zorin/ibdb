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
        private readonly IMapper _mapper;
        private readonly IOperationTracker _operationTracker;

        public ReviewsController(ILocalEventBus localEventBus, IMapper mapper, IOperationTracker operationTracker)
        {
            _localEventBus = localEventBus;
            _mapper = mapper;
            _operationTracker = operationTracker;
        }

        [HttpPost]
        public IActionResult Create(Guid operationId, CreateReviewDto dto)
        {
            var command = _mapper.Map<CreateReviewCommand>(dto);

            _ = _localEventBus.Send(command).ContinueWith(t => _operationTracker.Completed(t, operationId));

            return Accepted();
        }
    }
}
