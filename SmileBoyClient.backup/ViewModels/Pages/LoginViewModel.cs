using SmileBoyClient.Command;
using SmileBoyClient.Core.IContract.IProviders;
using SmileBoyClient.Helpers;
using SmileBoyClient.Navigation;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmileBoyClient.ViewModels
{
    class LoginViewModel : ViewModelBase
    {

        private readonly NavigationPageService _navigationPage;

        private readonly IAuthoriazationProvider _authoriazationProvider;

        private string _error;

        public string Error
        {
            get { return _error; }
            set { Set(ref _error, value); }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { Set(ref _email, value); }
        }

        public ICommand LoginCommand { get; }

        public ICommand CloseCommand { get; }

        public LoginViewModel(IAuthoriazationProvider authoriazationProvider, NavigationPageService navigationPage)
        {
            _authoriazationProvider = authoriazationProvider;
            _navigationPage = navigationPage;

            LoginCommand = new DelegateCommand(Login, pb =>
                !string.IsNullOrEmpty(Email) && (pb as PasswordBox)?.SecurePassword.Length > 0);

            CloseCommand = new DelegateCommand(_ => Application.Current.Shutdown());
        }

        private async void Login(object obj)
        {
            Error = string.Empty;

            var password = (obj as PasswordBox).SecurePassword;

            var state = await _authoriazationProvider.Login(Email, password);

            if (!state.IsAuthentication)
            {
                Error = state.ErrorMessage;
                return;
            }

            _navigationPage.NavigateTo(PageHelper.MainPage);
        }
    }
}
