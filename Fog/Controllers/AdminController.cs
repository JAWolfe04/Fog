using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Fog.Models;

namespace Fog.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminHome()
        {
            return View();
        }

        public IActionResult EditAdmin()
        {
            string Username = HttpContext.Session.GetString("Username");
            DataLibrary.Models.PlayerModel adminData = DataLibrary.DataAccess.SQLDataAccess.GetPlayerInfo(Username);
            PlayerModel admin = new PlayerModel();
            admin.Username = adminData.Username;
            admin.Email = adminData.Email;
            admin.DisplayName = adminData.DisplayName;

            return View(admin);
        }

        [HttpPost]
        public IActionResult EditAdmin(PlayerModel admin)
        {
            if(ModelState.IsValid)
            {
                DataLibrary.Models.PlayerModel adminData = new DataLibrary.Models.PlayerModel();
                adminData.Username = admin.Username;
                adminData.Password = admin.Password;
                adminData.Email = admin.Email;
                adminData.DisplayName = admin.DisplayName;

                DataLibrary.DataAccess.SQLDataAccess.EditPlayer(adminData);

                return RedirectToAction("AdminHome", "Admin");
            }

            return View(admin);
        }

        public IActionResult AdminStats()
        {
            DataLibrary.Models.AdminStatsModel adminStats = new DataLibrary.Models.AdminStatsModel();
            adminStats.Genre = DataLibrary.DataAccess.SQLDataAccess.GetAdminGenreStats();
            adminStats.Game = DataLibrary.DataAccess.SQLDataAccess.GetAdminGameStats();
            adminStats.Devs = DataLibrary.DataAccess.SQLDataAccess.GetAdminDevStats();

            return View(adminStats);
        }
    }
}