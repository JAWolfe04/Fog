using Microsoft.AspNetCore.Mvc;
using Fog.Models;
using DataLibrary.DataAccess;
using DataLibrary.Models;
using Microsoft.AspNetCore.Http;

namespace Fog.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            DataLibrary.Models.PlayerModel player = new DataLibrary.Models.PlayerModel();
            player.Username = login.Username;
            player.Password = login.Password;
            if (SQLDataAccess.VerifyPlayer(player))
            {
                HttpContext.Session.SetString("DisplayName", player.DisplayName);
                HttpContext.Session.SetString("Username", player.Username);
                HttpContext.Session.SetInt32("Permission", player.Permission);

                switch (player.Permission)
                {
                    case 0:
                        return RedirectToAction("Index", "Admin");
                    case 1:
                        return RedirectToAction("DevHome", "Home");
                    case 2:
                        return RedirectToAction("AdminHome", "Home");
                    default:
                        return RedirectToAction("Index", "Home");
                }
            }
            else
                return View();
        }

        public IActionResult CreatePlayer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePlayer(PlayerModel player)
        {
            if(SQLDataAccess.CreatePlayer(player))
            {
                HttpContext.Session.SetString("DisplayName", player.DisplayName);
                HttpContext.Session.SetString("Username", player.Username);
                HttpContext.Session.SetInt32("Permission", 0);

                return RedirectToAction("PlayerHome", "Home");
            }
            else
                return View();
        }

        public IActionResult RemovePlayer()
        {
            SQLDataAccess.DeletePlayer(HttpContext.Session.GetString("Username"));

            if (HttpContext.Session.GetInt32("Permission") == 2)
                return View();
            else
                return Logout();
        }

        public IActionResult CreateDev()
        {
            return View();
        }

        public IActionResult RemoveDev()
        {
            return View();
        }

        public IActionResult CreateAdmin()
        {
            return View();
        }

        public IActionResult RemoveAdmin()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}