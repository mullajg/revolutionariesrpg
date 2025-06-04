using RevolutionariesApi.Repositories;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        private bool _disposed = false;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            _repositories = new Dictionary<Type, object>();
        }

        public async Task<int> CommitAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            _disposed = true;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
        {
            //Return the repository if it already exists
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return (IGenericRepository<TEntity>)_repositories[typeof(TEntity)];
            }

            var repository = new GenericRepository<TEntity>(_db);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }
    }
}