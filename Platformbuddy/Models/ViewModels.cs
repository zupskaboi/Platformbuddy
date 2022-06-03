using ApiWrapper;
using BusinessLayer;

namespace Platformbuddy.Models
{
    public class ViewModels
    {
        public SteamGamePlatform steamGamePlatform { get; set; }
        public UserViewModel userViewModel { get; set; }
        public PreferenceViewModel preferenceViewModel { get; set; }
        public SteamPreferenceViewModel steamPreferenceViewModel { get; set; }
    }
}
