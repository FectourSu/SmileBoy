using MaterialDesignThemes.Wpf;
using SmileBoyClient.Helpers;
using SmileBoyClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileBoyClient.Dialogs
{
    public class DialogService : IDialogService
    {
        public async Task<object> ShowAsync(ViewModelBase viewModel)
        {
            Has.NotNull(viewModel);

            var dialogView = ViewDialogLocator.View(viewModel);
            dialogView.DataContext = viewModel;

            return await DialogHost.Show(dialogView, "RootDialog");
        }
    }
}
