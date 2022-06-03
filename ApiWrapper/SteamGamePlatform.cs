using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiWrapper
{
    public class SteamGamePlatform
    {
        public string PlatformName { get; set; }
        public string PlatformUserName { get; set; }
        public string GamesAmount { get; set; }

        public SteamOwnedGamesData steamOwnedGamesData;
        public SteamPlayerData steamPlayerData;

        public SteamGamePlatform()
        {
            PlatformName = "Steam";
        }

        public void ReceiveSteamApiData(string steamId)
        {
            System.Net.WebClient downloader = new System.Net.WebClient();
            string ownedGamesDataJson = downloader.DownloadString("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=61E6FF710E52D8C353B124A3BC175C22&steamid=" + steamId);
            string playerDataJson = downloader.DownloadString("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=61E6FF710E52D8C353B124A3BC175C22&steamids=" + steamId);

            JObject ownedGamesObject = JObject.Parse(ownedGamesDataJson);
            JObject playersObject = JObject.Parse(playerDataJson);

            steamOwnedGamesData = JsonConvert.DeserializeObject<SteamOwnedGamesData>(JsonConvert.SerializeObject(ownedGamesObject["response"]));
            steamPlayerData = JsonConvert.DeserializeObject<SteamPlayerData>(JsonConvert.SerializeObject(playersObject["response"]["players"][0]));
        }

        public List<SteamGamePlatform> SetSteamApiData()
        {
            ReceiveSteamApiData("76561198111088981");
            List<SteamGamePlatform> gamePlatform = new List<SteamGamePlatform>
            {
                new SteamGamePlatform { PlatformName = this.PlatformName, PlatformUserName = steamPlayerData.personaname, GamesAmount = steamOwnedGamesData.game_count}
            };

            return gamePlatform;
        }
    }

    public class SteamOwnedGamesData
    {
        public string steamid { get; set; }
        public string game_count { get; set; }
    }

    public class SteamPlayerData
    {
        public string steamid { get; set; }
        public string personaname { get; set; }
    }

}