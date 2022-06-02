namespace Ibdb.Shared.Application
{
    public interface IJsonSerializer
    {
        Task<string> Serialize<T>(T obj);

        Task<string> Serialize<T>(Action<T> objAction) where T : new();

        Task<T> Deserialize<T>(string json);

        Task<object> Deserialize(Type t, string json);
    }
}
