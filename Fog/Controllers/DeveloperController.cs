using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Fog.Models;

namespace Fog.Controllers
{
    public class DeveloperController : Controller
    {
        public IActionResult Home()
        {
            string Username = HttpContext.Session.GetString("Username");
            DevHomeModel DevHome = new DevHomeModel();

            DevHome.DevInfo = DataLibrary.DataAccess.SQLDataAccess.GetDevInfo(Username);
            DevHome.games = DataLibrary.DataAccess.SQLDataAccess.GetDevGames(DevHome.DevInfo.ID);

            return View(DevHome);
        }
    }
}