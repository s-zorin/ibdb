using Ibdb.Shared.Application.Controllers;
using Ibdb.Shared.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Ibdb.Shared.Application.Filters
{
    public class CommonResultFilter : IActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public CommonResultFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Controller is ErrorsController)
                return;

            if (context.Result is not ObjectResult objectResult)
                return;

            if (objectResult.Value is null)
            {
                context.Result = new ObjectResult(new CommonResultDto(Array.Empty<ErrorDto>()));
                return;
            }

            var value = objectResult.Value;
            var valueType = value.GetType();

            var commonResult = ActivatorUtilities
                .CreateInstance(_serviceProvider, typeof(CommonResultDto<>)
                .MakeGenericType(valueType), value, Array.Empty<ErrorDto>());

            context.Result = new ObjectResult(commonResult);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
