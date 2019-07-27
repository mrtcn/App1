using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using App1.Interfaces;
using App1.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Location Location { get; set; }
        public ObservableCollection<Player> Players { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemDetailViewModel(Location location = null)
        {
            Title = location?.Name;
            Location = location;
            Players = new ObservableCollection<Player>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        public async Task<bool> FollowSpot(FollowSpotViewModel model)
        {
            var httpHandler = DependencyService.Get<IHttpHandler>();
            var identityUrl = AppSettingsManager.Settings["spotUrl"];
            var jsonBody = JsonConvert.SerializeObject(model);

            Application.Current.Properties.TryGetValue("AccessToken", out object accessTokenObject);
            var accessToken = accessTokenObject == null ? string.Empty : accessTokenObject.ToString();

            var followSpotResponse = await httpHandler.AuthPostAsync<FollowSpotResponse>(accessToken, identityUrl, AppSettingsManager.Settings["FollowSpot"], jsonBody);

            return followSpotResponse.IsSuccess;
        }

        async Task ExecuteLoadItemsCommand () {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Players.Clear();
                var players = await PlayerDataStore.GetItemsAsync(Location.Id);
                foreach (var player in players)
                {
                    Players.Add(player);
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
