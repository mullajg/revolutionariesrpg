using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;
        Task<int> CommitAsync();
    }
}
