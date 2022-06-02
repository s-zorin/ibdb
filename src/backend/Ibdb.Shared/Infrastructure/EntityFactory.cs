using Ibdb.Shared.Application.Dtos;
using Microsoft.Extensions.DependencyInjection;

namespace Ibdb.Shared.Application
{
    public class EntityFactory : IEntityFactory, ITransientService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IJsonSerializer _jsonSerializer;

        public EntityFactory(IServiceProvider serviceProvider, IJsonSerializer jsonSerializer)
        {
            _serviceProvider = serviceProvider;
            _jsonSerializer = jsonSerializer;
        }

        public async Task<object> Create(IEnumerable<EventDto> events)
        {
            if (events is null)
                throw new ArgumentNullException(nameof(events));

            await using var scope = _serviceProvider.CreateAsyncScope();

            var eventHandlers = scope.ServiceProvider.GetServices(typeof(IEventHandler)).Cast<IEventHandler>().ToList();

            object? entity = null;

            foreach (var e in events)
            {
                if (e.Data is null)
                    continue;

                var eventHandler = eventHandlers.Single(h => h.Name == e.Name && h.DataVersion == e.DataVersion);

                // Get implemented IEventHandler<,> interface;
                // Then get the last generic argument out of that interface and use it for deserialization.
                var eventDataType = eventHandler
                    .GetType()
                    .FindInterfaces((t, c) => t.IsGenericType && t.GetGenericTypeDefinition() == c as Type, typeof(IEventHandler<,>))
                    .Single()
                    .GetGenericArguments()
                    .Last();

                var eventData = await _jsonSerializer.Deserialize(eventDataType, e.Data);

                entity = eventHandler.Handle(e.EntityId, entity, eventData);
            }

            if (entity is null)
                throw new InvalidOperationException("No entity was created as a result of provided events.");

            return entity;
        }

        public async Task<TEntity> Create<TEntity>(IEnumerable<EventDto> events)
        {
            return (TEntity)(await Create(events));
        }
    }
}
