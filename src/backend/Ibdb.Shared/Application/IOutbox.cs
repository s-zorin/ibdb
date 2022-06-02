namespace Ibdb.Shared.Application
{
    public interface IOutbox
    {
        Task AddEvent(Guid entityId, string eventName, int eventDataVersion, string eventData);
    }
}
