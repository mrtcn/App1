using App1.Interfaces;
using App1.Models;
using App1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

[assembly: Dependency(typeof(PlayerDataStore))]
namespace App1.Services
{

    public class PlayerDataStore : IDataStore<Player>
    {
        private readonly ILocationService _locationService;
        private readonly IHttpHandler _httpHandler;

        List<Player> players;

        public PlayerDataStore()
        {
            _httpHandler = DependencyService.Get<IHttpHandler>();
            players = new List<Player>();
        }

        public async Task<bool> AddItemAsync(Player item)
        {
            players.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (!int.TryParse(id, out int parsedId))
                throw new ArgumentException(id);

            var oldItem = players.Where((Player arg) => arg.Id == parsedId).FirstOrDefault();
            players.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Player> GetItemAsync(string id)
        {
            if (!int.TryParse(id, out int parsedId))
                throw new ArgumentException(id);

            return await Task.FromResult(players.FirstOrDefault(s => s.Id == parsedId));
        }

        public async Task<IEnumerable<Player>> GetItemsAsync(params object[] args)
        {
            //var guid = String.Empty;
            var spotId = args[0].ToString();

            var spotUrl = AppSettingsManager.Settings["spotUrl"];
            var followingPlayersUrl = AppSettingsManager.Settings["followingPlayers"];

            Application.Current.Properties.TryGetValue("AccessToken", out object accessTokenObject);
            var accessToken = accessTokenObject == null ? string.Empty : accessTokenObject.ToString();
            players = await _httpHandler.AuthPostAsync<List<Player>>(accessToken, spotUrl, followingPlayersUrl, spotId);

            return players;
        }

        public async Task<bool> UpdateItemAsync(Player item)
        {
            var oldItem = players.Where((Player arg) => arg.Id == item.Id).FirstOrDefault();
            players.Remove(oldItem);
            players.Add(item);

            return await Task.FromResult(true);
        }
    }
}
