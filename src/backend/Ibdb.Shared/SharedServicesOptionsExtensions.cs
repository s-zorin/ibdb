namespace Ibdb.Shared
{
    public static class SharedServicesOptionsExtensions
    {
        public static SharedServicesOptions SetEventStoreConnectionString(this SharedServicesOptions options, string connectionString)
        {
            options.EventStoreConnectionString = connectionString;
            return options;
        }

        public static SharedServicesOptions SetOutboxConnectionString(this SharedServicesOptions options, string connectionString)
        {
            options.OutboxConnectionString = connectionString;
            return options;
        }

        public static SharedServicesOptions SetRabbitMqConnectionString(this SharedServicesOptions options, string connectionString)
        {
            options.RabbitMqConnectionString = connectionString;
            return options;
        }
    }
}
