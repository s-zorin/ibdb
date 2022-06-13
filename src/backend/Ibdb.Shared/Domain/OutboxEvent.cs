
namespace Ibdb.Shared.Domain
{
    public sealed class OutboxEvent
    {
        public Guid Id { get; set; }

        public DateTime Timestamp { get; set; }

        public string Name { get; set; } = string.Empty;

        public Guid EntityId { get; set; }

        public int DataVersion { get; set; }

        public string Data { get; set; } = string.Empty;
    }
}
