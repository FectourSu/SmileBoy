using AutoMapper;
using SmileBoy.Client.Core.Dto;
using SmileBoy.Client.Core.IContract;
using SmileBoy.Client.Core.IContract.IManagers;
using SmileBoy.Client.Core.IContract.IProviders;
using SmileBoy.Client.Entities.Entities;
using SmileBoyClient.Command;
using SmileBoyClient.Dialogs;
using SmileBoyClient.ViewModels.Dialogs;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace SmileBoyClient.ViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        private const int PageSize = 4;
        
        private readonly IProductManager _productManager;
        private readonly ICustomerManager _customerManager;
        private readonly IOrderProvider _orderProvider;

        private readonly IDialogService _dialogService;
        private readonly IMapper _mapper;

        public PaginationViewModel Pagination { get; set; }

        public BindingList<OrderDto> Items { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DeleteManyCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        private string _filter;

        public string Filter
        {
            get => _filter;
            set => Set(ref _filter, value);
        }

        public OrderViewModel(IOrderProvider orderProvider, ICustomerManager customerManager,
            IProductManager productManager, IDialogService dialogService, IMapper mapper)
        {
            _customerManager = customerManager;
            _productManager = productManager;
            _orderProvider = orderProvider;

            _dialogService = dialogService;
            _mapper = mapper;

            Pagination = new PaginationViewModel(ReceiveData, PageSize);

            Items = new BindingList<OrderDto>();

            AddCommand = new DelegateCommand(OnAdd);
            UpdateCommand = new DelegateCommand(OnUpdate);
            DeleteCommand = new DelegateCommand(OnDelete);
            DeleteManyCommand = new DelegateCommand(OnDeleteMany);

            RefreshCommand = new DelegateCommand(_ =>
            {
                ReceiveData(Pagination.Index, PageSize);
            });

            SearchCommand = new DelegateCommand(_ =>
            {
                Pagination.Reset();
                ReceiveData(Pagination.Index, PageSize);
            });

            ReceiveData(Pagination.Index, PageSize);
        }

        private async void OnDeleteMany(object obj)
        {

            foreach (var item in Items)
                await _orderProvider.DeleteAsync(item.Id);

            if (Items.Count is not 0)
                Pagination.Previous();

            ReceiveData(Pagination.Index, PageSize);
        }

        private async void OnDelete(object obj)
        {
            if (Items.Count == 1)
                Pagination.Previous();

            await _orderProvider.DeleteAsync(Guid.Parse(obj.ToString()));
            ReceiveData(Pagination.Index, PageSize);
        }

        private async void OnUpdate(object obj)
        {
            var model = obj as OrderDto;

            var result = await _dialogService.ShowAsync(new OrdersDialogViewModel(
                orders: model,
                customers: (await _customerManager.GetAll(1, 100)).Values,
                products: (await _productManager.GetAll(1, 100)).Values));

            if (result is OrderDto orders)
            {
                var update = _mapper.Map<OrderUpdate>(orders);
                await _orderProvider.UpdateAsync(model.Id, update);
                ReceiveData(Pagination.Index, PageSize);
            }
        }

        private async void OnAdd(object obj)
        {
            await _orderProvider.InsertAsync(new OrderDto());
            ReceiveData(Pagination.Index, PageSize);
        }

        private async void ReceiveData(int index, int size)
        {
            Items.Clear();

            var orders = await _orderProvider.GetAllAsync(index, size, Filter);

            foreach (var item in orders.Values)
                Items.Add(item);
            
            Pagination.SetCount(orders.TotalCount);
        }
    }
}
