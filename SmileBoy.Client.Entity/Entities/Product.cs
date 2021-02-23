using SmileBoy.Client.Entity;
using System;

namespace SmileBoy.Client.Entities
{
    public class Product : EntityBase<Guid>
    {
        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public decimal CurrentPrice { get; set; }

        public string Img { get; set; }
    }
}
