using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Dtos;
using Ibdb.Shared.Application.Notifications;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Ibdb.Shared.Infrastructure
{
    internal class OperationTracker : IOperationTracker, ITransientService
    {
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IMapper _mapper;
        private readonly ILogger<OperationTracker> _logger;

        public OperationTracker(IDistributedEventBus distributedEventBus, IMapper mapper, ILogger<OperationTracker> logger)
        {
            _distributedEventBus = distributedEventBus;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Completed(Task completedTask, Guid operationId)
        {
            if (completedTask.Status is not (TaskStatus.Faulted or TaskStatus.RanToCompletion))
                return;

            var exceptions = completedTask.Exception?.Flatten().InnerExceptions.ToList() ?? new List<Exception>();

            foreach (var exception in exceptions)
                _logger.LogError(exception, "Unhandled exception during command execution.");

            var errors = exceptions.Select(e => _mapper.Map<ErrorDto>(e)).Distinct().ToArray();

            await _distributedEventBus.Publish(new OperationCompletedNotification(operationId, errors));
        }
    }
}
