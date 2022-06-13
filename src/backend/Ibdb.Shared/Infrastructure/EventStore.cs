using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Dtos;
using Ibdb.Shared.Application.Notifications;
using Ibdb.Shared.Domain;
using Ibdb.Shared.Infrastructure.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Ibdb.Shared.Infrastructure
{
    internal sealed class EventStore : IEventStore, IAsyncDisposable, IScopedService
    {
        private readonly EventStoreContext _context;
        private readonly IMapper _mapper;
        private readonly ILocalEventBus _localEventBus;
        private readonly IOutbox _outbox;
        private readonly IDbContextTransaction _transaction;
        private readonly HashSet<Event> _addedEvents;

        private bool _isDisposed;

        public EventStore(EventStoreContext context, IMapper mapper, ILocalEventBus localEventBus, IOutbox outbox)
        {
            _context = context;
            _mapper = mapper;
            _localEventBus = localEventBus;
            _outbox = outbox;
            _transaction = _context.Database.BeginTransaction(IsolationLevel.Serializable);
            _addedEvents = new HashSet<Event>();
        }

        public async Task AddEvent(Guid entityId, string eventName, int eventDataVersion, string eventData)
        {
            var newEvent = new Event
            {
                Name = eventName,
                EntityId = entityId,
                Data = eventData,
                DataVersion = eventDataVersion
            };

            var mostRecentEvent = await _context.Events
                .Where(e => e.EntityId == entityId)
                .OrderBy(e => e.EntityVersion)
                .LastOrDefaultAsync();

            if (mostRecentEvent is null)
            {
                newEvent.EntityVersion = 1;
                _context.Events.Add(newEvent);
            }
            else
            {
                newEvent.EntityVersion = mostRecentEvent.EntityVersion + 1;
                _context.Events.Add(newEvent);
            }

            await _context.SaveChangesAsync();

            _addedEvents.Add(newEvent);
        }

        public async Task<ICollection<EventDto>> GetEvents(Guid entityId)
        {
            var events = await _context.Events
                .Where(e => e.EntityId == entityId)
                .OrderBy(e => e.EntityVersion)
                .ToListAsync();

            return _mapper.Map<ICollection<EventDto>>(events);
        }

        public async ValueTask DisposeAsync()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            foreach (var e in _addedEvents)
            {
                await _outbox.AddEvent(e.EntityId, e.Name, e.DataVersion, e.Data);
            }

            await _transaction.CommitAsync();

            foreach (var entityId in _addedEvents.Select(e => e.EntityId).Distinct())
            {
                await _localEventBus.Publish(new EntityUpdateNotification { EntityId = entityId });
            }

            GC.SuppressFinalize(this);
        }
    }
}
