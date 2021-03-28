using SmileBoy.Client.Core.Models;
using System;
using System.Threading.Tasks;

namespace SmileBoyClient.Core.IContract.IService
{
    public interface IOperationBase<TModel, TKey>
        where TModel : IModel
    {
        Task<PaginationResult<TModel>> GetAll(int page, int pageSize, string filter = null);

        Task UpdateAsync(TKey id, TModel model);

        Task InsertAsync(TModel model);

        Task<TModel> GetByIdAsync(TKey id);

        Task DeleteAsync(TKey id);
    }
}