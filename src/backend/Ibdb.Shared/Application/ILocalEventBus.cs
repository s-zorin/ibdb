
namespace Ibdb.Shared.Application
{
    public interface ILocalEventBus : IEventBus
    {
        Task Send<TCommand>(TCommand command);

        Task<TResult> Execute<TResult>(IQuery<TResult> query);
    }
}
