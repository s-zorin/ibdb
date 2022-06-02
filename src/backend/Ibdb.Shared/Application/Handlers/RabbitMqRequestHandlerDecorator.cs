using EasyNetQ;

namespace Ibdb.Shared.Application.Handlers
{
    internal class RabbitMqRequestHandlerDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;

        public RabbitMqRequestHandlerDecorator(IRequestHandler<TRequest, TResponse> inner, IBus bus)
        {
            _inner = inner;

            bus.Rpc.Respond<TRequest, TResponse>(request => Handle(request));
        }

        public Task<TResponse> Handle(TRequest request)
        {
            return _inner.Handle(request);
        }
    }
}
