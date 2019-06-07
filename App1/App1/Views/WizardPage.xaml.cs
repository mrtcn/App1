﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WizardPage : CarouselPage
    {
        public WizardPage()
        {
            InitializeComponent();
        }

        private void Close_Wizard(object sender, EventArgs e)
        {
            Application.Current.MainPage = new AppShell();
        }
    }
}