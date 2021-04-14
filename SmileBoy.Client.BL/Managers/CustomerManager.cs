using SmileBoy.Client.Core.Dto;
using SmileBoy.Client.Core.IContract;
using SmileBoy.Client.Core.IContract.IData;
using SmileBoy.Client.Core.IContract.IManagers;
using SmileBoy.Client.Core.IContract.IService;
using SmileBoy.Client.Core.Models;
using SmileBoyClient.Core.IContract.IService;
using SmileBoyClient.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmileBoy.Client.Core.Managers
{
    public class CustomerManager : ICustomerManager
    {
        private readonly IReferenceExcludable _excludable;
        private readonly ICustomerService _customerService;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerManager(IUnitOfWork unitOfWork, IReferenceExcludable excludable, ICustomerService customerService)
        {
            _excludable = Has.NotNull(excludable);
            _customerService = Has.NotNull(customerService);
            _unitOfWork = Has.NotNull(unitOfWork);
        }
        /// <summary>
        /// Deletes as well as links to it in all orders
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var entity = await _unitOfWork.Customers.GetByIdAsync(id);

            if (entity == null)
                throw new InvalidOperationException();

            await _excludable.Exclude(o => o.DeleteReferencesCustomer(), entity);
            await _customerService.DeleteAsync(id);
        }

        public async Task<PaginationResult<CustomerDto>> GetAll(int page, int pageSize, string filter = null) =>
            await _customerService.GetAll(page, pageSize, filter);

        public async Task<CustomerDto> GetByIdAsync(Guid id) =>
            await _customerService.GetByIdAsync(id);

        public async Task InsertAsync(CustomerDto model) =>
            await _customerService.InsertAsync(model);

        public async Task UpdateAsync(Guid id, CustomerDto model) =>
            await _customerService.UpdateAsync(id, Has.NotNull(model));
    }
}
