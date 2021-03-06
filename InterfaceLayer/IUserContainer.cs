using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer
{
    public interface IUserContainer
    {
        public List<UserDTO> GetAllUsers();
        public UserDTO GetById(int userid);
        public int AddUser(UserDTO userDTO);
        public UserDTO Login(string name);
        public int EditUser(UserDTO userDTO);
        public int AddPreference(PreferenceDTO preferenceDTO);
        public int AddSteamPreference(SteamPreferenceDTO steamPreferenceDTO);
    }
}
