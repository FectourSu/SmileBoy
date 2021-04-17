using SmileBoy.Client.Core.Dto;
using SmileBoy.Client.Core.IContract.IManagers;
using SmileBoyClient.Dialogs;
using SmileBoyClient.ViewModels.Abstracts;

namespace SmileBoyClient.ViewModels
{
    class CustomerViewModel : CheckOperationViewModel<CustomerDto>
    {
        public CustomerViewModel(ICustomerManager manager, IDialogService dialogService)
           : base(manager, dialogService)
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
