using MongoDB.Driver;
using SmileBoy.Client.Core.IContract.IData;
using SmileBoy.Client.Entities;
using System;

namespace SmileBoy.Client.DAL.Repositories
{
    public class ProductRepository : Repository<Product, Guid>, IRepository<Product, Guid>
    {
        public ProductRepository(IMongoDatabase database) 
            : base(database, "Products")
        {
        }
    }
}
