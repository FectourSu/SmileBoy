using SmileBoyClient.Core.Entites;
using SmileBoyClient.Core.IContract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmileBoy.Client.Core.Dto
{
    public class OrderDto : IModel
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public string Number { get; set; }

        public DateTime DeliveryDate { get; set; }

        public Guid CustomerId { get; set; }

        public CustomerDto Customer { get; set; }

        public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();

        public bool Delivered => GetProgress();

        private bool GetProgress()
        {
            DeliveryDate = DateTime.Now.AddDays(5);

            if (DateTime.Now.Day == DeliveryDate.Day)
                return true;
            else
                return false;
        }
    }
}
