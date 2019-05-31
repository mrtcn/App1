using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties.TryGetValue("IsSportSelected", out object isSportSelectedObject);

            var isSelectedText = isSportSelectedObject == null ? string.Empty : isSportSelectedObject.ToString();

            if (bool.TryParse(isSelectedText, out bool isSportSelected))
                isSportSelected = false;

            if (isSportSelected)
            {
                Application.Current.MainPage = new WizardPage();
                //Application.Current.MainPage = new AppShell();
            }
            else
            {
                Application.Current.MainPage = new WizardPage();
            }
        }
    }
}