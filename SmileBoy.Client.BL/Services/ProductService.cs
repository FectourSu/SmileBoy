using AutoMapper;
using SmileBoy.Client.Core.IContract.IData;
using SmileBoy.Client.Entities;
using SmileBoyClient.Core.Entites;
using SmileBoyClient.Core.IContract.IService;
using SmileBoyClient.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmileBoy.Client.BL.Services
{
    class ProductService : IProductService
    {
        /// <summary>
        /// Buisness logic accessing data via mapping
        /// </summary>
        
        private readonly IRepository<Product, Guid> _repository;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = Has.NotNull(unitOfWork).Products;
            _mapper = Has.NotNull(mapper);
        }

        public Task DeleteAsync(Guid id)
        {
            return _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(int page, int pageSize)
        {
            return (await _repository.GetAllAsync(page, pageSize)).Select(e => _mapper.Map<ProductDto>(e));
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            return _mapper.Map<ProductDto>(await _repository.GetByIdAsync(id)
                ?? throw new InvalidOperationException($"Object with this id: {id}, was not found"));
        }

        public async Task InsertAsync(Product model)
        {
            var entity = _mapper.Map<Product>(Has.NotNull(model));
            await _repository.InsertAsync(entity);
        }

        public async Task UpdateAsync(Guid id, Product model)
        {
            var entity = _mapper.Map<Product>(Has.NotNull(model));
            await _repository.UpdateAsync(id, entity);
        }
    }
}
