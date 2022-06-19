using Microsoft.AspNetCore.SignalR;

namespace Ibdb.Notifications.Application.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task Register(Guid operationId)
        {
            var connectionId = Context.ConnectionId;

            await Groups
                .AddToGroupAsync(connectionId, operationId.ToString())
                .ContinueWith(_ => Cleanup());

            async Task Cleanup()
            {
                await Task.Delay(TimeSpan.FromMinutes(5));
                await Groups.RemoveFromGroupAsync(connectionId, operationId.ToString());
            }
        }

        public async Task Unregister(Guid operationId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, operationId.ToString());
        }
    }
}
