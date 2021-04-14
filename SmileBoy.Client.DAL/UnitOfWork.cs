using MongoDB.Driver;
using SmileBoy.Client.Core.IContract.IData;
using SmileBoy.Client.DAL.Repositories;
using SmileBoy.Client.Entities;
using SmileBoy.Client.Entities.Entities;
using System;

namespace SmileBoy.Client.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(string connectionString, string dataBaseName)
        {
            var client = new MongoClient(connectionString);

            _database = client.GetDatabase(dataBaseName);
        }

        private readonly IMongoDatabase _database;

        private IRepository<Product, Guid> _products;
        public IRepository<Product, Guid> Products => _products ??= new ProductRepository(_database);

        private IRepository<Customer, Guid> _customers;
        public IRepository<Customer, Guid> Customers => _customers ??= new CustomerRepository(_database);

        private IRepository<Order, Guid> _orders;
        public IRepository<Order, Guid> Orders => _orders ??= new OrderRepository(_database);
    }
}
