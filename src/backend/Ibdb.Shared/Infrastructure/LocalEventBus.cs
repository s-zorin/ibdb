using Ibdb.Shared.Application;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Ibdb.Shared.Infrastructure
{
    internal class LocalEventBus : ILocalEventBus, ISingletonService
    {
        private readonly IServiceProvider _serviceProvider;

        public LocalEventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Send<TCommand>(TCommand command)
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            await using var scope = _serviceProvider.CreateAsyncScope();

            var commandHandlerType = typeof(ICommandHandler<TCommand>);
            var commandHandler = (ICommandHandler)scope.ServiceProvider.GetRequiredService(commandHandlerType);

            await commandHandler.Handle(command);
        }

        public async Task<TResponse> Request<TResponse>(IRequest<TResponse> request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            await using var scope = _serviceProvider.CreateAsyncScope();

            var requestHandlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var requestHandler = (IRequestHandler)scope.ServiceProvider.GetRequiredService(requestHandlerType);

            return (TResponse)await requestHandler.Handle(request);
        }

        public async Task Publish<TNotification>(TNotification message)
        {
            if (message is null)
                throw new ArgumentNullException(nameof(message));

            await using var scope = _serviceProvider.CreateAsyncScope();

            foreach (var notificationHandler in scope.ServiceProvider
                .GetServices(typeof(INotificationHandler<TNotification>))
                .Cast<INotificationHandler<TNotification>>())
            {
                await notificationHandler.Handle(message);
            }
        }

        public async Task<TResult> Execute<TResult>(IQuery<TResult> query)
        {
            if (query is null)
                throw new ArgumentNullException(nameof(query));

            await using var scope = _serviceProvider.CreateAsyncScope();

            var queryHandlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            var queryHandler = (IQueryHandler)scope.ServiceProvider.GetRequiredService(queryHandlerType);

            return (TResult)await queryHandler.Handle(query);
        }
    }
}
