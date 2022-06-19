using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Dtos;
using Microsoft.Extensions.DependencyInjection;

namespace Ibdb.Shared.Infrastructure
{
    internal sealed class EventConverter : IEventConverter, IScopedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IJsonSerializer _jsonSerializer;

        public EventConverter(IServiceProvider serviceProvider, IJsonSerializer jsonSerializer)
        {
            _serviceProvider = serviceProvider;
            _jsonSerializer = jsonSerializer;
        }

        public async Task<ConvertedEventDto?> Convert(EventDto e)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            var eventConvertHandler = scope.ServiceProvider
                .GetServices<IEventConvertHandler>()
                .SingleOrDefault(h => h.Name == e.Name && h.DataVersion == e.DataVersion);

            if (eventConvertHandler is null)
                return null;

            var eventDataType = eventConvertHandler
                .GetType()
                .FindInterfaces((t, c) => t.IsGenericType && t.GetGenericTypeDefinition() == c as Type, typeof(IEventConvertHandler<>))
                .Single()
                .GetGenericArguments()
                .Last();

            var eventData = await _jsonSerializer.Deserialize(eventDataType, e.Data);

            return await eventConvertHandler.Convert(e.EntityId, eventData);
        }
    }
}
