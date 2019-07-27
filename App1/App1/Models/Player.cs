
namespace App1.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string PictureUrl { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
