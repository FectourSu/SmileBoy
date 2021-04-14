using SmileBoy.Client.Entities;
using SmileBoy.Client.Entities.Entities;
using System;

namespace SmileBoy.Client.Core.IContract.IData
{
    public interface IUnitOfWork
    {
        IRepository<Customer, Guid> Customers { get; }
        IRepository<Product, Guid> Products { get; }
        IRepository<Order, Guid> Orders { get; }
    }
}
