using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace App1.Models
{
    public class Location
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public Pin Pin { get; set; }
    }
}
