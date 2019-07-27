using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App1.Spot.ViewModels
{
    public class CreateOrUpdatePlayerViewModel
    {
        public string UserId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Nickname { get; set; }
        public String PictureUrl { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
