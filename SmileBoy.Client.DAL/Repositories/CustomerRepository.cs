using MongoDB.Driver;
using SmileBoy.Client.Core.IContract.IData;
using SmileBoy.Client.Entities.Entities;
using System;

namespace SmileBoy.Client.DAL.Repositories
{
    public class CustomerRepository : Repository<Customer, Guid>, IRepository<Customer, Guid>
    {
        public CustomerRepository(IMongoDatabase database)
            : base(database, "Customers")
        {
        }
    }
}
