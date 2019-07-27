using Xamarin.Forms;

using App1.ViewModels;
using App1.Models;
using System.Linq;

namespace App1.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
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

        protected override void OnAppearing()
        {
            if (viewModel.Players == null || !viewModel.Players.Any())
                viewModel.LoadItemsCommand.Execute(null);

            base.OnAppearing();
        }

        private async void OnFollowClicked(object sender, System.EventArgs e)
        {
            var followSpotViewModel = new FollowSpotViewModel() { SpotId = viewModel.Location.Id };
            var result = await viewModel.FollowSpot(followSpotViewModel);
            if (result)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}