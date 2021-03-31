using SmileBoy.Client.Core.IContract;
using SmileBoy.Client.Core.IContract.IData;
using SmileBoy.Client.Core.Models;
using SmileBoyClient.Core.Entites;
using SmileBoyClient.Core.IContract.IService;
using SmileBoyClient.Helpers;
using System;
using System.Threading.Tasks;

namespace SmileBoy.Client.Core.Managers
{
    public class ProductManager : IProductManager
    {
        private readonly IProductService _productService;
        private readonly IUnitOfWork _unitOfWork;

        public ProductManager(IUnitOfWork unitOfWork, IProductService productService)
        {
            _productService = Has.NotNull(productService);
            _unitOfWork = Has.NotNull(unitOfWork);
        }
        /// <summary>
        /// Deletes as well as links to it in all orders
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            //soon

            //var entity = await _unitOfWork.Products.GetByIdAsync(id);
            //Has.NotNull(entity);

            await _productService.DeleteAsync(id); 
        }

        public async Task<PaginationResult<ProductDto>> GetAll(int page, int pageSize, string filter = null) =>
            await _productService. GetAll(page, pageSize, filter);

        public async Task<ProductDto> GetByIdAsync(Guid id) =>
            await _productService.GetByIdAsync(id);

        public async Task InsertAsync(ProductDto model) =>
            await _productService.InsertAsync(model);

        public async Task UpdateAsync(Guid id, ProductDto model) =>
            await _productService.UpdateAsync(id, Has.NotNull(model));
    }
}
