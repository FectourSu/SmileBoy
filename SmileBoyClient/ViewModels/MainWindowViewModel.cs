using SmileBoyClient.Core.IContract.IProviders;
using SmileBoyClient.Navigation;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmileBoyClient.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private Page _currentPage;

        private readonly IAuthoriazationProvider _authorizationProvider;

        private readonly NavigationPageService _pageService;

        public ICommand LogoutCommand { get; }

        public Page CurrentPage
        {
            get { return _currentPage; }
            set { Set(ref _currentPage, value); }
        }

        public MainWindowViewModel(IAuthoriazationProvider authoriazationProvider, NavigationPageService pageService)
        {

            _authorizationProvider = authoriazationProvider;

            _pageService = pageService;

            _pageService.OnPageChanged += (page) => CurrentPage = page;

            //progress session logic, but this code need refactoring
            var state = _authorizationProvider.AuthenticationState;
            _authorizationProvider.ExtendSession();

            if (state.IsAuthentication)
                _pageService.NavigateTo(ViewPageLocator.MainPage);
            else
                pageService.NavigateTo(ViewPageLocator.LoginPage);
        }
    }
}
