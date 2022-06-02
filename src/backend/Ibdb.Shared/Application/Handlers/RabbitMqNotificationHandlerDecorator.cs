using EasyNetQ;

namespace Ibdb.Shared.Application.Handlers
{
    internal class RabbitMqNotificationHandlerDecorator<TNotification> : INotificationHandler<TNotification>
    {
        private readonly INotificationHandler<TNotification> _inner;

        public RabbitMqNotificationHandlerDecorator(INotificationHandler<TNotification> inner, IBus bus)
        {
            _inner = inner;

            bus.PubSub.Subscribe<TNotification>(Guid.NewGuid().ToString(), Handle);
        }

        public Task Handle(TNotification notification)
        {
            return _inner.Handle(notification);
        }
    }
}
