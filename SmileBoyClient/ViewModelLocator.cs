using Microsoft.Extensions.DependencyInjection;
using SmileBoy.Client.BL.Services;
using SmileBoy.Client.Core.Providers;
using SmileBoy.Client.Core.Services;
using SmileBoyClient.Core;
using SmileBoyClient.Core.IContract;
using SmileBoyClient.Core.IContract.IProviders;
using SmileBoyClient.Core.IContract.IService;
using SmileBoyClient.Core.Models;
using SmileBoyClient.Extentions;
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
            ServiceCollection services = new();

            ConfigureService(services);

            _provider = services.BuildServiceProvider();

        }

        public static void ConfigureService(IServiceCollection services)
        {
            //Pages
            services.AddTransient<MainViewModel>();
            services.AddScoped<LoginViewModel>();

            //Tabs
            services.AddTransient<HomeViewModel>();
            services.AddTransient<OptionViewModel>();
            services.AddTransient<ProductViewModel>();
            services.AddTransient<OrderViewModel>();
            services.AddTransient<CustomerViewModel>();

            //Main window
            services.AddSingleton<MainWindowViewModel>();

            //Authorization
            services.AddScoped(typeof(IAuthorizationService<>), typeof(AuthorizationService<>));
            services.AddScoped<ISessionService<UserSession>, UserSessionService>();
            services.AddScoped<IAuthoriazationProvider, AuthorizationProvider>();

            //Infrastructure
            services.AddAutoMapper();
            services.AddSingleton<NavigationPageService>();
            services.AddScoped<HttpClient>();
            services.AddSingleton<ITokenStorage, InMemoryTokenStorage>()
                .AddTransient<IReaderTokenStorage>(p => p.GetService<ITokenStorage>());

            //Services
            services.AddScoped<IProductService, ProductService>();

            //Data
            services.AddMongoProvider();
        }

        public MainWindowViewModel MainWindow =>
            _provider.GetRequiredService<MainWindowViewModel>();

        public MainViewModel MainPage =>
            _provider.GetRequiredService<MainViewModel>();

        public LoginViewModel LoginPage =>
            _provider.GetRequiredService<LoginViewModel>();

        public ProductViewModel ProductTab =>
            _provider.GetRequiredService<ProductViewModel>();

        public OrderViewModel OrderTab =>
            _provider.GetRequiredService<OrderViewModel>();
        public CustomerViewModel CustomerTab =>
            _provider.GetRequiredService<CustomerViewModel>();

        public OptionViewModel SettingTab =>
            _provider.GetRequiredService<OptionViewModel>();

        public HomeViewModel HomeTab =>
            _provider.GetRequiredService<HomeViewModel>();


    }
}