using System;

namespace SmileBoy.Client.Entities.Entities
{
    public interface IReferenceDeletable
    {
        public void DeleteReferencesProduct(Guid id);

        public void DeleteReferencesCustomer();
    }
}