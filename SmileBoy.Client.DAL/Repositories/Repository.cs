using MongoDB.Driver;
using SmileBoy.Client.Core.IContract.IData;
using SmileBoy.Client.Entity;
using SmileBoyClient.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmileBoy.Client.DAL.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : EntityBase<TKey>, new()
    {
        private readonly string _tableName;

        protected readonly IMongoDatabase Database;

        public Repository(IMongoDatabase database, string tablename)
        {
            Database = Has.NotNull(database);

            _tableName = Has.NotNullOrEmpty(tablename);
        }
        public Task DeleteAsync(TKey id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> FindAsync<TValue>(System.Linq.Expressions.Expression<System.Func<TEntity, TValue>> expression, TValue value)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync(int page, int size)
        {
            throw new System.NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(TKey id)
        {
            throw new System.NotImplementedException();
        }

        public Task InsertAsync(TEntity model)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(TKey id, TEntity model)
        {
            throw new System.NotImplementedException();
        }
    }
}
