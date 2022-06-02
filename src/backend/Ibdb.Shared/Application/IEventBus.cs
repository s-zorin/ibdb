namespace Ibdb.Shared.Application
{
    public interface IEventBus
    {
        Task<TResponse> Request<TResponse>(IRequest<TResponse> request);

        Task Publish<TNotification>(TNotification message);
    }
}
