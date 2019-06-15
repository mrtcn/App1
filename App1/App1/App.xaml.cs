using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Services;
using App1.Views;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

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

            if (services == null)
            {
                services = new ServiceCollection();
            }
            services.AddHttpClient();


            ServiceProvider = services.BuildServiceProvider();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();
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
    }
}
