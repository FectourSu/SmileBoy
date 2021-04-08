using SmileBoy.Client.Entity;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SmileBoy.Client.Entities.Entities
{
    public class Orders : EntityBase<Guid>, IParticipantsReferences, IReferenceDeletable
    {
        public string Number { get; set; }

        public decimal Amount { get; set; }

        public DateTime DeliveryDate  { get; set; }

        public Guid CustomerId { get; set; }

        public ICollection<Guid> ProductsIds { get; set; } = new List<Guid>();

        public void DeleteReferencesProduct(Guid id) => ProductsIds.Remove(id);

        public void DeleteReferencesCustomer() => CustomerId = Guid.Empty;
    }
}
