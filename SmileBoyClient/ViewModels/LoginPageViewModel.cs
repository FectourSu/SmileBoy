using SmileBoyClient.Command;
using SmileBoyClient.Helpers;
using SmileBoyClient.Navigation;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmileBoyClient.ViewModels
{
    class LoginPageViewModel : ViewModelBase
    {
        private string _error;

        private string _email;

        private readonly NavigationPageService _navigationPage;

        public string Error
        {
            get { return _error; }
            set { Set(ref _error, value); }
        }

        public string Email
        {
            get { return _email; }
            set { Set(ref _email, value); }
        }

        public ICommand LoginCommand { get; }

        public ICommand CloseCommand { get; }

        public LoginPageViewModel(NavigationPageService navigationPage)
        {
            _navigationPage = navigationPage;

            LoginCommand = new DelegateCommand(Login, pb =>
                !string.IsNullOrEmpty(Email) && (pb as PasswordBox)?.SecurePassword.Length > 0);

            CloseCommand = new DelegateCommand(_ => Application.Current.Shutdown());
        }

        private void Login(object obj)
        {
            var password = (obj as PasswordBox).SecurePassword;

            var credential = new NetworkCredential(Email, password);

            var state = "1488";

            if (credential.Password != state)
            {
                Error = "Incorrect password";
                return;
            }

            _navigationPage.NavigateTo(PageHelper.MainPage);

        }

    }
}
