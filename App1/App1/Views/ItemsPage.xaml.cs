using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using App1.Models;
using App1.Views;
using App1.ViewModels;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;

namespace App1.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);


            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var permissions = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    status = permissions.FirstOrDefault(x => x.Key == Permission.Location).Value;
                }

                if (status == PermissionStatus.Granted)
                {
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;

                    var location = await locator.GetPositionAsync(TimeSpan.FromTicks(10000));
                    Position position = new Position(location.Latitude, location.Longitude);

                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(3)));

                    var heybeliTennisClubPosition = new Position(40.871517, 29.089059); // Latitude, Longitude
                    var heybeliTennisClubPin = new Pin
                    {
                        Type = PinType.Place,
                        Position = heybeliTennisClubPosition,
                        Label = "Heybeli Tennis Club",
                        Address = "Heybeli Tennis Club Detail Info"
                    };

                    var kinaliTennisClubPosition = new Position(40.906027, 29.048176); // Latitude, Longitude
                    var kinaliTennisClubPin = new Pin
                    {
                        Type = PinType.Place,
                        Position = kinaliTennisClubPosition,
                        Label = "Kınalı Tennis Club",
                        Address = "Kınalı Tennis Club Detail Info"
                    };

                    MyMap.Pins.Add(heybeliTennisClubPin);
                    MyMap.Pins.Add(kinaliTennisClubPin);
                }
                else if (status != PermissionStatus.Unknown)
                {
                    //location denied
                }
            }
            catch (Exception ex)
            {
                //Something went wrong
            }
        }
    }
}