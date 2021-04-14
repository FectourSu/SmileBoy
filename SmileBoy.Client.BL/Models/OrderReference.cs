using SmileBoy.Client.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmileBoy.Client.Core.Models
{
    /// <summary>
    /// DTO for deleting group references
    /// </summary>
    public sealed class DeletedOrderReference : IParticipantsReferences
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public ICollection<Guid> ProductsIds { get; set; }

        private DeletedOrderReference()
        {
        }

        public static DeletedOrderReference GetInstance(IParticipantsReferences entity, IParticipantsReferences update)
        {
            return new DeletedOrderReference
            {
                Id = entity.Id,
                CustomerId = update.CustomerId != entity.CustomerId
                    ? entity.CustomerId
                    : default,

                ProductsIds = entity.ProductsIds
                    .Except(update.ProductsIds)
                    .ToList()
            };
        }
    }

    /// <summary>
    /// DTO for inserting group references
    /// </summary>
    public sealed class InsertOrderReferences : IParticipantsReferences
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public ICollection<Guid> ProductsIds { get; set; }

        private InsertOrderReferences()
        {
        }

        public static InsertOrderReferences GetInstance(IParticipantsReferences entity, IParticipantsReferences update)
        {
            return new InsertOrderReferences
            {
                Id = entity.Id,
                CustomerId = update.CustomerId != entity.CustomerId
                    ? update.CustomerId
                    : default,

                ProductsIds = update.ProductsIds
                    .Except(entity.ProductsIds)
                    .ToList()
            };
        }
    }
}
