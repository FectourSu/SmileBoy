using SmileBoyClient.Command;
using SmileBoyClient.Core.IContract.IProviders;
using SmileBoyClient.Navigation;
using SmileBoyClient.Views.Tabs;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmileBoyClient.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private readonly NavigationPageService _navigationPage;

        private readonly IDictionary<string, Page> _pages;
        private readonly IAuthoriazationProvider _authorizationProvider;
        private FrameworkElement _selectedElement;

        public FrameworkElement SelectedElement
        {
            set
            {
                if (_pages.TryGetValue(value.Name, out var page) &&
                    Set(ref _selectedElement, value))
                {
                    CurrentPage = page;
                }
            }
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                Set(ref _userName, value);
                Icon = _userName.ToUpper().First();
            }
        }

        private char _icon;

        public char Icon
        {
            get { return _icon; }
            set { Set(ref _icon, value); }
        }


        Page _currentPage;

        public Page CurrentPage
        {
            get { return _currentPage; }
            set { Set(ref _currentPage, value); }
        }

        public ICommand LogoutCommand { get; }

        public ICommand CloseCommand { get; }

        public MainViewModel(IAuthoriazationProvider authorizationProvider, NavigationPageService pageService)
        {
            _authorizationProvider = authorizationProvider;
            _pages = ViewPageLocator.Tabs;

            UserName = _authorizationProvider.AuthenticationState.GetClaim("sub");

            _navigationPage = pageService;
            CurrentPage = _pages[nameof(HomeTab)];

            LogoutCommand = new DelegateCommand(Logout);
            CloseCommand = new DelegateCommand(_ => Application.Current.Shutdown());
        }

        private void Logout(object obj)
        {
            _authorizationProvider.Logout();
            _navigationPage.NavigateTo(ViewPageLocator.LoginPage);
        }
    }
}
