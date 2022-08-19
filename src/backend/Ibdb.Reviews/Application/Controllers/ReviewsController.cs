using Ibdb.Reviews.Application.Commands;
using Ibdb.Reviews.Application.Dtos;
using Ibdb.Reviews.Application.Queries;
using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Dtos;
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
        [ProducesResponseType(202)]
        public IActionResult Create(Guid operationId, CreateReviewDto dto)
        {
            var command = _mapper.Map<CreateReviewCommand>(dto);

            _ = _localEventBus.Send(command).ContinueWith(t => _operationTracker.Completed(t, operationId));

            return Accepted();
        }

        [HttpPut]
        [ProducesResponseType(202)]
        public IActionResult Edit(Guid operationId, EditReviewDto dto)
        {
            var command = _mapper.Map<EditReviewCommand>(dto);

            _ = _localEventBus.Send(command).ContinueWith(t => _operationTracker.Completed(t, operationId));

            return Accepted();
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<PageDto<ReviewDto>> Get([FromQuery] GetReviewsDto dto)
        {
            var query = _mapper.Map<GetReviewsQuery>(dto);

            var result = await _localEventBus.Execute(query);

            return new PageDto<ReviewDto>(result.Reviews.ToArray(), result.TotalCount);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<ReviewDto?> FindReview(Guid id)
        {
            var query = new FindReviewQuery { Id = id };

            return await _localEventBus.Execute(query);
        }
    }
}
