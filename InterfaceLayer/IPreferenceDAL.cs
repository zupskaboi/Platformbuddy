using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer
{
    public interface IPreferenceDAL
    {
        public List<PreferenceDTO> GetAllPreferences();
        public PreferenceDTO ShowPreferencePerUserId(int preferenceId);
        public int UpdatePreference(PreferenceDTO preferencesDTO);
        public int UpdateSteamPreference(SteamPreferenceDTO steamPreferenceDTO);
        public SteamPreferenceDTO ShowSteamPreferencePerId(int userId);
    }
}
