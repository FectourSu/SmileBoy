using SmileBoy.Client.Core.Dto;
using SmileBoyClient.Core.Entites;
using SmileBoyClient.ViewModels;
using SmileBoyClient.ViewModels.Dialogs;
using SmileBoyClient.Views.Dialogs;
using System;
using System.Windows.Controls;

namespace SmileBoyClient
{
    public static class ViewDialogLocator
    {
        public static ContentControl View(ViewModelBase viewModel) =>
            viewModel switch
            {
                AddDialogViewModel<ProductDto> => new AddDialog(),
                UpdateDialogViewModel<ProductDto> => new UpdateDialog(),

                AddDialogViewModel<CustomerDto> => new AddDialog(),
                UpdateDialogViewModel<CustomerDto> => new UpdateDialog(),

                AddDialogViewModel<OrderDto> => new AddDialog(),
                OrdersDialogViewModel => new OrdersDialog(),

                _ => throw new NotImplementedException(),
            };
    }
}
