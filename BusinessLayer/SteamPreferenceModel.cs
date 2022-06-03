using InterfaceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SteamPreferenceModel
    {
        public int SteamPreferenceID { get; set; }
        public int PlatformUserNameSelect { get; set; }
        public int GamesSelect { get; set; }
        public int PreferenceID { get; set; }
        

        public SteamPreferenceModel()
        {
        //    SteamPreferenceID = 2;
        //    PlatformUserNameSelect = 1;
        //    GamesSelect = 1;
        //    PreferenceID = 4;
        }

        public SteamPreferenceModel(SteamPreferenceDTO steamPreferenceDTO)
        {
            this.PlatformUserNameSelect = steamPreferenceDTO.PlatformUserNameSelect;
            this.GamesSelect = steamPreferenceDTO.GamesSelect;
            this.PreferenceID = steamPreferenceDTO.PreferenceID;
        }
    }
}
