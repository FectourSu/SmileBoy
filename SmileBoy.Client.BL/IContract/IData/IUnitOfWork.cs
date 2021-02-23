using SmileBoy.Client.Entities;
using System;

namespace SmileBoy.Client.Core.IContract.IData
{
    public interface IUnitOfWork
    {
        IRepository<Product, Guid> Products { get; }
    }
}
