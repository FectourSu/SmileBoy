using Microsoft.Extensions.DependencyInjection;
using SmileBoy.Client.BL.Services;
using SmileBoy.Client.Core.IContract;
using SmileBoy.Client.Core.IContract.IManagers;
using SmileBoy.Client.Core.IContract.IProviders;
using SmileBoy.Client.Core.IContract.IService;
using SmileBoy.Client.Core.Managers;
using SmileBoy.Client.Core.Providers;
using SmileBoy.Client.Core.Services;
using SmileBoyClient.Core;
using SmileBoyClient.Core.IContract;
using SmileBoyClient.Core.IContract.IProviders;
using SmileBoyClient.Core.IContract.IService;
using SmileBoyClient.Core.Models;
using SmileBoyClient.Dialogs;
using SmileBoyClient.Extentions;
using SmileBoyClient.Navigation;
using SmileBoyClient.ViewModels;
using System;
using System.Net.Http;

namespace SmileBoyClient
{
    class ViewModelLocator
    {
        public static IServiceProvider Provider { get; private set; }

        public static void Initialize()
        {
            ServiceCollection services = new();

            ConfigureService(services);

            Provider = services.BuildServiceProvider();

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
            services.AddScoped<HttpClient>();
            services.AddSingleton<NavigationPageService>();
            services.AddSingleton<ITokenStorage, InMemoryTokenStorage>()
                .AddTransient<IReaderTokenStorage>(p => p.GetService<ITokenStorage>());

            //Services
            services.AddScoped<IDialogService, DialogService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductService, ProductService>();

            //Providers
            services.AddScoped<IOrderProvider, OrderProvider>()
                .AddTransient<IReferenceExcludable>(p => p.GetService<IOrderProvider>() as OrderProvider);

            //Managers
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<ICustomerManager, CustomerManager>();

            //Data
            services.AddMongoProvider();
        }

        public MainWindowViewModel MainWindow =>
            Provider.GetRequiredService<MainWindowViewModel>();

        public MainViewModel MainPage =>
            Provider.GetRequiredService<MainViewModel>();

        public LoginViewModel LoginPage =>
            Provider.GetRequiredService<LoginViewModel>();

        public ProductViewModel ProductTab =>
            Provider.GetRequiredService<ProductViewModel>();

        public OrderViewModel OrderTab =>
            Provider.GetRequiredService<OrderViewModel>();
        public CustomerViewModel CustomerTab =>
            Provider.GetRequiredService<CustomerViewModel>();

        public OptionViewModel SettingTab =>
            Provider.GetRequiredService<OptionViewModel>();

        public HomeViewModel HomeTab =>
            Provider.GetRequiredService<HomeViewModel>();


    }
}