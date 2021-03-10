using SmileBoyClient.Command;
using SmileBoyClient.Core.IContract.IProviders;
using SmileBoyClient.Helpers;
using SmileBoyClient.Navigation;
using SmileBoyClient.Views.Tabs;
using System.Collections.Generic;
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

        Page _currentPage;

        public Page CurrentPage
        {
            get { return _currentPage; }
            set { Set(ref _currentPage, value); }
        }

        public ICommand LogoutCommand { get; }

        public ICommand CloseCommand { get; }

        public MainViewModel(IAuthoriazationProvider authorizationProvider,NavigationPageService pageService)
        {
            _authorizationProvider = authorizationProvider;
            _pages = PageHelper.Tabs;

            _navigationPage = pageService;
            CurrentPage = _pages[nameof(HomeTab)];

            LogoutCommand = new DelegateCommand(Logout);
            CloseCommand = new DelegateCommand(_ => Application.Current.Shutdown());
        }

        private void Logout(object obj)
        {
            _authorizationProvider.Logout();
            _navigationPage.NavigateTo(PageHelper.LoginPage);
        }
    }
}
