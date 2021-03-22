using SmileBoyClient.ViewModels;
using System.Threading.Tasks;

namespace SmileBoyClient.Dialogs
{
    public interface IDialogService
    {
        public Task<object> ShowAsync(ViewModelBase viewModel);
    }
}
