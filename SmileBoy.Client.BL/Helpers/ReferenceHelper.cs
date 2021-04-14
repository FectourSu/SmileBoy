using SmileBoy.Client.Core.IContract.IData;
using SmileBoy.Client.Core.Models;
using SmileBoy.Client.Entities;
using SmileBoy.Client.Entities.Entities;
using System;
using System.Threading.Tasks;

namespace SmileBoy.Client.Core.Helpers
{
    public static class ReferenceHelper
    {
        /// <summary>
        /// sets a reference to the group for all the specified object
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task SetAllReferences(this IUnitOfWork unitOfWork, IParticipantsReferences model, Action<IOrdersReference, Guid> action)
        {
            if (model.CustomerId != default)
            {
                var customer = await unitOfWork.Customers.GetByIdAsync(model.CustomerId);

                action?.Invoke((IOrdersReference)customer, model.Id);

                await unitOfWork.Customers.UpdateAsync(customer.Id, customer);
            }

            await model.ProductsIds?.SelectAsync(async id =>
            {
                var products = await unitOfWork.Products.GetByIdAsync(id);

                action?.Invoke((IOrdersReference)products, model.Id);

                await unitOfWork.Products.UpdateAsync(products.Id, products);
                return id;
            });
        }

        public static async Task SetAllReferences(this IUnitOfWork unitOfWork, DeletedOrderReference model)
            => await ReferenceHelper.SetAllReferences(unitOfWork, model, (@ref, id) => @ref.Orders.Remove(id));

        public static async Task SetAllReferences(this IUnitOfWork unitOfWork, InsertOrderReferences model)
            => await ReferenceHelper.SetAllReferences(unitOfWork, model, (@ref, id) => @ref.Orders.Add(id));
    }
}
