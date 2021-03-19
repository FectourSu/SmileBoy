using MongoDB.Driver;
using SmileBoy.Client.Core.IContract.IData;
using SmileBoy.Client.DAL.Repositories;
using SmileBoy.Client.Entities;
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
    }
}
