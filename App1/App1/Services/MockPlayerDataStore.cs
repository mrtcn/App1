using App1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace App1.Services
{

    public class MockPlayerDataStore: IDataStore<Player>
    {
        List<Player> players;

        public MockPlayerDataStore()
        {
            players = new List<Player>();
            var guid = String.Empty;

            var newPlayers = new List<Player>()
            {
                new Player(){
                    Id = guid = Guid.NewGuid().ToString(),
                    Name = "Murat Can",
                    Surname = "Tuna",
                    ImageUrl = String.Empty
                },
                new Player(){
                    Id = guid = Guid.NewGuid().ToString(),
                    Name = "Duygu Tozduman",
                    Surname = "Tuna",
                    ImageUrl = String.Empty
                },
                new Player(){
                    Id = guid = Guid.NewGuid().ToString(),
                    Name = "Ayça",
                    Surname = "Tozduman",
                    ImageUrl = String.Empty
                },
                new Player(){
                    Id = guid = Guid.NewGuid().ToString(),
                    Name = "Özlem Selin Tuna",
                    Surname = "İnkayalı",
                    ImageUrl = String.Empty
                },
                new Player(){
                    Id = guid = Guid.NewGuid().ToString(),
                    Name = "İlker",
                    Surname = "İnkayalı",
                    ImageUrl = String.Empty
                }
            };

            foreach(var player in newPlayers)
            {
                players.Add(player);
            }
        }

        public async Task<bool> AddItemAsync(Player item)
        {
            players.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = players.Where((Player arg) => arg.Id == id).FirstOrDefault();
            players.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Player> GetItemAsync(string id)
        {
            return await Task.FromResult(players.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Player>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(players);
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
