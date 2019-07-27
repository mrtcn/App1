using App1.Interfaces;
using App1.Services.DeviceServices;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

[assembly: Xamarin.Forms.Dependency(typeof(LocationService))]
namespace App1.Services.DeviceServices
{
    public class LocationService: ILocationService
    {
        public async Task<Position> GetCurrentLocation()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var location = await locator.GetPositionAsync(TimeSpan.FromTicks(10000));

                return location;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                throw fnsEx;
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                throw fneEx;
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                throw pEx;
            }
            catch (Exception ex)
            {
                // Unable to get location
                throw ex;
            }
        }
    }
}
