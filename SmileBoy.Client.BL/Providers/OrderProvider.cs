using AutoMapper;
using SmileBoy.Client.Core.Dto;
using SmileBoy.Client.Core.Helpers;
using SmileBoy.Client.Core.IContract;
using SmileBoy.Client.Core.IContract.IData;
using SmileBoy.Client.Core.IContract.IProviders;
using SmileBoy.Client.Core.IContract.IService;
using SmileBoy.Client.Core.Models;
using SmileBoy.Client.Entities;
using SmileBoy.Client.Entities.Entities;
using SmileBoyClient.Core.IContract.IService;
using SmileBoyClient.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmileBoy.Client.Core.Providers
{
    public class OrderProvider : IOrderProvider, IReferenceExcludable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productSerivice;

        private readonly IMapper _mapper;

        public OrderProvider(IUnitOfWork unitOfWork, ICustomerService customerService, IProductService productService, IMapper mapper)
        {
            _unitOfWork = Has.NotNull(unitOfWork);
            _customerService = Has.NotNull(customerService);
            _productSerivice = Has.NotNull(productService);

            _mapper = Has.NotNull(mapper);
        }

        public async Task<PaginationResult<OrderDto>> GetAllAsync(int page, int pageSize, string filter = null)
        {
            var entites = string.IsNullOrEmpty(filter) ? _unitOfWork.Orders.GetAll() : await _unitOfWork.Orders.SearchAsync(filter);

            return new PaginationResult<OrderDto>
            {
                TotalCount = entites.Count(),
                Values = await entites.Pagination(page, pageSize).SelectAsync(async (orders) => await MapToModel(orders))
            };
        }

        private async Task<OrderDto> MapToModel(Order entity)
        {
            var model = _mapper.Map<OrderDto>(Has.NotNull(entity));

            if (entity.CustomerId != default)
            {
                var customer = await _customerService.GetByIdAsync(entity.CustomerId);
                model.Customer = customer; 
            }

            entity.ProductsIds?.SelectAsync(async id =>
            {
                Has.NotNull(model.Products).Add(await _productSerivice.GetByIdAsync(id));
                return id;
            });

            return model;
        }

        public async Task<OrderDto> GetByIdAsync(Guid id) => await MapToModel(await _unitOfWork.Orders.GetByIdAsync(id));

        public async Task InsertAsync(OrderDto model)
        {
            var entity = _mapper.Map<Order>(Has.NotNull(model));

            await _unitOfWork.Orders.InsertAsync(entity);
        }

        public async Task UpdateAsync(Guid id, OrderUpdate model)
        {
            Has.NotNull(model);

            if (!id.Equals(model.Id))
                throw new InvalidOperationException($"This IDs don't match: {id} and {model.Id}");

            var entity = await _unitOfWork.Orders.GetByIdAsync(model.Id);

            await _unitOfWork.SetAllReferences(DeletedOrderReference.GetInstance(entity, model));


            //Adding new links received from the UI
            await _unitOfWork.SetAllReferences(InsertOrderReferences.GetInstance(entity, model));
            await _unitOfWork.Orders.UpdateAsync(id, _mapper.Map(model, entity));
        }
        public async Task DeleteAsync(Guid id)
        {
            var entity = await _unitOfWork.Orders.GetByIdAsync(id);

            if (entity == null)
                throw new InvalidOperationException($"Missing id specified: {id}");

            await _unitOfWork.SetAllReferences(entity, (@ref, id) => @ref.Orders.Remove(id));
            await _unitOfWork.Orders.DeleteAsync(id);
        }

        /// <summary>
        /// Deleted installed links in Orders
        /// </summary>
        /// <param name="referencesAction"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task Exclude(Action<IReferenceDeletable> referencesAction, IOrdersReference model)
        {
            Has.NotNull(model);

            foreach (var id in model.Orders)
            {
                var orderEntity = await _unitOfWork.Orders.GetByIdAsync(id);

                if (orderEntity == null)
                    throw new InvalidOperationException($"Missing id specified: {id}");

                referencesAction?.Invoke(orderEntity);
                await _unitOfWork.Orders.UpdateAsync(id, orderEntity);
            }
        }
    }
}
