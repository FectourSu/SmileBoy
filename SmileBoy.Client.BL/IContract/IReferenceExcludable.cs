using SmileBoy.Client.Entities;
using SmileBoy.Client.Entities.Entities;
using System;
using System.Threading.Tasks;

namespace SmileBoy.Client.Core.IContract
{
    public interface IReferenceExcludable
    {
        Task Exclude(Action<IReferenceDeletable> referencesAction, IOrdersReference model);
    }
}
