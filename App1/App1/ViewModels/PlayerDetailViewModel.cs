using App1.Interfaces;
using App1.Models;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class PlayerDetailViewModel : BaseViewModel
    {
        IMultiMediaPickerService _multiMediaPickerService;

        public ObservableCollection<MediaFile> Media { get; set; }
        public ICommand SelectImagesCommand { get; set; }
        public ICommand SelectVideosCommand { get; set; }

        public Player Player { get; set; }
        public PlayerDetailViewModel(Player player = null)
        {
            Title = player?.FirstName + " Profil";
            Player = player;
            _multiMediaPickerService = DependencyService.Get<IMultiMediaPickerService>();

            SelectImagesCommand = new Command(async (obj) =>
            {
                var hasPermission = await CheckPermissionsAsync();
                if (hasPermission)
                {
                    Media = new ObservableCollection<MediaFile>();
                    await _multiMediaPickerService.PickPhotosAsync();
                }
            });

            SelectVideosCommand = new Command(async (obj) =>
            {
                var hasPermission = await CheckPermissionsAsync();
                if (hasPermission)
                {

                    Media = new ObservableCollection<MediaFile>();

                    await _multiMediaPickerService.PickVideosAsync();

                }
            });

            _multiMediaPickerService.OnMediaPicked += (s, a) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Media.Add(a);

                });

            };
        }

        async Task<bool> CheckPermissionsAsync()
        {
            var retVal = false;
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Storage);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Storage))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Need Storage permission to access to your photos.", "Ok");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Plugin.Permissions.Abstractions.Permission.Storage });
                    status = results[Plugin.Permissions.Abstractions.Permission.Storage];
                }

                if (status == PermissionStatus.Granted)
                {
                    retVal = true;

                }
                else if (status != PermissionStatus.Unknown)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Permission Denied. Can not continue, try again.", "Ok");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await App.Current.MainPage.DisplayAlert("Alert", "Error. Can not continue, try again.", "Ok");
            }

            return retVal;

        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
