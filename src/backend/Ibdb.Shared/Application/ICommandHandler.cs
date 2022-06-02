
namespace Ibdb.Shared.Application
{
    public interface ICommandHandler
    {
        Task<ICommandResult> Handle(object command);
    }

    public interface ICommandHandler<in TCommand> : ICommandHandler
        where TCommand : ICommand
    {
        Task<ICommandResult> ICommandHandler.Handle(object command) => Handle((TCommand)command);

        Task<ICommandResult> Handle(TCommand command);
    }
}
