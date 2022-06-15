
namespace Ibdb.Shared.Application
{
    public interface IQueryHandler
    {
        Task<object> Handle(object query);
    }

    public interface IQueryHandler<in TQuery, TResult> : IQueryHandler
        where TQuery : IQuery<TResult>
    {
        Task<object> IQueryHandler.Handle(object query) => Cast(Handle((TQuery)query));

        Task<TResult> Handle(TQuery query);

        private static Task<object> Cast(Task<TResult> task)
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
