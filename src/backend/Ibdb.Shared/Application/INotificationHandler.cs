
namespace Ibdb.Shared.Application
{
    public interface INotificationHandler<TNotification>
    {
        Task Handle(TNotification notification);
    }
}
