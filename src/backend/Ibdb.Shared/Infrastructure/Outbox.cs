using Ibdb.Shared.Application;
using Ibdb.Shared.Domain;
using Ibdb.Shared.Infrastructure.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Ibdb.Shared.Infrastructure
{
    internal sealed class Outbox : IOutbox, IAsyncDisposable, IScopedService
    {
        private readonly OutboxContext _context;
        private readonly IDbContextTransaction _transaction;

        private bool _isDisposed;

        public Outbox(OutboxContext context)
        {
            _context = context;
            _transaction = _context.Database.BeginTransaction(IsolationLevel.Serializable);
        }

        public async Task AddEvent(Guid entityId, string eventName, int eventDataVersion, string eventData)
        {
            var newEvent = new OutboxEvent
            {
                Name = eventName,
                EntityId = entityId,
                Data = eventData,
                DataVersion = eventDataVersion
            };

            _context.Events.Add(newEvent);

            await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            await _transaction.CommitAsync();

            GC.SuppressFinalize(this);
        }
    }
}
