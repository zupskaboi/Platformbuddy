using BusinessLayer;
using DAL;
using InterfaceLayer;
using Microsoft.AspNetCore.Mvc;
using Platformbuddy.Models;
using System.Diagnostics;

namespace Platformbuddy.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //GamePlatform gamePlatform = new GamePlatform();
            //List<GamePlatform> ApiData = gamePlatform.SetApiData();
            //return View(ApiData);
            //UserModel userModel = TempData["userData"] as UserModel;
            //return View(userModel);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}