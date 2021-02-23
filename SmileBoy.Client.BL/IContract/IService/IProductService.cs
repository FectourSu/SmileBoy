using SmileBoy.Client.Entities;
using SmileBoyClient.Core.Entites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmileBoyClient.Core.IContract.IService
{
    interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync(int page, int pageSize);

        Task UpdateAsync(Guid id, Product model);

        Task InsertAsync(Product model);

        Task<ProductDto> GetByIdAsync(Guid id);

        Task DeleteAsync();
    }
}
