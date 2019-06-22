using App1.Models;
using App1.ServiceBase.Enums;
using App1.ServiceBase.Interfaces;
using App1.ServiceBase.Models;
using System;

namespace App1.Spot.EntityParams
{
    public class PlayerEntityParams : IEntityParams, IPlayer, IOperatorFields
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImageUrl { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Status Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
