using SmileBoy.Client.Core.Dto;
using SmileBoyClient.Core.IContract.IService;
using System;

namespace SmileBoy.Client.Core.IContract.IManagers
{
    public interface ICustomerManager : IOperationBase<CustomerDto, Guid>
    {
    }
}
