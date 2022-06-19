namespace Ibdb.Shared
{
    public static class SharedServicesOptionsExtensions
    {
        public static SharedServicesOptions AddEventStore(this SharedServicesOptions options, string connectionString)
        {
            options.EventStoreConnectionString = connectionString;
            return options;
        }

        public static SharedServicesOptions AddOutbox(this SharedServicesOptions options, string connectionString)
        {
            options.OutboxConnectionString = connectionString;
            return options;
        }

        public static SharedServicesOptions AddRabbitMq(this SharedServicesOptions options, string connectionString)
        {
            options.RabbitMqConnectionString = connectionString;
            return options;
        }
    }
}
