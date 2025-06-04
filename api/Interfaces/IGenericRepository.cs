namespace revolutionariesrpg.api.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> AddAsync(Stream reqBody);
        Task<TEntity> Update(Stream reqBody, Guid id);
        Task<bool> Delete(Guid id);
        Task SaveChangesAsync();
    }
}
