using App1.Interfaces;
using App1.Models;
using App1.Services;
using App1.ViewModels;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

[assembly: Dependency(typeof(LocationDataStore))]
namespace App1.Services
{

    public class LocationDataStore : IDataStore<Location>
    {
        private readonly ILocationService _locationService;
        private readonly IHttpHandler _httpHandler;

        List<Location> locations;

        public LocationDataStore()
        {
            _httpHandler = DependencyService.Get<IHttpHandler>();
            _locationService = DependencyService.Get<ILocationService>();
            //locations = GetItemsAsync().Result?.ToList();
            locations = new List<Location>();
        }

        public async Task<bool> AddItemAsync(Location item)
        {
            locations.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (!int.TryParse(id, out int parsedId))
                parsedId = 0;

            var oldItem = locations.Where((Location arg) => arg.Id == parsedId).FirstOrDefault();
            locations.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Location> GetItemAsync(string id)
        {
            if (!int.TryParse(id, out int parsedId))
                parsedId = 0;

            return await Task.FromResult(locations.FirstOrDefault(s => s.Id == parsedId));
        }

        public async Task<IEnumerable<Location>> GetItemsAsync(params object[] args)
        {
            //var guid = String.Empty;

            var spotUrl = AppSettingsManager.Settings["spotUrl"];

            var currentLocation = await _locationService.GetCurrentLocation();
            var getSpotsUrl = AppSettingsManager.Settings["spots"];

            var coordinateModel = new CoordinateModel() { Latitude = currentLocation.Latitude, Longitude = currentLocation.Longitude };
            var jsonBody = JsonConvert.SerializeObject(coordinateModel);
            Application.Current.Properties.TryGetValue("AccessToken", out object accessTokenObject);
            var accessToken = accessTokenObject == null ? string.Empty : accessTokenObject.ToString();
            var nearLocations = await _httpHandler.AuthPostAsync<List<SpotListViewModel>>(accessToken, spotUrl, getSpotsUrl, jsonBody);

            #region static content
            //var newLocations = new List<Location>()
            //{
            //    new Location(){
            //        Id = guid = Guid.NewGuid().ToString(),
            //        Name = "Heybeliada Tennis Club",
            //        ShortDescription = "Greatest Tennis Club of the World",
            //        Description = "Very Magnificiant Tennis Club with a lot of blabla and blabla",
            //        Pin = new Pin(){
            //            ClassId = guid,
            //            Type = PinType.Place,
            //            Position = new Position(40.871517, 29.089059),
            //            Label = "Heybeli Tennis Club",
            //            Address = "Heybeli Tennis Club Detail Info"
            //        }
            //    },
            //    new Location(){
            //        Id = guid =  Guid.NewGuid().ToString(),
            //        Name = "Kınalıada Tennis Club",
            //        ShortDescription = "Greatest Tennis Club of the World",
            //        Description = "Very Magnificiant Tennis Club with a lot of blabla and blabla",
            //        Pin = new Pin()
            //        {
            //            ClassId = guid,
            //            Type = PinType.Place,
            //            Position = new Position(40.906027, 29.048176),
            //            Label = "Kınalı Tennis Club",
            //            Address = "Kınalı Tennis Club Detail Info"
            //        }
            //    },
            //    new Location(){
            //        Id = guid = Guid.NewGuid().ToString(),
            //        Name = "Burgazada Tennis Club",
            //        ShortDescription = "Greatest Tennis Club of the World",
            //        Description = "Very Magnificiant Tennis Club with a lot of blabla and blabla",
            //        Pin = new Pin()
            //        {
            //            ClassId = guid,
            //            Type = PinType.Place,
            //            Position = new Position(40.880651, 29.061132),
            //            Label = "Burgazada Tennis Club",
            //            Address = "Burgazada Tennis Club Detail Info"
            //        }
            //    },
            //    new Location(){
            //        Id = guid = Guid.NewGuid().ToString(),
            //        Name = "Suadiye Tennis Club",
            //        ShortDescription = "Greatest Tennis Club of the World",
            //        Description = "Very Magnificiant Tennis Club with a lot of blabla and blabla",
            //        Pin = new Pin()
            //        {
            //            ClassId = guid,
            //            Type = PinType.Place,
            //            Position = new Position(40.958429, 29.081837),
            //            Label = "Suadiye Tennis Club",
            //            Address = "Suadiye Tennis Club Detail Info"
            //        }
            //    },
            //    new Location(){
            //        Id = guid = Guid.NewGuid().ToString(),
            //        Name = "Kadıköy Tennis Club",
            //        ShortDescription = "Greatest Tennis Club of the World",
            //        Description = "Very Magnificiant Tennis Club with a lot of blabla and blabla",
            //        Pin = new Pin()
            //        {
            //            ClassId = guid,
            //            Type = PinType.Place,
            //            Position = new Position(40.991617, 29.025069),
            //            Label = "Kadıköy Tennis Club",
            //            Address = "Kadıköy Tennis Club Detail Info"
            //        }
            //    }
            //};
            #endregion
            foreach (var nearLocation in nearLocations)
            {
                var location = Mapper.Map<Location>(nearLocation);
                location.Pin = new Pin()
                {
                    ClassId = Guid.NewGuid().ToString(),
                    Type = PinType.Place,
                    Position = new Position(nearLocation.Latitude, nearLocation.Longitude),
                    Label = location.Name,
                    Address = location.ShortDescription
                };

                locations.Add(location);
            }

            return locations;
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
