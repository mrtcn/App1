using App1.ServiceBase.Enums;
using App1.ServiceBase.Interfaces;
using App1.ServiceBase.ServiceBase.Models;
using System;
using System.Collections.Generic;

namespace App1.Models
{
    public interface IPlayer : IEntity, IHasStatus, IDateOperationFields
    {
        string UserId { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        string ImageUrl { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
    }

    public class Player: IPlayer
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
        public ICollection<PlayerSpot> PlayerSpots { get; set; }
    }
}
