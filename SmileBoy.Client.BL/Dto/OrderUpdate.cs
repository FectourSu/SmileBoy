using SmileBoy.Client.Entities.Entities;
using SmileBoyClient.Core.IContract;
using System;
using System.Collections.Generic;

namespace SmileBoy.Client.Core.Dto
{
    public class OrderUpdate : IModel, IParticipantsReferences
    {
        public Guid Id { get; set; }

        public string Number { get; set; }

        public decimal Amount { get; set; }

        public DateTime DeliveryDate { get; set; }


        public Guid CustomerId { get; set; }

        public ICollection<Guid> ProductsIds { get; set; }
    }
}
