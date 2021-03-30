using SmileBoy.Client.Core.Dto;
using SmileBoyClient.Core.IContract.IService;
using System;
using System.Threading.Tasks;

namespace SmileBoy.Client.Core.IContract.IService
{
    public interface ICustomerService : IOperationBase<CustomerDto, Guid>
    {
        Task<CustomerDto> SingleOrDefaultAsync(Guid id);
    }
}
