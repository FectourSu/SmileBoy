using SmileBoyClient.Command;
using SmileBoyClient.Core.IContract.IProviders;
using SmileBoyClient.Helpers;
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

            LogoutCommand = new DelegateCommand(SessionLogin);

            pageService.NavigateTo(PageHelper.LoginPage);
        }

        private async void SessionLogin(object obj)
        {
            var state = _authorizationProvider.AuthenticationState;

            if (state.IsAuthentication)
            {
                await _authorizationProvider.ExtendSession();

                _pageService.NavigateTo(PageHelper.MainPage);
                return;
            }
        }
    }
}
