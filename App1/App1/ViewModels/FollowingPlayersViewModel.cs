using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using App1.Models;
using App1.Views;

namespace App1.ViewModels
{
    public class FollowingPlayersViewModel : BaseViewModel
    {
        public ObservableCollection<Player> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public FollowingPlayersViewModel(int spotId)
        {
            Title = "Browse";
            Items = new ObservableCollection<Player>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(spotId));

            MessagingCenter.Subscribe<PlayerDetailPage, Player>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Player;
                Items.Add(newItem);
                await PlayerDataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand(int spotId)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await PlayerDataStore.GetItemsAsync(spotId);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}