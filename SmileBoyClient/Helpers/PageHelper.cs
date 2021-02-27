using SmileBoyClient.Views.Pages;
using SmileBoyClient.Views.Tabs;
using System.Collections.Generic;
using System.Windows.Controls;

namespace SmileBoyClient.Helpers
{
    class PageHelper
    {
        public static Page MainPage => new MainPage();
        public static Page LoginPage => new LoginPage();

        public static IDictionary<string, Page> Tabs => new Dictionary<string, Page>()
        {
            [nameof(HomeTab)] = new HomeTab(),
            [nameof(SettingTab)] = new SettingTab(),
            [nameof(ProductTab)] = new ProductTab(),
            [nameof(OrderTab)] = new OrderTab(),
            [nameof(CustomerTab)] = new CustomerTab()
        };
    }
}
