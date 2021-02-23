using SmileBoy.Client.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmileBoy.Client.Core.IContract.IData
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        Task DeleteAsync(TKey id);

        Task<IEnumerable<TEntity>> GetAllAsync(int page, int size);

        Task<TEntity> GetByIdAsync(TKey id);

        Task InsertAsync(TEntity model);

        Task UpdateAsync(TKey id, TEntity model);

        Task<IEnumerable<TEntity>> FindAsync<TValue>(Expression<Func<TEntity, TValue>> expression, TValue value);
    }
}
