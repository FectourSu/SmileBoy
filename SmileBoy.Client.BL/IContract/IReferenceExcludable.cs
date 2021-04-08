using SmileBoy.Client.Entities;
using System;
using System.Threading.Tasks;

namespace SmileBoy.Client.Core.IContract
{
    public interface IReferenceExcludable
    {
        Task Exclude(Action<IReferenceExcludable> referencesAction, IOrdersReference model);
    }
}
