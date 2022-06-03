using BusinessLayer;
using ApiWrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformbuddy.Models;
using DAL;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Platformbuddy.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        //In de index haal ik de objecten van de benodigde models om te tonen op de pagina
        public ActionResult Index(int userid, int preferenceid)
        {
            UserContainer userContainer = new UserContainer(new UserDAL());
            PreferenceContainer preferenceContainer = new PreferenceContainer(new PreferenceDAL());
            ViewModels viewModels = new ViewModels();
            viewModels.steamGamePlatform = new SteamGamePlatform();
            viewModels.steamGamePlatform.SetSteamApiData();

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            userid = int.TryParse(userIdClaim, out var id) ? id : 0;

            UserModel userModel = userContainer.FindById(userid);
            viewModels.userViewModel = new UserViewModel() { UserID = userModel.UserID, Name = userModel.Name};

            PreferenceModel preferenceModel = preferenceContainer.ShowPreferencePerUserId(userid);
            viewModels.preferenceViewModel = new PreferenceViewModel() {PreferenceID = preferenceModel.PreferenceID, SteamSelect = preferenceModel.SteamSelect, UserID = userModel.UserID};
            
            preferenceid = preferenceModel.PreferenceID;
            SteamPreferenceModel steamPreferenceModel = preferenceContainer.ShowSteamPreferencePerPreferenceId(preferenceid);
            viewModels.steamPreferenceViewModel = new SteamPreferenceViewModel() {SteamPreferenceID = steamPreferenceModel.SteamPreferenceID, GamesSelect = steamPreferenceModel.GamesSelect, PlatformUserNameSelect = steamPreferenceModel.PlatformUserNameSelect, PreferenceID = preferenceModel.PreferenceID };
            
            return View(viewModels);
        }

        [AllowAnonymous]
        public IActionResult LoginUser()
        {
            return View();
        }
        //De action om in te loggen
        [HttpPost, AllowAnonymous] 
        public IActionResult LoginUser(string name, string password)
        {
            try
            {
                UserContainer userContainer = new UserContainer(new UserDAL());
                UserModel userModel = userContainer.Login(name, password);

                if (userModel == null)
                {
                    return Conflict("credentials not found");
                }

                //claims n cookies
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userModel.UserID.ToString()),
                    new Claim(ClaimTypes.Name, userModel.Name)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                return SignIn(new ClaimsPrincipal(claimsIdentity), CookieAuthenticationDefaults.AuthenticationScheme);
                //return Ok();
                //return RedirectToAction(actionName: "index", routeValues: viewModels.userModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Logout()
        {
            // Clear the existing external cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "User");
        }

        [AllowAnonymous]
        public IActionResult RegisterUser()
        {
            return View();
        }
        //functie om te registreren
        [HttpPost, AllowAnonymous]
        public IActionResult RegisterUser(UserModel userModel, PreferenceModel preferenceModel)
        {
            try
            {
                UserContainer userContainer = new UserContainer(new UserDAL());
                PreferenceContainer preferenceContainer = new PreferenceContainer(new PreferenceDAL());
                
                if (userContainer.AddUser(userModel) == -1)
                {
                    return Conflict("Name already exist");
                }
                    userModel = userContainer.GetByName(userModel.Name);
                    //userModel = userContainer.FindById(userModel.UserID);
                    preferenceModel = new PreferenceModel() { SteamSelect = 0, UserID = userModel.UserID};
                    userContainer.AddPreference(preferenceModel);

                    preferenceModel = preferenceContainer.ShowPreferencePerUserId(userModel.UserID);
                    SteamPreferenceModel steamPreferenceModel = new SteamPreferenceModel() { PlatformUserNameSelect = 0, GamesSelect = 0, PreferenceID = preferenceModel.PreferenceID};
                    userContainer.AddSteamPreference(steamPreferenceModel);


                    return Ok();
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Geeft de pagina weer met de juiste model
        public IActionResult UpdatePreference(int userid)
        {
            UserContainer userContainer = new UserContainer(new UserDAL());
            PreferenceContainer preferenceContainer = new PreferenceContainer(new PreferenceDAL());
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            userid = int.TryParse(userIdClaim, out var id) ? id : 0;
            UserModel userModel = userContainer.FindById(userid);
            PreferenceModel preferenceModel = preferenceContainer.ShowPreferencePerUserId(userid);
            PreferenceViewModel preferenceViewModel = new PreferenceViewModel() {SteamSelect = preferenceModel.SteamSelect, UserID = userModel.UserID};
            return View(preferenceViewModel);
        }
        //De functie om een preference te wijzigen.
        [HttpPost]
        public IActionResult UpdatePreference(PreferenceModel preferenceModel, int userid)
        {
            try
            {
                PreferenceContainer preferenceContainer = new PreferenceContainer(new PreferenceDAL());
                preferenceContainer.UpdatePreference(preferenceModel);

                return Ok();
            }
            catch
            {
                return View();
            }
        }

        public IActionResult UpdateSteamPreference(int userid, int preferenceid)
        {
            UserContainer userContainer = new UserContainer(new UserDAL());
            PreferenceContainer preferenceContainer = new PreferenceContainer(new PreferenceDAL());
            ViewModels viewModels = new ViewModels();
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            userid = int.TryParse(userIdClaim, out var id) ? id : 0;
            UserModel userModel = userContainer.FindById(userid);

            PreferenceModel preferenceModel = preferenceContainer.ShowPreferencePerUserId(userid);
            viewModels.preferenceViewModel = new PreferenceViewModel() { PreferenceID = preferenceModel.PreferenceID, SteamSelect = preferenceModel.SteamSelect, UserID = userModel.UserID };

            preferenceid = preferenceModel.PreferenceID;
            SteamPreferenceModel steamPreferenceModel = preferenceContainer.ShowSteamPreferencePerPreferenceId(preferenceid);
            viewModels.steamPreferenceViewModel = new SteamPreferenceViewModel() { SteamPreferenceID = steamPreferenceModel.SteamPreferenceID, GamesSelect = steamPreferenceModel.GamesSelect, PlatformUserNameSelect = steamPreferenceModel.PlatformUserNameSelect, PreferenceID = preferenceModel.PreferenceID };

            return View(viewModels);
        }
        //functie om een steamPreference te wijzigen
        [HttpPost]
        public IActionResult UpdateSteamPreference(SteamPreferenceModel steamPreferenceModel)
        {
            try
            {
                PreferenceContainer preferenceContainer = new PreferenceContainer(new PreferenceDAL());
                preferenceContainer.UpdateSteamPreference(steamPreferenceModel);

                return Ok();
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
