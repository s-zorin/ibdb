using EasyNetQ;
using Ibdb.Shared.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Ibdb.Shared.Infrastructure
{
    internal class RabbitMqRpcSubscription<TRequestHandler, TRequest, TResponse> : IDisposable
        where TRequestHandler : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDisposable _disposable;

        public RabbitMqRpcSubscription(IBus bus, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _disposable = bus.Rpc.Respond<TRequest, TResponse>(Handle);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private async Task<TResponse> Handle(TRequest arg)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            var requestHandler = (TRequestHandler)scope.ServiceProvider.GetRequiredService(typeof(TRequestHandler));

            return await requestHandler.Handle(arg);
        }
    }
}
