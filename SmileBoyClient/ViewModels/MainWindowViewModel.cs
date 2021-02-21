using SmileBoyClient.Helpers;
using SmileBoyClient.Navigation;
using System.Windows.Controls;

namespace SmileBoyClient.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private Page _currentPage;

        public Page CurrentPage
        {
            get { return _currentPage; }
            set { Set(ref _currentPage, value); }
        }

        public MainWindowViewModel(NavigationPageService pageService)
        {
            pageService.OnPageChanged += (page) => CurrentPage = page;

            pageService.NavigateTo(PageHelper.LoginPage);
        }
    }
}
