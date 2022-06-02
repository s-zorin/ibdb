using Ibdb.Shared.Application;
using Ibdb.Shared.Infrastructure.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Ibdb.Shared.Infrastructure
{
    internal sealed class Outbox : IOutbox, IDisposable, IAsyncDisposable, IScopedService
    {
        private readonly OutboxContext _context;
        private readonly IDbContextTransaction _transaction;

        public Outbox(OutboxContext context)
        {
            _context = context;
            _transaction = _context.Database.BeginTransaction(IsolationLevel.Serializable);
        }

        public Task AddEvent(Guid entityId, string eventName, int eventDataVersion, string eventData)
        {
            // TODO
            return Task.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            await _transaction.CommitAsync();

            Dispose(false);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _transaction.Commit();
            }
        }

        public void Dispose()
        {
            Dispose(isDisposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
