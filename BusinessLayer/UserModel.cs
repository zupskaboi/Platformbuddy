using InterfaceLayer;

namespace BusinessLayer
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public UserModel()
        {
            //UserID = 22;
            //Name = "helper";
            //Password = "man";
        }

        public UserModel(UserDTO userDTO)
        {
            UserID = userDTO.UserID;
            Name = userDTO.Name;
            Password = userDTO.Password;
        }
    }
}