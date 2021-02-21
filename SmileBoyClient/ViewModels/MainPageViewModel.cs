using SmileBoyClient.Command;
using SmileBoyClient.Helpers;
using SmileBoyClient.Navigation;
using SmileBoyClient.Views.Tabs;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmileBoyClient.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        private readonly NavigationPageService _navigationPage;

        private readonly IDictionary<string, Page> _pages;

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

        public MainPageViewModel(NavigationPageService pageService)
        {
            _pages = PageHelper.Tabs;

            _navigationPage = pageService;
            CurrentPage = _pages[nameof(HomeTab)];

            LogoutCommand = new DelegateCommand(Logout);
            CloseCommand = new DelegateCommand(_ => Application.Current.Shutdown());
        }

        private void Logout(object obj)
        {
            _navigationPage.NavigateTo(PageHelper.LoginPage);
        }
    }
}
