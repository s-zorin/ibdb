namespace Ibdb.Shared.Application.Dtos
{
    public class EventDto
    {
        public Guid Id { get; set; }

        public Guid EntityId { get; set; }

        public DateTime Timestamp { get; set; }

        public string? Name { get; set; }

        public int EntityVersion { get; set; }

        public int DataVersion { get; set; }

        public string? Data { get; set; }
    }
}
