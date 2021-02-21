using SmileBoyClient.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmileBoyClient.Core.IContract.IService
{
    interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task DeleteAsync();
    }
}
