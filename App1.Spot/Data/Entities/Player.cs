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
        String FirstName { get; set; }
        String LastName { get; set; }
        String Nickname { get; set; }
        String PictureUrl { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
    }

    public class Player: IPlayer
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Nickname { get; set; }
        public String PictureUrl { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Status Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public ICollection<PlayerSpot> PlayerSpots { get; set; }
    }
}
