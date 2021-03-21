using SmileBoyClient.Core.IContract;
using SmileBoyClient.Core.IContract.IService;
using System;

namespace SmileBoyClient.ViewModels
{
    /// <summary>
    /// A base class that implements CRUD methods and logic for paginated output and filtering
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    public abstract class ViewModel<TModel> : ViewModelBase
        where TModel : class, IModel, new()
    {
        private readonly ICrudService<TModel, Guid> _serivce;

    }
}
