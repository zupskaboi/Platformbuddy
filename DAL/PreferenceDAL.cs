using ApiWrapper;
using InterfaceLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PreferenceDAL : IPreferenceDAL
    {
        SqlCommand cmd;
        SqlConnection cn;
        SqlDataReader dr;

        string connectionstring = @"Server=mssqlstud.fhict.local;Database=dbi487346;User Id=dbi487346;Password=YeetWithYeet@1;";
        public List<PreferenceDTO> GetAllPreferences()
        {
            cn = new SqlConnection(connectionstring);
            cn.Open();

            var sql = "SELECT * FROM PlatformBuddyPreferences";
            cmd = new SqlCommand(sql, cn);

            dr = cmd.ExecuteReader();

            List<PreferenceDTO> preferenceDTOs = new List<PreferenceDTO>();

            while (dr.Read())
            {

                PreferenceDTO dto = new PreferenceDTO
                {
                    PreferenceID = Convert.ToInt32(dr["preferenceid"]),
                    SteamSelect = Convert.ToInt32(dr["steamselect"]),
                    UserID = Convert.ToInt32(dr["userid"])
                };

                preferenceDTOs.Add(dto);
            }

            cn.Close();

            return preferenceDTOs;
        }

        public void GetSteamApiData(SteamGamePlatform gamePlatform)
        {
            gamePlatform.SetSteamApiData();
        }

        public int UpdatePreference(PreferenceDTO preferenceDTO)
        {
            cn = new SqlConnection(connectionstring);
            cn.Open();

            var sql = "UPDATE PlatformBuddyPreferences SET steamselect = @steamselect";
            cmd = new SqlCommand(sql, cn);

            cmd.Parameters.AddWithValue("@steamselect", preferenceDTO.SteamSelect);

            return cmd.ExecuteNonQuery();
        }

        public PreferenceDTO ShowPreferencePerUserId(int userid)
        {
            cn = new SqlConnection(connectionstring);
            cn.Open();

            var sql = "SELECT * FROM PlatformBuddyPreferences WHERE userid = " + userid;
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
            dr.Read();

            try
            {
                PreferenceDTO preferenceDTO = new PreferenceDTO
                {
                    PreferenceID = Convert.ToInt32(dr["preferenceid"]),
                    SteamSelect = Convert.ToInt32(dr["steamselect"]),
                    UserID = Convert.ToInt32(dr["userid"])
                };

                return preferenceDTO;
            }
            catch
            {
                PreferenceDTO preferenceDTO = new PreferenceDTO
                {
                    PreferenceID = Convert.ToInt32(dr["preferenceid"]),
                    SteamSelect = Convert.ToInt32(dr["steamselect"]),
                    UserID = Convert.ToInt32(dr["userid"])
                };

                return preferenceDTO;
            }
        }

        public int UpdateSteamPreference(SteamPreferenceDTO steamPreferenceDTO)
        {
            cn = new SqlConnection(connectionstring);
            cn.Open();

            var sql = "UPDATE PlatformBuddySteamPreferences SET platformusernameselect = @platformusernameselect, gamesselect = @gamesselect";
            cmd = new SqlCommand(sql, cn);

            cmd.Parameters.AddWithValue("@platformusernameselect", steamPreferenceDTO.PlatformUserNameSelect);
            cmd.Parameters.AddWithValue("@gamesselect", steamPreferenceDTO.GamesSelect);

            return cmd.ExecuteNonQuery();
        }

        public SteamPreferenceDTO ShowSteamPreferencePerId(int preferenceId)
        {
            cn = new SqlConnection(connectionstring);
            cn.Open();

            var sql = "SELECT * FROM PlatformBuddySteamPreferences WHERE preferenceid = " + preferenceId;
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
            dr.Read();

            SteamPreferenceDTO steamPreferenceDTO = new SteamPreferenceDTO
            {
                SteamPreferenceID = Convert.ToInt32(dr["steampreferenceid"]),
                PlatformUserNameSelect = Convert.ToInt32(dr["platformusernameselect"]),
                GamesSelect = Convert.ToInt32(dr["gamesselect"]),
                PreferenceID = Convert.ToInt32(dr["preferenceid"])
            };

            return steamPreferenceDTO;
        }
    }
}
