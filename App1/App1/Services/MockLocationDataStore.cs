using App1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace App1.Services
{

    public class MockLocationDataStore: IDataStore<Location>
    {
        List<Location> locations;

        public MockLocationDataStore()
        {
            locations = new List<Location>();
            var guid = String.Empty;

            var newLocations = new List<Location>()
            {
                new Location(){
                    Id = guid = Guid.NewGuid().ToString(),
                    Name = "Heybeliada Tennis Club",
                    ShortDescription = "Greatest Tennis Club of the World",
                    Description = "Very Magnificiant Tennis Club with a lot of blabla and blabla",
                    Pin = new Pin(){
                        ClassId = guid,
                        Type = PinType.Place,
                        Position = new Position(40.871517, 29.089059),
                        Label = "Heybeli Tennis Club",
                        Address = "Heybeli Tennis Club Detail Info"
                    }
                },
                new Location(){
                    Id = guid =  Guid.NewGuid().ToString(),
                    Name = "Kınalıada Tennis Club",
                    ShortDescription = "Greatest Tennis Club of the World",
                    Description = "Very Magnificiant Tennis Club with a lot of blabla and blabla",
                    Pin = new Pin()
                    {
                        ClassId = guid,
                        Type = PinType.Place,
                        Position = new Position(40.906027, 29.048176),
                        Label = "Kınalı Tennis Club",
                        Address = "Kınalı Tennis Club Detail Info"
                    }
                },
                new Location(){
                    Id = guid = Guid.NewGuid().ToString(),
                    Name = "Burgazada Tennis Club",
                    ShortDescription = "Greatest Tennis Club of the World",
                    Description = "Very Magnificiant Tennis Club with a lot of blabla and blabla",
                    Pin = new Pin()
                    {
                        ClassId = guid,
                        Type = PinType.Place,
                        Position = new Position(40.880651, 29.061132),
                        Label = "Burgazada Tennis Club",
                        Address = "Burgazada Tennis Club Detail Info"
                    }
                },
                new Location(){
                    Id = guid = Guid.NewGuid().ToString(),
                    Name = "Suadiye Tennis Club",
                    ShortDescription = "Greatest Tennis Club of the World",
                    Description = "Very Magnificiant Tennis Club with a lot of blabla and blabla",
                    Pin = new Pin()
                    {
                        ClassId = guid,
                        Type = PinType.Place,
                        Position = new Position(40.958429, 29.081837),
                        Label = "Suadiye Tennis Club",
                        Address = "Suadiye Tennis Club Detail Info"
                    }
                },
                new Location(){
                    Id = guid = Guid.NewGuid().ToString(),
                    Name = "Kadıköy Tennis Club",
                    ShortDescription = "Greatest Tennis Club of the World",
                    Description = "Very Magnificiant Tennis Club with a lot of blabla and blabla",
                    Pin = new Pin()
                    {
                        ClassId = guid,
                        Type = PinType.Place,
                        Position = new Position(40.991617, 29.025069),
                        Label = "Kadıköy Tennis Club",
                        Address = "Kadıköy Tennis Club Detail Info"
                    }
                }
            };

            foreach(var location in newLocations)
            {
                locations.Add(location);
            }
        }

        public async Task<bool> AddItemAsync(Location item)
        {
            locations.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = locations.Where((Location arg) => arg.Id == id).FirstOrDefault();
            locations.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Location> GetItemAsync(string id)
        {
            return await Task.FromResult(locations.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Location>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(locations);
        }

        public async Task<bool> UpdateItemAsync(Location item)
        {
            var oldItem = locations.Where((Location arg) => arg.Id == item.Id).FirstOrDefault();
            locations.Remove(oldItem);
            locations.Add(item);

            return await Task.FromResult(true);
        }
    }
}
