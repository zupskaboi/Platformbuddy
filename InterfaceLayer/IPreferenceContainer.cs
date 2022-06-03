using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer
{
    public interface IPreferenceContainer
    {
        public List<PreferenceDTO> GetAllPreferences();
        public PreferenceDTO ShowPreferencePerId(int preferenceId);
        public int SelectPreferenceItem(PreferenceDTO preferencesModel);
    }
}
