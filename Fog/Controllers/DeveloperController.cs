using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Fog.Models;

namespace Fog.Controllers
{
    public class DeveloperController : Controller
    {
        public IActionResult DevHome()
        {
            string Username = HttpContext.Session.GetString("Username");
            DevHomeModel DevHome = new DevHomeModel();

            DevHome.DevInfo = DataLibrary.DataAccess.SQLDataAccess.GetDevInfo(Username);
            DevHome.games = DataLibrary.DataAccess.SQLDataAccess.GetDevGames(DevHome.DevInfo.ID);

            return View(DevHome);
        }

        public IActionResult DevInfo(int DevID)
        {
            DevHomeModel DevInfo = new DevHomeModel();

            DevInfo.DevInfo = DataLibrary.DataAccess.SQLDataAccess.GetDevInfo(DevID);
            DevInfo.games = DataLibrary.DataAccess.SQLDataAccess.GetDevGames(DevID);

            return View(DevInfo);
        }

        public IActionResult DevStats(int DevID)
        {
            DataLibrary.Models.DevStatsModel devStats = new DataLibrary.Models.DevStatsModel();
            devStats.gameStats = DataLibrary.DataAccess.SQLDataAccess.GetDevGameStats(DevID);
            devStats.genreStats = DataLibrary.DataAccess.SQLDataAccess.GetDevGenreStats(DevID);

            return View(devStats);
        }
    }
}