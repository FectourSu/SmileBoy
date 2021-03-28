using AutoMapper;
using Moq;
using SmileBoy.Client.BL.Services;
using SmileBoy.Client.Core.IContract.IData;
using SmileBoy.Client.Core.Models;
using SmileBoy.Client.Entities;
using SmileBoyClient;
using SmileBoyClient.Core.Entites;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SmileBoy.Client.Tests
{
    public class ProductServiceTests
    {
        private IQueryable<Product> GetProduct() => new Product[]
        {
            new Product() {Name = "A"},
            new Product() {Name = "B"},
            new Product() {Name = "C"},
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
            var mockRepo = new Mock<IRepository<Product, Guid>>();
            mockRepo.Setup(repo => repo.GetAll()).Returns(GetProduct());

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(unit => unit.Products).Returns(mockRepo.Object);

            var service = new ProductService(mockUnitOfWork.Object, CreateMapper());

            var result = await service.GetAll(1, 2);

            Assert.NotNull(result);
            Assert.IsType<PaginationResult<ProductDto>>(result);
            Assert.Equal(3, result.TotalCount);
            Assert.Equal(2, result.Values.Count());
            Assert.Equal("B", result.Values.Last().Name);
        }
    }
}
