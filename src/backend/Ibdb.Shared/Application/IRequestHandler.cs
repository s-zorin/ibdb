
namespace Ibdb.Shared.Application
{
    public interface IRequestHandler
    {
        Task<object> Handle(object request);
    }

    public interface IRequestHandler<in TRequest, TResponse> : IRequestHandler
        where TRequest : IRequest<TResponse>
    {
        Task<object> IRequestHandler.Handle(object request) => IRequestHandler<TRequest, TResponse>.Cast(Handle((TRequest)request));

        Task<TResponse> Handle(TRequest request);

        private static Task<object> Cast(Task<TResponse> task)
        {
            var tcs = new TaskCompletionSource<object>();

            task.ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    tcs.TrySetException(t.Exception!.InnerExceptions);
                }
                else if (t.IsCanceled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetResult(t.Result!);
                }
            }, TaskContinuationOptions.ExecuteSynchronously);

            return tcs.Task;
        }
    }
}
