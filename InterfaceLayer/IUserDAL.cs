using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer
{
    public interface IUserDAL
    {
        public List<UserDTO> GetAllUsers();
        public UserDTO GetByName(string name);
        public UserDTO GetById(int userid);
        public int AddUser(UserDTO userDTO);
        public int AddUserTest(UserDTO userDTO);
        public UserDTO Login(string name);
        public UserDTO LoginTest(string name, string password);
        public int EditUser(UserDTO userDTO);
        public int AddPreference(PreferenceDTO preferenceDTO);
        public int AddSteamPreference(SteamPreferenceDTO steamPreferenceDTO);
    }
}
