using SmileBoy.Client.Entities;
using SmileBoyClient.Core.Entites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmileBoyClient.Core.IContract.IService
{
    public interface IProductService : ICrudService<ProductDto, Guid>
    {
        Task<long> CountAsync();
    }
}
