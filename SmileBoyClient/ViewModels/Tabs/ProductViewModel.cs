using SmileBoyClient.Command;
using SmileBoyClient.Core.Entites;
using SmileBoyClient.Core.IContract.IService;
using SmileBoyClient.Dialogs;
using SmileBoyClient.Helpers;
using SmileBoyClient.ViewModels.Abstracts;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace SmileBoyClient.ViewModels
{
    public class ProductViewModel : CheckOperationViewModel<ProductDto>
    {
        public ProductViewModel(IProductService service, IDialogService dialogService)
            : base(service, dialogService)
        {
            PageSize = 6;
            Pagination = new PaginationViewModel(ReceiveData, PageSize);

            ReceiveData(Pagination.Index, PageSize);
        }

        protected override bool CanExecuteDelete(object obj) => !IsAnyCheck;

        protected override void OnDelete(object obj)
        {
            if (ListItems.Count == 1)
                Pagination.Previous();

            base.OnDelete(obj);
        }
    }
}
