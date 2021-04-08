using MongoDB.Driver;
using SmileBoy.Client.Core.IContract.IData;
using SmileBoy.Client.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmileBoy.Client.DAL.Repositories
{
    public class OrderRepository : Repository<Orders, Guid>, IRepository<Orders, Guid>
    {
        public OrderRepository(IMongoDatabase database)
          : base(database, "Orders", searchable: true)
        {
        }
    }
}
