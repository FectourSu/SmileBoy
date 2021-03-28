using SmileBoyClient.Core.Entites;
using SmileBoyClient.Core.IContract.IService;
using System;

namespace SmileBoy.Client.Core.IContract
{
    public interface IProductManager : IOperationBase<ProductDto, Guid>
    {
    }
}
