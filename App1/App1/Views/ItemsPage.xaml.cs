﻿using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;

using App1.Models;
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
            var location = args.SelectedItem as Location;
            if (location == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(location)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override async void OnAppearing()
        {
            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                var permissions = new Dictionary<Permission, PermissionStatus>();

                if (status != PermissionStatus.Granted)
                {
                    try
                    {
                        permissions = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                        permissions.TryGetValue(Permission.Location, out status);
                    }
                    catch (Exception ex)
                    {
                        status = permissions.FirstOrDefault(x => x.Key == Permission.Location).Value;
                    }
                }

                if (status == PermissionStatus.Granted)
                {
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;

                    var location = await locator.GetPositionAsync(TimeSpan.FromTicks(10000));
                    Position position = new Position(location.Latitude, location.Longitude);

                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(3)));

                    foreach(var locationItem in viewModel.Items)
                    {
                        locationItem.Pin.Clicked += NavigateToLocation;
                        MyMap.Pins.Add(locationItem.Pin);
                    }

                    MyMap.IsShowingUser = true;

                    base.OnAppearing();

                }
                else if(status == PermissionStatus.Denied)
                {
                    await DisplayAlert("Need location", "Gunna need that location", "OK");
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

        private async void NavigateToLocation(object sender, EventArgs e)
        {
            var pin = sender as Pin;
            var location = viewModel.Items.FirstOrDefault(x => x.Id == pin.ClassId.ToString());
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(location)));
        }
    }
}