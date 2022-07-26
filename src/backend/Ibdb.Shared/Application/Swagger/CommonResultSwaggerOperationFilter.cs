using Ibdb.Shared.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ibdb.Shared.Application.Swagger
{
    public class CommonResultOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!operation.Responses.ContainsKey("400"))
            {
                operation.Responses.Add("400", CreateErrorResponse());
            }

            if (!operation.Responses.ContainsKey("500"))
            {
                operation.Responses.Add("500", CreateErrorResponse());
            }

            var pairs = context.ApiDescription.SupportedResponseTypes.Zip(operation.Responses).ToList();

            foreach (var (responseDescription, response) in pairs)
            {
                if (responseDescription.Type is null)
                    continue;

                if (responseDescription.Type == typeof(void))
                    continue;

                if (responseDescription.Type.IsAssignableTo(typeof(IActionResult)))
                    continue;

                var commonResultType = typeof(CommonResultDto<>).MakeGenericType(responseDescription.Type);

                foreach (var mediaType in response.Value.Content.Values)
                {
                    mediaType.Schema = context.SchemaGenerator.GenerateSchema(commonResultType, context.SchemaRepository);
                }
            }

            OpenApiResponse CreateErrorResponse() => new()
            {
                Content = new Dictionary<string, OpenApiMediaType>()
                {
                    { "text/plain", CreateErrorMediaType() },
                    { "application/json", CreateErrorMediaType() },
                    { "text/json",  CreateErrorMediaType() }
                }
            };

            OpenApiMediaType CreateErrorMediaType() => new()
            {
                Schema = context.SchemaGenerator.GenerateSchema(typeof(CommonResultDto<>), context.SchemaRepository)
            };
        }
    }
}
