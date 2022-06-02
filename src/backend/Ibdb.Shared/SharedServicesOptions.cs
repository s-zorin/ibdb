namespace Ibdb.Shared
{
    public class SharedServicesOptions
    {
        public string? EventStoreConnectionString { get; set; }

        public string? OutboxConnectionString { get; set; }

        public string? RabbitMqConnectionString { get; set; }
    }
}
