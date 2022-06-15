namespace Ibdb.Shared.Application
{
    public interface ILocalEventBus : IEventBus
    {
        public Task<ICommandResult> Send(ICommand command);

        public Task<TResult> Execute<TResult>(IQuery<TResult> query);
    }
}
