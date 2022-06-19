using Ibdb.Shared.Application.Dtos;

namespace Ibdb.Shared.Application.Notifications
{
    public record OperationCompletedNotification(Guid OperationId, ErrorDto[] Errors);
}
