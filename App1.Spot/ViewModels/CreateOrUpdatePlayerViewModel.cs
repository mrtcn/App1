using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App1.Spot.ViewModels
{
    public class CreateOrUpdatePlayerViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImageUrl { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
