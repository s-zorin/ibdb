using Ibdb.Shared.Application.Dtos;
using Ibdb.Shared.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Ibdb.Shared.Application.Controllers
{
    [Route("errors")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        private readonly IMapper _mapper;

        public ErrorsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("{code}")]
        public IActionResult Error(int code)
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = feature?.Error;

            var exceptions = feature?.Error switch
            {
                AggregateException agg => agg.Flatten().InnerExceptions.ToArray(),
                Exception ex => new[] { ex },
                _ => Array.Empty<Exception>()
            };

            var errors = exceptions.Select(e => _mapper.Map<ErrorDto>(e)).Distinct().ToArray();

            if (code == 500)
            {
                HttpContext.Response.StatusCode = exceptions.All(e => e is ValidationException or BusinessException)
                    ? 400
                    : 500;
            }
            else
            {
                HttpContext.Response.StatusCode = code;
            }

            return new ObjectResult(new CommonResultDto(errors));
        }
    }
}
