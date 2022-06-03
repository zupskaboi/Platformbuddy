using ApiWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer
{
    public interface ISteamPreferenceContainer
    {
        public void GetSteamApiData(SteamGamePlatform gamePlatform);
        //public int SelectSteamPreferenceItem(SteamPreferenceDTO steamPreferenceDTO);
        //public SteamPreferenceDTO ShowSteamPreferencePerId(int steamPreferenceId);
    }
}
