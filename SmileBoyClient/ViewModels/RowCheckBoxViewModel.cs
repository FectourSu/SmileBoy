using SmileBoyClient.Command;
using SmileBoyClient.Core.IContract;
using System.Windows.Input;

namespace SmileBoyClient.ViewModels
{
    public interface IRowViewModel<TModel>
        where TModel : IModel
    {
        TModel Model { get; set; }
    }

    public class RowCheckBoxViewModel<TModel> : ViewModelBase, IRowViewModel<TModel>
        where TModel : IModel
    {
        private bool _check;

        public bool Check
        {
            get => _check;
            set => Set(ref _check, value);
        }

        public TModel Model { get; set; }

        public ICommand ClickCommand { get; }

        public RowCheckBoxViewModel(TModel model = default)
        {
            Model = model;

            ClickCommand = new DelegateCommand(_ => Click(!Check));
        }

        public void Click(bool v) => Check = v;
    }
}
