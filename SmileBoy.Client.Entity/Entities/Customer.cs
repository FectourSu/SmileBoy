using SmileBoy.Client.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmileBoy.Client.Entities.Entities
{
    public class Customer : EntityBase<Guid>, IOrdersReference
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Img { get; set; }

        public ICollection<Guid> Orders { get; set; } = new List<Guid>();
    }
}
