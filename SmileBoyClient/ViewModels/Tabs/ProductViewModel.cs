using SmileBoyClient.Command;
using SmileBoyClient.Core.Entites;
using SmileBoyClient.Core.IContract.IService;
using SmileBoyClient.Dialogs;
using SmileBoyClient.Helpers;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace SmileBoyClient.ViewModels
{
    public class ProductViewModel : ViewModel<ProductDto>
    {
        private readonly IProductService _service;
        
        public BindingList<RowCheckBoxViewModel<ProductDto>> ListItems { get; set; }

        public bool IsAnyCheck => ListItems.Any(i => i.Check);

        private bool _isCheckAll;

        public bool IsCheckAll
        {
            get => _isCheckAll;
            set => Set(ref _isCheckAll, value);
        }
        
        public ICommand CheckAllCommand { get; }
        
        public ICommand DeleteManyCommand { get; }


        public ProductViewModel(IProductService service, IDialogService dialogService)
            : base(service, dialogService)
        {
            _service = Has.NotNull(service, nameof(service));

            PageSize = 10;
            Pagination = new PaginationViewModel(ReceiveData, PageSize);

            CheckAllCommand = new DelegateCommand(Check);

            ListItems = new BindingList<RowCheckBoxViewModel<ProductDto>>();
            ListItems.ListChanged += OnListChanged;

            ReceiveData(Pagination.Index, PageSize);
        }

        private void Check(object obj)
        {
            bool check = IsCheckAll;

            foreach (var item in ListItems)
                item.Click(check);
        }

        protected override bool CanExecuteDelete(object obj) => !IsAnyCheck;

        protected override void OnDelete(object obj)
        {
            if (ListItems.Count == 1)
                Pagination.Previous();

            base.OnDelete(obj);
        }


        private async void DeleteMany(object obj)
        {
            var checkItems = ListItems.Where(i => i.Check);

            foreach (var item in checkItems)
                await _service.DeleteAsync(item.Model.Id);

            if (ListItems.Count == checkItems.Count())
                Pagination.Previous();

            ReceiveData(Pagination.Index, PageSize);
        }

        private void OnListChanged(object s, ListChangedEventArgs e)
        {

            var hasFlag = ListChangedType.ItemAdded | ListChangedType.ItemChanged;

            if (hasFlag.HasFlag(e.ListChangedType) && e.ListChangedType != ListChangedType.Reset)
                IsCheckAll = ListItems.All(i => i.Check);
        }

        protected override async void ReceiveData(int page, int pageSize)
        {
            ListItems.Clear();

            var result = await _service.GetAll(page, pageSize, Filter);

            foreach (var item in result.Values)
            {
                ListItems.Add(new RowCheckBoxViewModel<ProductDto>(item));
            }

            Pagination.SetCount(result.TotalCount);
        }

        public override void Dispose(bool collect)
        {
            ListItems.ListChanged -= OnListChanged;
            ListItems.Clear();

            base.Dispose(collect);
        }
    }
}
