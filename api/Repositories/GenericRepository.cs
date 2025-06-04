using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using revolutionariesrpg.api.Interfaces;
using revolutionariesrpg.api;

namespace RevolutionariesApi.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly AppDbContext _db;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = db.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(Stream reqBody)
        {
            string requestBody = await new StreamReader(reqBody).ReadToEndAsync();
            var entity = JsonConvert.DeserializeObject<TEntity>(requestBody);
            //commented out until I figure out authorization
            //await _dbSet.AddAsync(entity);

            return entity;
        }

        //returns true if successful 
        //returns false is failure
        public async Task<bool> Delete(Guid id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(pc => pc.Id == id);
            if (entity == null)
            {
                return false;
            }

            //commented out until I figure out authorization
            //_dbSet.Remove(entity);
            return true;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<TEntity> Update(Stream reqBody, Guid id)
        {
            string requestBody = await new StreamReader(reqBody).ReadToEndAsync();
            var entity = JsonConvert.DeserializeObject<TEntity>(requestBody);
            entity.Id = id;
            _dbSet.Update(entity);

            return entity;
        }
    }
}
