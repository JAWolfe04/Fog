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

        public IActionResult EditDev(int DevID)
        {
            string Username = HttpContext.Session.GetString("Username");
            DeveloperModel dev = new DeveloperModel();
            DataLibrary.Models.DeveloperModel devData = DataLibrary.DataAccess.SQLDataAccess.GetDevInfoAccount(DevID, Username);
            dev.About = devData.About;
            dev.BankAccountNumber = devData.Account;
            dev.BankRoutingNumber = devData.Routing;
            dev.CompanyName = devData.Name;
            dev.Email = devData.Email;
            dev.ID = devData.ID;
            dev.Phone = devData.Phone;
            dev.Username = devData.Username;
            dev.WebLink = devData.Link;

            return View(dev);
        }

        [HttpPost]
        public IActionResult EditDev(DeveloperModel dev)
        {
            if (DataLibrary.DataAccess.SQLDataAccess.GetPlayerInfo(dev.Username).Username == null)
                ModelState.AddModelError("Username", "Username not found. Please try again.");

            if(ModelState.IsValid)
            {
                DataLibrary.Models.DeveloperModel devData = new DataLibrary.Models.DeveloperModel();
                devData.About = dev.About;
                devData.Account = dev.BankAccountNumber;
                devData.Routing = dev.BankRoutingNumber;
                devData.Name = dev.CompanyName;
                devData.Email = dev.Email;
                devData.ID = dev.ID;
                devData.Phone = dev.Phone;
                devData.Username = dev.Username;
                devData.Link = dev.WebLink;
                DataLibrary.DataAccess.SQLDataAccess.EditDev(devData);

                return RedirectToAction("DevInfo", "Developer", new { DevID = dev.ID });
            }

            return View(dev);
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