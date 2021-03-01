using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmileBoyClient.ViewModels
{
    class ViewModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (field == null && value == null)
                return false;

            if (field?.Equals(value) ?? false)
                return false;

            field = value;

            OnPropertyChanged(propertyName);

            return true;

        }
    }
}
