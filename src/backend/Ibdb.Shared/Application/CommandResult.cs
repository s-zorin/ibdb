namespace Ibdb.Shared.Application
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(Guid entityId, bool ok)
        {
            EntityId = entityId;
            Ok = ok;
        }

        public Guid EntityId { get; }

        public bool Ok { get; }
    }
}
