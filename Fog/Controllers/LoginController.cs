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
            if (SQLDataAccess.verifyPlayer(player))
            {
                HttpContext.Session.SetString("DisplayName", player.DisplayName);
                HttpContext.Session.SetString("Username", player.Username);
                HttpContext.Session.SetInt32("Permission", player.Permission);

                if (player.Permission == 0)
                    return RedirectToAction("PlayerHome", "Home");
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Create()
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