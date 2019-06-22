using App1.ServiceBase.Enums;
using App1.ServiceBase.Interfaces;
using App1.ServiceBase.Models;
using App1.Spot.Data.Entities;
using System;

namespace App1.Spot.EntityParams
{
    public class SpotEntityParams : IEntityParams, ISpot, IOperatorFields
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
    }
}
