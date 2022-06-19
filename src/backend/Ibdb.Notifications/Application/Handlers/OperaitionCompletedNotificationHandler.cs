using Ibdb.Notifications.Application.Hubs;
using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Notifications;
using Microsoft.AspNetCore.SignalR;

namespace Ibdb.Notifications.Application.Handlers
{
    public class OperaitionCompletedNotificationHandler : INotificationHandler<OperationCompletedNotification>
    {
        private readonly IHubContext<NotificationHub> _context;

        public OperaitionCompletedNotificationHandler(IHubContext<NotificationHub> context)
        {
            _context = context;
        }

        public async Task Handle(OperationCompletedNotification notification)
        {
            var operationId = notification.OperationId.ToString();

            await _context.Clients.Group(operationId).SendAsync("Completed", operationId);
        }
    }
}
