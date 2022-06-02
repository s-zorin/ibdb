namespace Ibdb.Shared.Application
{
    public interface IRepository
    {
        Task AddOrUpdate(object entity);
    }

    public interface IRepository<TEntity> : IRepository
    {
        Task IRepository.AddOrUpdate(object entity) => AddOrUpdate((TEntity)entity);

        Task AddOrUpdate(TEntity entity);
    }
}
