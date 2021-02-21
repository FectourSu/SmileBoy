using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SmileBoyClient.Navigation
{
    class NavigationPageService
    {
        private Page _oldPage;

        public event Action<Page> OnPageChanged;

        public void NavigateTo(Page page)
        {
            if (_oldPage != null)
            {
                var navService = NavigationService.GetNavigationService(_oldPage);

                while (navService.CanGoBack)
                {
                    navService.RemoveBackEntry();
                }
            }

            OnPageChanged?.Invoke(page);
            _oldPage = page;
        }
    }
}
