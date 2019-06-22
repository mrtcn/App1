using System;
using System.Collections.Generic;
using App1.Models;
using App1.ServiceBase.Enums;
using App1.ServiceBase.Interfaces;
using App1.ServiceBase.ServiceBase.Models;

namespace App1.Spot.Data.Entities
{
    public interface ISpot : IEntity, IHasStatus, IDateOperationFields
    {
        string Name { get; set; }
        string ShortDescription { get; set; }
        string Description { get; set; }
        string ImageUrl { get; set; }
        double Latitude { get; }
        double Longitude { get; }
    }

    public class Spot: ISpot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Latitude { get; }
        public double Longitude { get; }
        public Status Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public ICollection<PlayerSpot> PlayerSpots { get; set; }
    }
}
