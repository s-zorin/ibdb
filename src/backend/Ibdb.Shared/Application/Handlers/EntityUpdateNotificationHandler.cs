using Ibdb.Shared.Application.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace Ibdb.Shared.Application.Handlers
{
    internal class EntityUpdateNotificationHandler : INotificationHandler<EntityUpdateNotification>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEventStore _eventStore;
        private readonly IEntityFactory _entityFactory;

        public EntityUpdateNotificationHandler(
            IServiceProvider serviceProvider,
            IEventStore eventSource,
            IEntityFactory entityFactory)
        {
            _serviceProvider = serviceProvider;
            _eventStore = eventSource;
            _entityFactory = entityFactory;
        }

        public async Task Handle(EntityUpdateNotification notification)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            var events = await _eventStore.GetEvents(notification.EntityId);
            var entity = await _entityFactory.Create(events);

            var repositoryType = typeof(IRepository<>).MakeGenericType(entity.GetType());
            var repository = (IRepository)scope.ServiceProvider.GetRequiredService(repositoryType);

            await repository.AddOrUpdate(entity);
        }
    }
}
