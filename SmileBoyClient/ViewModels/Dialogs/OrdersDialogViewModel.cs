using MaterialDesignThemes.Wpf;
using SmileBoy.Client.Core.Dto;
using SmileBoyClient.Command;
using SmileBoyClient.Core.Entites;
using SmileBoyClient.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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
            //trash code = bug fixed
            if(Model.checker)
                Model.DeliveryDate = Model.DeliveryDate.AddDays(1);

            var selectCustomers = Customers.Single(c => c.Check).Model;
            Model.Customer = selectCustomers;

            var selectProducts = Products.Where(p => p.Check).Select(p => p.Model);
            Model.Products = selectProducts.ToList();

            //warning - trash code
            if (Model.Products.Count() == 0)
                Model.Amount = 0;

            foreach (var item in Model.Products)
                Model.Amount = Products.Where(s => s.Check).Any()
                    ? Model.Amount + item.CurrentPrice
                    : Model.Amount - item.CurrentPrice;
            

            DialogHost.CloseDialogCommand.Execute(Model, null);
        }
    }
}
