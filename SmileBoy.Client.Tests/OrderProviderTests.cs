using AutoMapper;
using Moq;
using SmileBoy.Client.Core.Dto;
using SmileBoy.Client.Core.IContract.IData;
using SmileBoy.Client.Core.IContract.IService;
using SmileBoy.Client.Core.Providers;
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
                    Number = "Test orders",
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
            mockRepo.Setup(repo => repo.GetAll()).Returns(GetOrders());

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Orders).Returns(mockRepo.Object);

            var provider = new OrderProvider(mockUnitOfWork.Object, mockCustomer.Object, mockProducts.Object, CreateMapper());

            //Act
            var result = await provider.GetAllAsync(1, 1);

            Assert.NotNull(result);

            var firstOrder = result.Values.FirstOrDefault();

            Assert.Equal("Test orders", firstOrder.Number);

            Assert.NotNull(firstOrder.Customer);
            Assert.IsType<CustomerDto>(firstOrder.Customer);
            Assert.Equal("Test customer", firstOrder.Customer.Name);

            Assert.NotNull(firstOrder.Products);
            Assert.IsType<ProductDto>(firstOrder.Products.First());
            Assert.Equal("Test product", firstOrder.Products.First().Name);
        }
    }
}
