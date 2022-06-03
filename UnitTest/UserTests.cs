using ApiWrapper;
using BusinessLayer;
using DAL;

namespace UnitTest
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void LoginUser_Test()
        {
            string name = "naam";
            string password = "wachtwoord";
            UserContainer userContainer = new UserContainer(new UserDAL());
            ArgumentNullException.ThrowIfNull(nameof(userContainer.LoginTest));
            UserModel userModel = userContainer.LoginTest(name, password);

            string expectedName = "naam";
            string expectedPassword = "wachtwoord";

            Assert.AreEqual(expectedName, userModel.Name, "names are the same");
            Assert.AreEqual(expectedPassword, userModel.Password.Trim(), "passwords are the same");
        }

        [TestMethod]
        public void Register_Test()
        {
            UserModel userModel = new UserModel();
            userModel.Name = "nogeennaam";
            userModel.Password = "nogeenwachtwoord";
            UserContainer userContainer = new UserContainer(new UserDAL());

            userContainer.AddUserTest(userModel);
            string name = "nogeennaam";
            string password = "nogeenwachtwoord";
            userModel = userContainer.LoginTest(name, password);

            string expectedName = "nogeennaam";
            string expectedPassword = "nogeenwachtwoord";

            Assert.AreEqual(expectedName, userModel.Name, "names are the same");
            Assert.AreEqual(expectedPassword, userModel.Password.Trim(), "passwords are the same");
        }

        [TestMethod]
        public void SetSteamApiData_Test()
        {
            SteamGamePlatform steamGamePlatform = new SteamGamePlatform();
            
            steamGamePlatform.ReceiveSteamApiData("76561198111088981");
            List<SteamGamePlatform> gamePlatform = steamGamePlatform.SetSteamApiData();

            List<SteamGamePlatform> expectedList = new List<SteamGamePlatform>
            {
                new SteamGamePlatform { PlatformName = "Steam", PlatformUserName = "zupskaboi", GamesAmount = "216"}
            };

            //Assert.IsTrue(gamePlatform.SequenceEqual(expectedList));
            CollectionAssert.AreEqual(expectedList, gamePlatform, "It works");
            //if (gamePlatform.SequenceEqual(ExpectedList))
            //{
            //    Console.WriteLine("THEY ARE EQUEL");
            //}
            //else
            //{
            //    Console.WriteLine("they are not equel....");
            //}
        }
    }
}