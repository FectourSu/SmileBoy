using SmileBoyClient.Command;
using SmileBoyClient.Core.IContract;
using SmileBoyClient.Core.IContract.IService;
using SmileBoyClient.Dialogs;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace SmileBoyClient.ViewModels.Abstracts
{

    /// <summary>
    /// A class that implement the logic of working with marked object in tables
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class CheckOperationViewModel<TModel> : OperationBaseViewModel<TModel>
        where TModel : class, IModel, new()
    {
        public bool IsAnyCheck => ListItems.Any(i => i.Check);

        private bool _isCheckAll;

        public bool IsCheckAll
        {
            get => _isCheckAll;
            set => Set(ref _isCheckAll, value);
        }
        public BindingList<RowCheckBoxViewModel<TModel>> ListItems { get; set; }

        public ICommand CheckAllCommand { get; }

        public ICommand DeleteManyCommand { get; }

        public CheckOperationViewModel(IOperationBase<TModel, Guid> service, IDialogService dialogService)
            : base(service, dialogService)
        {
            CheckAllCommand = new DelegateCommand(Check);
            DeleteManyCommand = new DelegateCommand(DeleteMany, _ => IsAnyCheck);

            ListItems = new BindingList<RowCheckBoxViewModel<TModel>>();
            ListItems.ListChanged += OnListChanged;
        }

        private void Check(object obj)
        {
            bool check = IsCheckAll;

            foreach (var item in ListItems)
                item.Click(check);
        }

        protected override async void ReceiveData(int page, int pageSize)
        {
            ListItems.Clear();

            var result = await CrudService.GetAll(page, pageSize, Filter);

            foreach (var item in result.Values)
            {
                ListItems.Add(new RowCheckBoxViewModel<TModel>(item));
            }

            Pagination.SetCount(result.TotalCount);
        }
        private async void DeleteMany(object obj)
        {
            var checkItems = ListItems.Where(i => i.Check);

            foreach (var item in checkItems)
                await CrudService.DeleteAsync(item.Model.Id);

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

        public override void Dispose(bool collect)
        {
            ListItems.ListChanged -= OnListChanged;
            ListItems.Clear();

            base.Dispose(collect);
        }

    }
}
