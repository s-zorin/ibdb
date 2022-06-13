namespace Ibdb.Shared.Application.Dtos
{
    public class EventDto
    {
        public Guid Id { get; set; }

        public Guid EntityId { get; set; }

        public string Name { get; set; } = string.Empty;

        public int DataVersion { get; set; }

        public string Data { get; set; } = string.Empty;
    }
}
