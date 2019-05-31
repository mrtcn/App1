using App1.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SportSelection : ContentPage
    {
        public SportSelection()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DependencyService.Register<MockDataStore>();
            if(!Application.Current.Properties.ContainsKey("IsSportSelected"))
                Application.Current.Properties.Add("IsSportSelected", true);

            Application.Current.MainPage = new AppShell();
        }
    }
}