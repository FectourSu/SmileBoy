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
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : EntityBase<TKey>, new()
    {

        protected readonly IMongoCollection<TEntity> _collection;

        private readonly string _tableName;

        protected readonly IMongoDatabase Database;

        public Repository(IMongoDatabase database, string tablename, bool searchable = true)
        {
            _collection = Has.NotNull(database).GetCollection<TEntity>(tablename);

            if (searchable)
                CreateIndex();
        }

        private void CreateIndex()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Deletes an entry by the specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(TKey id)
        {
            var collection = Database.GetCollection<TEntity>(_tableName);

            var filter = Builders<TEntity>.Filter.Eq("Id", id);

            await collection.DeleteOneAsync(filter);
        }

        /// <summary>
        /// Gets a record in the data database divided by pages
        /// </summary>
        /// <typeparam name="TModel">The model that the initial entity will be converted to</typeparam>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Number of entries per page</param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetAll() => _collection.AsQueryable();

        public virtual async Task<IEnumerable<TEntity>> SearchAsync(string search)
        {
            var filter = Builders<TEntity>.Filter.Text(search);

            return (await _collection.FindAsync(filter))
                .ToList();
        }

        /// <summary>
        /// Retrieves a record by the specified id
        /// </summary>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByIdAsync(TKey id)
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
        public virtual async Task InsertAsync(TEntity entity)
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
        public virtual async Task UpdateAsync(TKey id, TEntity entity)
        {
            var collection = Database.GetCollection<TEntity>(_tableName);
            var filter = Builders<TEntity>.Filter.Eq("Id", id);

            var foundEntity = await GetByIdAsync(id);

            entity.CreatedBy = foundEntity?.CreatedBy ?? DateTime.Now;
            entity.UpdateBy = DateTime.Now;

            await collection.ReplaceOneAsync(filter, entity,
                new ReplaceOptions { IsUpsert = true });
        }

        public Task<long> CountAsync()
        {
            throw new NotImplementedException();
        }
    }
}
