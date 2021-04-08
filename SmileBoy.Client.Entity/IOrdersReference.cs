using System;
using System.Collections.Generic;

namespace SmileBoy.Client.Entities
{
    public interface IOrdersReference
    {
        public ICollection<Guid> Orders { get; set; }
    }
}
