namespace Ibdb.Shared.Application
{
    public interface IEventHandler
    {
        string Name { get; }

        int DataVersion { get; }

        object Handle(Guid entityId, object? entity, object data);
    }

    public interface IEventHandler<TEntity, TEventData> : IEventHandler
    {
        object IEventHandler.Handle(Guid entityId, object? entity, object data) => Handle(entityId, (TEntity?)entity, (TEventData)data)!;

        TEntity Handle(Guid entityId, TEntity? entity, TEventData data);
    }
}
