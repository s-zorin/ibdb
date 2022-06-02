using Ibdb.Shared.Application.Dtos;

namespace Ibdb.Shared.Application
{
    public interface ICommandResult
    {
        public Guid EntityId { get; }

        public bool Ok { get; }
    }
}
