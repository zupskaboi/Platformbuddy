using BCrypt.Net;
using InterfaceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class UserContainer
    {
        private IUserDAL dal;
        //zorgt ervoor dat de container met de dal kan communiceren
        public UserContainer(IUserDAL context)
        {
            this.dal = context;
        }
        //haalt alle users op
        public List<UserModel> GetAllUsers()
        {
            List<UserModel> users = new List<UserModel>();
            List<UserDTO> dtos = this.dal.GetAllUsers();

            foreach (UserDTO dto in dtos)
            {
                users.Add(new UserModel(dto));
            }

            return users;
        }
        //zoekt naar de huidige gebruikers id
        public UserModel FindById(int userid)
        {
            return new UserModel(dal.GetById(userid));
        }
        public UserModel GetByName(string name)
        {
            return new UserModel(dal.GetByName(name));
        }
        //voegt een gebruiker toe.
        public int AddUser(UserModel userModel)
        {
            UserDTO userDTO = new UserDTO();

            userDTO.Name = userModel.Name;
            userDTO.Password = BCrypt.Net.BCrypt.HashPassword(userModel.Password);

            return dal.AddUser(userDTO);
        }
        //zorgt ervoor dat je kan inloggen
        public UserModel Login(string name, string password)
        {
            UserDTO dto = dal.Login(name);

            UserModel model = new UserModel(dto);

            if(BCrypt.Net.BCrypt.Verify(password, model.Password))
            { // verify if correct
                return model;
            } else
            {
                return null;
            }
        }
        //zorgt ervoor dat je de gebruiker kan bewerken
        public int EditUser(UserModel userModel)
        {
            UserDTO userDTO = new UserDTO();

            userDTO.Name = userModel.Name;
            userDTO.Password = userModel.Password;

            return dal.EditUser(userDTO);
        }
        //voegt een voorkeur toe
        public int AddPreference(PreferenceModel preferenceModel)
        {
            PreferenceDTO preferenceDTO = new PreferenceDTO();

            preferenceDTO.SteamSelect = preferenceModel.SteamSelect;
            preferenceDTO.UserID = preferenceModel.UserID;

            return dal.AddPreference(preferenceDTO);
        }
        //voegt een steam voorkeur toe
        public int AddSteamPreference(SteamPreferenceModel steamPreferenceModel)
        {
            SteamPreferenceDTO steamPreferenceDTO = new SteamPreferenceDTO();

            steamPreferenceDTO.PlatformUserNameSelect = steamPreferenceModel.PlatformUserNameSelect;
            steamPreferenceDTO.GamesSelect = steamPreferenceModel.GamesSelect;
            steamPreferenceDTO.PreferenceID = steamPreferenceModel.PreferenceID;

            return dal.AddSteamPreference(steamPreferenceDTO);
        }

        #region TESTING

        public UserModel LoginTest(string name, string password)
        {
            UserDTO dto = dal.LoginTest(name, password);

            UserModel model = new UserModel(dto);

            return model;
        }

        public int AddUserTest(UserModel userModel)
        {
            UserDTO userDTO = new UserDTO();

            userDTO.Name = userModel.Name;
            userDTO.Password = userModel.Password;

            return dal.AddUserTest(userDTO);
        }

        #endregion
    }
}
