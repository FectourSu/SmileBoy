using System;
using System.Collections.Generic;

namespace SmileBoy.Client.Entities.Entities
{
    public interface IParticipantsReferences
    {
        /// <summary>
        /// Orders Id
        /// </summary>
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public ICollection<Guid> ProductsIds { get; set; }
    }
}