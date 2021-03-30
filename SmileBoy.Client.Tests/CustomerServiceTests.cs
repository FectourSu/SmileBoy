using AutoMapper;
using Moq;
using SmileBoy.Client.Core.Dto;
using SmileBoy.Client.Core.IContract.IData;
using SmileBoy.Client.Core.Models;
using SmileBoy.Client.Core.Services;
using SmileBoy.Client.Entities.Entities;
using SmileBoyClient;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SmileBoy.Client.Tests
{
    public class CustomerServiceTests
    {
        private IQueryable<Customer> GetCustomers() => new Customer[]
       {
            new Customer() {Name = "A"},
            new Customer() {Name = "B"},
            new Customer() {Name = "C"},
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
        public async Task GetAll()
        {
            var mockRepo = new Mock<IRepository<Customer, Guid>>();
            mockRepo.Setup(repo => repo.GetAll()).Returns(GetCustomers());

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(unit => unit.Customers).Returns(mockRepo.Object);

            var service = new CustomerService(mockUnitOfWork.Object, CreateMapper());

            var result = await service.GetAll(1, 2);

            Assert.NotNull(result);
            Assert.IsType<PaginationResult<CustomerDto>>(result);
            Assert.Equal(3, result.TotalCount);
            Assert.Equal(2, result.Values.Count());
            Assert.Equal("B", result.Values.Last().Name);
        }
    }
}
