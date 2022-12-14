using Npgsql;
using Polly;

namespace Ibdb.Shared.Application.Handlers
{
    internal class RetryCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _inner;

        public RetryCommandHandlerDecorator(ICommandHandler<TCommand> inner)
        {
            _inner = inner;
        }

        public Task Handle(TCommand command)
        {
            var retryPolicy = Policy
                .Handle<NpgsqlException>()
                .WaitAndRetryAsync(10, _ => TimeSpan.FromMilliseconds(100));

            return retryPolicy.ExecuteAsync(() => _inner.Handle(command));
        }
    }
}
