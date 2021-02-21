using Microsoft.Extensions.DependencyInjection;
using SmileBoyClient.Navigation;
using SmileBoyClient.ViewModels;
using System;
using System.Net.Http;

namespace SmileBoyClient
{
    class ViewModelLocator
    {
        public static IServiceProvider _provider;

        public static void Initialize()
        {
            var services = new ServiceCollection();

            ConfigureService(services);

            _provider = services.BuildServiceProvider();
            
        }

        public static void ConfigureService(IServiceCollection services)
        {
            services.AddTransient<MainPageViewModel>();
            services.AddScoped<LoginPageViewModel>();

            services.AddTransient<HomePageViewModel>();
            services.AddTransient<OptionPageViewModel>();
            services.AddTransient<ProductViewModel>();
            services.AddTransient<OrderPageViewModel>();
            services.AddTransient<OrderProductPageViewModel>();
            services.AddTransient<CustomerPageViewModel>();

            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<NavigationPageService>();
        }

        public MainWindowViewModel MainWindow =>
            _provider.GetRequiredService<MainWindowViewModel>();

        public MainPageViewModel MainPage =>
            _provider.GetRequiredService<MainPageViewModel>();

        public LoginPageViewModel LoginPage =>
            _provider.GetRequiredService<LoginPageViewModel>();

        public ProductViewModel ProductTab =>
            _provider.GetRequiredService<ProductViewModel>();

        public OrderPageViewModel OrderTab =>
            _provider.GetRequiredService<OrderPageViewModel>();

        public OrderProductPageViewModel OrderProductTab =>
            _provider.GetRequiredService<OrderProductPageViewModel>();

        public CustomerPageViewModel CustomerTab =>
            _provider.GetRequiredService<CustomerPageViewModel>();

        public OptionPageViewModel SettingTab =>
            _provider.GetRequiredService<OptionPageViewModel>();

        public HomePageViewModel HomeTab =>
            _provider.GetRequiredService<HomePageViewModel>();


    }
}