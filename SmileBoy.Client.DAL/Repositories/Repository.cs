using MongoDB.Driver;
using SmileBoy.Client.Core.IContract.IData;
using SmileBoy.Client.Entity;
using SmileBoyClient.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmileBoy.Client.DAL.Repositories
{
    /// <summary>
    /// general CRUD implementation for TEntity objects
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
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


        /// <summary>
        /// Deletes an entry by the specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(TKey id)
        {
            var collection = Database.GetCollection<TEntity>(_tableName);

            var filter = Builders<TEntity>.Filter.Eq("Id", id);

            await collection.DeleteOneAsync(filter);
        }

        /// <summary>
        /// Native find wrapper from entity framework
        /// Searching record
        /// </summary>
        /// <param name="expression">An expression that specifies the property that will be searched for</param>
        /// <param name="value">The value to search for</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FindAsync<TValue>(Expression<Func<TEntity, TValue>> expression, TValue value)
        {
            var memeberExpression = expression.Body as MemberExpression;

            if (memeberExpression == null)
                throw new InvalidOperationException();

            var collection = Database.GetCollection<TEntity>(_tableName);
            var filter = Builders<TEntity>.Filter.Eq(memeberExpression.Member.Name, value);

            using (var entities = await collection.FindAsync(filter))
            {
                return entities.ToList();
            }
        }

        /// <summary>
        /// Retrieves records from the database broken down by page
        /// </summary>
        /// <typeparam name="TModel">The model that forms the entity</typeparam>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync(int page, int pageSize)
        {
            var collection = Database.GetCollection<TEntity>(_tableName);

            using (var entities = await collection.FindAsync(Builders<TEntity>.Filter.Empty))
            {
                return entities.ToList()
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
            }
        }

        /// <summary>
        /// Retrieves a record by the specified id
        /// </summary>
        /// <returns></returns>
        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            var collection = Database.GetCollection<TEntity>(_tableName);

            var filter = Builders<TEntity>.Filter.Eq("Id", id);

            using(var entities = await collection.FindAsync(filter))
            {
                return await entities.FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// Add an entry
        /// </summary>
        /// <returns></returns>
        public async Task InsertAsync(TEntity entity)
        {
            var collection = Database.GetCollection<TEntity>(_tableName);
            entity.CreatedBy = entity.UpdateBy = DateTime.Now;

            await collection.InsertOneAsync(entity);
        }

        /// <summary>
        /// Updates a record in the database,
        /// if the record cannot be found by the specified id, a new one will be added
        /// </summary>
        /// <returns></returns>
        public async Task UpdateAsync(TKey id, TEntity entity)
        {
            var collection = Database.GetCollection<TEntity>(_tableName);
            var filter = Builders<TEntity>.Filter.Eq("Id", id);

            var foundEntity = await GetByIdAsync(id);

            entity.CreatedBy = foundEntity?.CreatedBy ?? DateTime.Now;
            entity.UpdateBy = DateTime.Now;

            await collection.ReplaceOneAsync(filter, entity,
                new ReplaceOptions { IsUpsert = true });
        }
    }
}
