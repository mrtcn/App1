using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Services;
using App1.Views;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper.Configuration;
using AutoMapper;
using App1.ViewModels;
using App1.Interfaces;

namespace App1
{
    public partial class App : Application
    {
        IServiceCollection services;
        internal static IServiceProvider ServiceProvider { get; private set; }

        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "http://localhost:5000";
        public static bool UseMockDataStore = true;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<HttpHandler>();
            InitAutoMapper();

            if (services == null)
            {
                services = new ServiceCollection();
            }
            services.AddHttpClient();

            ServiceProvider = services.BuildServiceProvider();

            //if (UseMockDataStore)
            //    DependencyService.Register<MockDataStore>();
            //else
            //    DependencyService.Register<AzureDataStore>();
            //MainPage = new AppShell();
            MainPage = new NavigationPage(new Login());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void InitAutoMapper()
        {
            MapperConfigurationExpression cfg = new MapperConfigurationExpression();
            cfg.CreateMap<Models.Location, Xamarin.Essentials.Location>();
            cfg.CreateMap<Xamarin.Essentials.Location, Models.Location>();
            cfg.CreateMap<Xamarin.Essentials.Location, SpotListViewModel>();
            cfg.CreateMap<SpotListViewModel, Xamarin.Essentials.Location>();
            cfg.CreateMap<Models.Location, SpotListViewModel>();
            cfg.CreateMap<SpotListViewModel, Models.Location>();

            Mapper.Initialize(cfg);
        }
    }
}
