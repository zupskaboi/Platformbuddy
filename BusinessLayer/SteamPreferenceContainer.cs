using ApiWrapper;
using InterfaceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SteamPreferenceContainer : ISteamPreferenceContainer
    {
        public void GetSteamApiData(SteamGamePlatform gamePlatform)
        {
            gamePlatform.SetSteamApiData();
        }
        //public SteamPreference ShowSteamPreferencePerId(int steamPreferenceId)
        //{
        //    SteamPreferenceDTO steamPreferenceDTO = this.dal.ShowSteamPreferencePerId(steamPreferenceId);
        //    SteamPreference steampreference = new SteamPreference(steamPreferenceDTO);

        //    return steampreference;
        //}
        //public int SelectSteamPreferenceItem(SteamPreferenceDTO steamPreferenceDTO)
        //{
        //    SteamPreferenceDTO steamPreferencesDTO = new SteamPreferenceDTO();

        //    steamPreferenceDTO.PlatformUserNameSelect = steamPreferenceDTO.PlatformUserNameSelect;
        //    steamPreferenceDTO.GamesSelect = steamPreferenceDTO.GamesSelect;

        //    return dal.SelectSteamPreferenceItem(steamPreferencesDTO);
        //}
    }
}
