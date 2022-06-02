using Ibdb.Shared.Application;

namespace Ibdb.Shared.Infrastructure
{
    internal class JsonSerializer : IJsonSerializer, ITransientService
    {
        public Task<T> Deserialize<T>(string json)
        {
            return Task.FromResult(System.Text.Json.JsonSerializer.Deserialize<T>(json)!);
        }

        public Task<object> Deserialize(Type t, string json)
        {
            return Task.FromResult(System.Text.Json.JsonSerializer.Deserialize(json, t)!);
        }

        public Task<string> Serialize<T>(T obj)
        {
            return Task.FromResult(System.Text.Json.JsonSerializer.Serialize(obj));
        }

        public Task<string> Serialize<T>(Action<T> objAction)
            where T : new()
        {
            T obj = new();
            objAction(obj);

            return Task.FromResult(System.Text.Json.JsonSerializer.Serialize(obj));
        }
    }
}
