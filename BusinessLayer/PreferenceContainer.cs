using InterfaceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class PreferenceContainer 
    {
        private IPreferenceDAL dal;
        public PreferenceContainer(IPreferenceDAL context)
        {
            this.dal = context;
        }
        public List<PreferenceModel> GetAllPreferences()
        {
            List<PreferenceModel> preferencesModel = new List<PreferenceModel>();
            List<PreferenceDTO> preferenceDTOs = this.dal.GetAllPreferences();

            foreach (PreferenceDTO preferenceDTO in preferenceDTOs)
            {
                preferencesModel.Add(new PreferenceModel(preferenceDTO));
            }

            return preferencesModel;
        }

        public PreferenceModel ShowPreferencePerUserId(int userid)
        {
            return new PreferenceModel(dal.ShowPreferencePerUserId(userid));
        }

        public SteamPreferenceModel ShowSteamPreferencePerPreferenceId(int preferenceId)
        {
            return new SteamPreferenceModel(dal.ShowSteamPreferencePerId(preferenceId));
        }

        public int UpdatePreference(PreferenceModel preferenceModel)
        {
            PreferenceDTO preferenceDTO = new PreferenceDTO();

            preferenceDTO.SteamSelect = preferenceModel.SteamSelect;

            return dal.UpdatePreference(preferenceDTO);
        }

        public int UpdateSteamPreference(SteamPreferenceModel steamPreferenceModel)
        {
            SteamPreferenceDTO steamPreferenceDTO = new SteamPreferenceDTO();

            steamPreferenceDTO.PlatformUserNameSelect = steamPreferenceModel.PlatformUserNameSelect;
            steamPreferenceDTO.GamesSelect = steamPreferenceModel.GamesSelect;

            return dal.UpdateSteamPreference(steamPreferenceDTO);
        }
    }
}
