using InterfaceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class PreferenceModel
    {
        public int PreferenceID { get; set; }
        public int SteamSelect { get; set; }
        public int UserID { get; set; }

        public PreferenceModel()
        {
        //    PreferenceID = 4;
        //    SteamSelect = 1;
        //    UserID = 22;
        }

        public PreferenceModel(PreferenceDTO preferenceDTO)
        {
            this.PreferenceID = preferenceDTO.PreferenceID;
            this.SteamSelect = preferenceDTO.SteamSelect;
            this.UserID = preferenceDTO.UserID;
        }
    }
}
