using EasyNetQ;
using Ibdb.Shared.Application;

namespace Ibdb.Shared.Infrastructure
{
    internal class RabbitMqDistributedEventBus : IDistributedEventBus, ISingletonService
    {
        private readonly IBus _bus;

        public RabbitMqDistributedEventBus(IBus bus)
        {
            _bus = bus;
        }

        public async Task<TResponse> Request<TResponse>(IRequest<TResponse> request)
        {
            return await _bus.Rpc.RequestAsync<IRequest<TResponse>, TResponse>(request);
        }

        public Task Publish<TNotification>(TNotification message)
        {
            _bus.PubSub.Publish(message);
            return Task.CompletedTask;
        }
    }
}
