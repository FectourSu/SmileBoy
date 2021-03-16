using AutoMapper;
using SmileBoy.Client.Core.Helpers;
using SmileBoy.Client.Core.IContract.IData;
using SmileBoy.Client.Core.Models;
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
    public class ProductService : IProductService
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

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        /// <summary>
        /// Will give records, dividing them into pages and filtering them if necessary
        /// </summary>
        /// <param name="page">The number of the desired page</param>
        /// <param name="pageSize">Number of entries per page</param>
        /// <param name="search">Search filter</param>
        /// <returns></returns>
        public async Task<PaginationResult<ProductDto>> GetAll(int page, int pageSize, string search = null)
        {
            var entites = string.IsNullOrEmpty(search) ? _repository.GetAll() : await _repository.SearchAsync(search);

            return new PaginationResult<ProductDto>
            {
                TotalCount = entites.Count(),
                Values = _mapper.Map<IEnumerable<ProductDto>>(entites
                    .Pagination(page, pageSize))
            };
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            return _mapper.Map<ProductDto>(await _repository.GetByIdAsync(id)
                ?? throw new InvalidOperationException($"Object with this id: {id}, was not found"));
        }

        public async Task InsertAsync(ProductDto model)
        {
            var entity = _mapper.Map<Product>(Has.NotNull(model));
            await _repository.InsertAsync(entity);
        }

        public async Task UpdateAsync(Guid id, ProductDto model)
        {
            var entity = _mapper.Map<Product>(Has.NotNull(model));
            await _repository.UpdateAsync(id, entity);
        }

        public async Task<long> CountAsync()
        {
            return await _repository.CountAsync();
        }
    }
}
