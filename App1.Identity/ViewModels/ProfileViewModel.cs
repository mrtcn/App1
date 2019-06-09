using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App1.Identity.Enum;

namespace App1.Identity.ViewModels
{
    public class ProfileViewModel
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public Gender Gender { get; set; }
        public String PictureUrl { get; set; }
        public DateTime? Birthday { get; set; }
        public int Win { get; set; }
        public int Credit { get; set; }
        public int Score { get; set; }
        public UserType UserType { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
