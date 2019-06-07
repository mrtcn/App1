using System;

using App1.Models;

namespace App1.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Location Location { get; set; }
        public ItemDetailViewModel(Location location = null)
        {
            Title = location?.Name;
            Location = location;
        }
    }
}
