using Xamarin.Forms;

using App1.ViewModels;

namespace App1.Views
{
    public partial class PlayerDetailPage : ContentPage
    {
        PlayerDetailViewModel viewModel;

        public PlayerDetailPage(PlayerDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }
    }
}