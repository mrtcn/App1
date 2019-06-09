using Xamarin.Forms;

using App1.ViewModels;
using App1.Models;

namespace App1.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var player = args.SelectedItem as Player;
            if (player == null)
                return;

            await Navigation.PushAsync(new PlayerDetailPage(new PlayerDetailViewModel(player)));

            // Manually deselect item.
            PlayerListView.SelectedItem = null;
        }
    }
}