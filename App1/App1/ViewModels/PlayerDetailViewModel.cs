using App1.Models;

namespace App1.ViewModels
{
    public class PlayerDetailViewModel : BaseViewModel
    {
        public Player Player { get; set; }
        public PlayerDetailViewModel(Player player = null)
        {
            Title = player?.Name + " Profil";
            Player = player;
        }
    }
}
