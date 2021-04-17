using AutoMapper;
using SmileBoy.Client.Core.Dto;
using SmileBoy.Client.Core.Helpers;
using SmileBoy.Client.Core.IContract.IData;
using SmileBoy.Client.Core.IContract.IService;
using SmileBoy.Client.Core.Models;
using SmileBoy.Client.Entities.Entities;
using SmileBoyClient.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmileBoy.Client.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer, Guid> _repository;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = Has.NotNull(unitOfWork).Customers;
            _mapper = Has.NotNull(mapper);
        }

        public async Task DeleteAsync(Guid id) => await _repository.DeleteAsync(id);

        public async Task<PaginationResult<CustomerDto>> GetAll(int page, int pageSize, string filter = null)
        {
            var entites = string.IsNullOrEmpty(filter) ? _repository.GetAll() : await _repository.SearchAsync(filter);

            return new PaginationResult<CustomerDto>
            {
                TotalCount = entites.Count(),
                Values = _mapper.Map<IEnumerable<CustomerDto>>(entites
                    .Pagination(page, pageSize))
            };
        }

        public async Task<CustomerDto> GetByIdAsync(Guid id)
        {
            return _mapper.Map<CustomerDto>(await _repository.GetByIdAsync(id)
                ?? throw new InvalidOperationException($"Object with this id: {id}, was not found"));
        }

        public async Task InsertAsync(CustomerDto model)
        {
            var entity = _mapper.Map<Customer>(Has.NotNull(model));
            await _repository.InsertAsync(entity);
        }

        public async Task UpdateAsync(Guid id, CustomerDto model)
        {
            var entity = _mapper.Map<Customer>(Has.NotNull(model));
            await _repository.UpdateAsync(id, entity);
        }

        public async Task<CustomerDto> SingleOrDefaultAsync(Guid id)
        {
            return _mapper.Map<CustomerDto>(await _repository.GetByIdAsync(id));
        }
    }
}
