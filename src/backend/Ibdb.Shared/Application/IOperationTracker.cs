
namespace Ibdb.Shared.Application
{
    public interface IOperationTracker
    {
        Task Completed(Task task, Guid operationId);
    }
}
