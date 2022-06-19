
namespace Ibdb.Shared.Application
{
    public interface ICommandHandler
    {
        Task Handle(object command);
    }

    public interface ICommandHandler<in TCommand> : ICommandHandler
    {
        Task ICommandHandler.Handle(object command) => Handle((TCommand)command);

        Task Handle(TCommand command);
    }
}
