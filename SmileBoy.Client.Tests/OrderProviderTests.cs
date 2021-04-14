using AutoMapper;
using Moq;
using SmileBoy.Client.Core.Dto;
using SmileBoy.Client.Core.IContract.IData;
using SmileBoy.Client.Core.IContract.IService;
using SmileBoy.Client.Entities.Entities;
using SmileBoyClient;
using SmileBoyClient.Core.Entites;
using SmileBoyClient.Core.IContract.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SmileBoy.Client.Tests
{
    public class OrderProviderTests
    {
        private const string CustomerId = "106ca36e-0fbf-46e7-b641-b2d40ae8ac99";
        private const string ProductId = "106ca36e-0fbf-46e7-b641-b2d40ae8ac99";
        private const string OrderId = "106ca36e-0fbf-46e7-b641-b2d40ae8ac99";

        private IQueryable<Order> GetOrders() => new Order[]
            {
                new Order()
                {
                    Id = new Guid(OrderId),
                    Amount = 100M,
                    CustomerId = Guid.Parse(CustomerId),
                    ProductsIds = new List<Guid>
                    {
                        Guid.Parse(ProductId)
                    }
                }
            }.AsQueryable();

        private IMapper CreateMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationProfile());
            });

            return mapper.CreateMapper();
        }

        [Fact]
        public async Task GetAllAsync()
        {
            //Arrange
            var mockProducts = new Mock<IProductService>();
            mockProducts.Setup(repo => repo.GetByIdAsync(Guid.Parse(ProductId)))
                .Returns(Task.FromResult(new ProductDto
                {
                    Name = "Test product",
                    CurrentPrice = 100
                }));

            var mockCustomer = new Mock<ICustomerService>();
            mockCustomer.Setup(repo => repo.GetByIdAsync(Guid.Parse(CustomerId)))
                .Returns(Task.FromResult(new CustomerDto
                {
                    Name = "Test customer"
                }));

            var mockRepo = new Mock<IRepository<Order, Guid>>();
        }
    }
}
