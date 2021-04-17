using SmileBoy.Client.Core.IContract;
using SmileBoyClient.Core.Entites;
using SmileBoyClient.Dialogs;
using SmileBoyClient.ViewModels.Abstracts;

namespace SmileBoyClient.ViewModels
{
    public class ProductViewModel : CheckOperationViewModel<ProductDto>
    {

        public ProductViewModel(IProductManager manager, IDialogService dialogService)
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
