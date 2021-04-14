using SmileBoy.Client.Core.Dto;
using SmileBoy.Client.Core.Models;
using System;
using System.Threading.Tasks;

namespace SmileBoy.Client.Core.IContract.IProviders
{
    public interface IOrderProvider
    {
        Task<PaginationResult<OrderDto>> GetAllAsync(int page, int pageSize, string filter = null);

        Task<OrderDto> GetByIdAsync(Guid id);

        Task InsertAsync(OrderDto model);

        Task UpdateAsync(Guid id, OrderUpdate model);

        Task DeleteAsync(Guid id);
    }
}
