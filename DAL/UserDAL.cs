using InterfaceLayer;
using System.Data.SqlClient;

namespace DAL
{
    public class UserDAL : IUserDAL, IUserContainer
    {
        //voegt de sql classes toe voor: het uitvoeren van de sql querys, voor de connectie van de database en voor het lezen van de data.
        SqlCommand cmd;
        SqlConnection cn;
        SqlDataReader dr;

        string connectionstring = @"Server=mssqlstud.fhict.local;Database=dbi487346;User Id=dbi487346;Password=YeetWithYeet@1;";

        public List<UserDTO> GetAllUsers()
        {
            //maakt de connectie met de database.
            cn = new SqlConnection(connectionstring);
            cn.Open();
            //zorgt ervoor dat de query word uitgevoerd.
            var sql = "SELECT * FROM PlatformBuddyUsers";
            cmd = new SqlCommand(sql, cn);

            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
                Console.WriteLine();
                Console.WriteLine("Query Executed: " + sql);
                Console.WriteLine();
                dr.Close();
            }

            List<UserDTO> userDTOs = new List<UserDTO>();
            //leest de data en haalt dan alle users op.
            while (dr.Read())
            {

                UserDTO userDTO = new UserDTO
                {
                    UserID = Convert.ToInt32(dr["userid"]),
                    Name = Convert.ToString(dr["name"]),
                    Password = Convert.ToString(dr["password"])
                };

                userDTOs.Add(userDTO);
            }

            cn.Close();

            return userDTOs;
        }

        public UserDTO GetByName(string name)
        {
            cn = new SqlConnection(connectionstring);
            cn.Open();

            var sql = "SELECT * FROM PlatformBuddyUsers WHERE name = @name";
            cmd = new SqlCommand(sql, cn);

            cmd.Parameters.AddWithValue("@name", name);

            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
                Console.WriteLine();
                Console.WriteLine("Query Executed: " + sql);
                Console.WriteLine();
                dr.Close();
            }
            dr.Read();

            UserDTO userDTO = new UserDTO
            {
                UserID = Convert.ToInt32(dr["userid"]),
                Name = Convert.ToString(dr["name"]),
                Password = Convert.ToString(dr["password"])
            };

            return userDTO;
        }

        public UserDTO GetById(int userid)
        {
            cn = new SqlConnection(connectionstring);
            cn.Open();

            var sql = "SELECT * FROM PlatformBuddyUsers WHERE userid = @userid";
            cmd = new SqlCommand(sql, cn);

            cmd.Parameters.AddWithValue("@userid", userid);

            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
                Console.WriteLine();
                Console.WriteLine("Query Executed: " + sql);
                Console.WriteLine();
                dr.Close();
            }
            dr.Read();

            UserDTO userDTO = new UserDTO
            {
                UserID = Convert.ToInt32(dr["userid"]),
                Name = Convert.ToString(dr["name"]),
                Password = Convert.ToString(dr["password"])
            };

            return userDTO;
        }

        public int AddUser(UserDTO userDTO)
        {
            cn = new SqlConnection(connectionstring);
            cn.Open();

            var sql = "IF NOT EXISTS(SELECT 1 FROM PlatformBuddyUsers WHERE name = @name) INSERT INTO PlatformBuddyUsers (name, password) VALUES (@name, @password)";
            cmd = new SqlCommand(sql, cn);

            cmd.Parameters.AddWithValue("@name", userDTO.Name);
            cmd.Parameters.AddWithValue("@password", userDTO.Password);

            return cmd.ExecuteNonQuery();
        }

        public UserDTO Login(string name)
        {
            cn = new SqlConnection(connectionstring);
            cn.Open();

            var sql = "SELECT * FROM PlatformBuddyUsers WHERE name = @name";
            cmd = new SqlCommand(sql, cn);

            cmd.Parameters.AddWithValue("@name", name);

            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
                Console.WriteLine();
                Console.WriteLine("Query Executed: " + sql);
                Console.WriteLine();
                dr.Close();
            }

            dr.Read();

            UserDTO userDTO = new UserDTO
            {
                UserID = Convert.ToInt32(dr["userid"]),
                Name = Convert.ToString(dr["name"]),
                Password= Convert.ToString(dr["password"])
            };

            return userDTO;
        }

        public int EditUser(UserDTO userDTO)
        {
            cn = new SqlConnection(connectionstring);
            cn.Open();

            var sql = "UPDATE PlatformBuddyUsers SET name = @name, password = @password";
            cmd = new SqlCommand(sql, cn);

            cmd.Parameters.AddWithValue("@name", userDTO.Name);
            cmd.Parameters.AddWithValue("@password", userDTO.Password);


            return cmd.ExecuteNonQuery();
        }

        public int AddPreference(PreferenceDTO preferenceDTO)
        {
            cn = new SqlConnection(connectionstring);
            cn.Open();

            var sql = "INSERT INTO PlatformBuddyPreferences (steamselect, userid) VALUES (@steamselect, @userid)";
            cmd = new SqlCommand(sql, cn);

            cmd.Parameters.AddWithValue("@steamselect", preferenceDTO.SteamSelect);
            cmd.Parameters.AddWithValue("@userid", preferenceDTO.UserID);

            return cmd.ExecuteNonQuery();
        }

        public int AddSteamPreference(SteamPreferenceDTO steamPreferenceDTO)
        {
            cn = new SqlConnection(connectionstring);
            cn.Open();

            var sql = "INSERT INTO PlatformBuddySteamPreferences (platformusernameselect, gamesselect, preferenceid) VALUES (@platformusernameselect, @gamesselect, @preferenceid)"; /*SELECT preferenceid FROM PlatformBuddyPreferences*/
            cmd = new SqlCommand(sql, cn);

            cmd.Parameters.AddWithValue("@platformusernameselect", steamPreferenceDTO.PlatformUserNameSelect);
            cmd.Parameters.AddWithValue("@gamesselect", steamPreferenceDTO.GamesSelect);
            cmd.Parameters.AddWithValue("@preferenceid", steamPreferenceDTO.PreferenceID);

            return cmd.ExecuteNonQuery();
        }

        #region TESTING

        public UserDTO LoginTest(string name, string password)
        {
            cn = new SqlConnection(connectionstring);
            cn.Open();

            var sql = "SELECT * FROM TestUser WHERE name = @name AND password = @password";
            cmd = new SqlCommand(sql, cn);

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@password", password);

            try 
            {
                dr = cmd.ExecuteReader();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
                Console.WriteLine();
                Console.WriteLine("Query Executed: " + sql);
                Console.WriteLine();
                dr.Close();
            }
            
            dr.Read();

            UserDTO userDTO = new UserDTO
            {
                UserID = Convert.ToInt32(dr["userid"]),
                Name = Convert.ToString(dr["name"]),
                Password = Convert.ToString(dr["password"])
            };

            return userDTO;
        }

        public int AddUserTest(UserDTO userDTO)
        {
            cn = new SqlConnection(connectionstring);
            cn.Open();

            var sql = "INSERT INTO TestUser (name, password) VALUES (@name, @password)";
            try
            {
                cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@name", userDTO.Name);
                cmd.Parameters.AddWithValue("@password", userDTO.Password);

                return cmd.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
                Console.WriteLine();
                Console.WriteLine("Query Executed: " + sql);
                Console.WriteLine();
                dr.Close();
                return 0;
            }
            
        }

        #endregion
    }
}