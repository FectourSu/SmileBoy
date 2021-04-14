using MaterialDesignThemes.Wpf;
using SmileBoy.Client.Core.Dto;
using SmileBoyClient.Command;
using SmileBoyClient.Core.Entites;
using SmileBoyClient.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmileBoyClient.ViewModels.Dialogs
{
    class OrdersDialogViewModel : ViewModelBase
    {
        public ICommand UpdateCommand { get; set; }

        public OrderDto Model { get; }


        public ObservableCollection<RowCheckBoxViewModel<CustomerDto>> Customers { get; set; }
        public ObservableCollection<RowCheckBoxViewModel<ProductDto>> Products { get; set; }

        public OrdersDialogViewModel(OrderDto orders,
            IEnumerable<CustomerDto> customers, IEnumerable<ProductDto> products)
        {
            
            Customers = new ObservableCollection<RowCheckBoxViewModel<CustomerDto>>();

            foreach (var item in customers)
            {
                Customers.Add(new RowCheckBoxViewModel<CustomerDto>(item)
                {
                    Check = orders.Customer?.Id.Equals(item.Id) ?? false
                });
            }

            Products = new ObservableCollection<RowCheckBoxViewModel<ProductDto>>();

            foreach (var item in products)
            {
                Products.Add(new RowCheckBoxViewModel<ProductDto>(item)
                {
                    Check = orders.Products?.Select(p => p.Id).Contains(item.Id) ?? false
                });
            }

            Model = orders;
            UpdateCommand = new DelegateCommand(OnUpdate, CanUpdate);
        }

        private bool CanUpdate(object arg) => Customers.Where(c => c.Check).Count() == 1;

        private void OnUpdate(object obj)
        {
            var selectCustomers = Customers.Single(c => c.Check).Model;
            Model.Customer = selectCustomers;

            var selectProducts = Products.Where(p => p.Check).Select(p => p.Model);
            Model.Products = selectProducts.ToList();

            DialogHost.CloseDialogCommand.Execute(Model, null);
        }
    }
}
