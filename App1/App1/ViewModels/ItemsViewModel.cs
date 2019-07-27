using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using App1.Models;
using App1.Views;
using Xamarin.Forms.Maps;

namespace App1.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Location> Items { get; set; }
        public ObservableCollection<Location> MapItems { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Kortlar";
            Items = new ObservableCollection<Location>();
            MapItems = new ObservableCollection<Location>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Location>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Location;
                Items.Add(newItem);
                MapItems.Add(newItem);
                await LocationDataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await LocationDataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }

                foreach (var locationItem in Items)
                {
                    try
                    {
                        MapItems.Add(locationItem);
                    }
                    catch (Exception ex)
                    {

                        continue;
                    }
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