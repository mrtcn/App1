
namespace App1.Spot.ViewModels
{
    public class SpotListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Latitude { get; }
        public double Longitude { get; }
    }
}
