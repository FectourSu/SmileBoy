using SmileBoy.Client.Entity;
using System;
using System.Collections.Generic;

namespace SmileBoy.Client.Entities
{
    public class Product : EntityBase<Guid>, IOrdersReference
    {
        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public decimal CurrentPrice { get; set; }

        public string Img { get; set; }

        public int ProductCode { get; set; }

        public ICollection<Guid> Orders { get; set; } = new List<Guid>();
    }
}
