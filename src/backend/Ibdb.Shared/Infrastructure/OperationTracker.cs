using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Dtos;
using Ibdb.Shared.Application.Exceptions;
using Ibdb.Shared.Application.Notifications;
using System.Data;

namespace Ibdb.Shared.Infrastructure
{
    internal class OperationTracker : IOperationTracker, ITransientService
    {
        private readonly IDistributedEventBus _distributedEventBus;

        public OperationTracker(IDistributedEventBus distributedEventBus)
        {
            _distributedEventBus = distributedEventBus;
        }

        public async Task Completed(Task completedTask, Guid operationId)
        {
            if (completedTask.Status is not (TaskStatus.Faulted or TaskStatus.RanToCompletion))
                return;

            var errors = completedTask.Exception?.Flatten()
                .InnerExceptions.Select(ToError).Distinct().ToArray() ?? Array.Empty<ErrorDto>();

            await _distributedEventBus.Publish(new OperationCompletedNotification(operationId, errors));

            static ErrorDto ToError(Exception e)
            {
                return e switch
                {
                    ValidationException ve => new(ve.Code, ve.Message),
                    BusinessException be => new(be.Code, be.Message),
                    _ => new("INTERNAL_SERVER_ERROR", "Internal server error."),
                };
            }
        }
    }
}
