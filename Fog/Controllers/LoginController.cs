using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Fog.Models;

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
            if (DataLibrary.DataAccess.SQLDataAccess.VerifyPlayer(player))
            {
                HttpContext.Session.SetString("DisplayName", player.DisplayName);
                HttpContext.Session.SetString("Username", player.Username);
                HttpContext.Session.SetInt32("Permission", player.Permission);

                switch (player.Permission)
                {
                    case 0:
                        return RedirectToAction("PlayerHome", "Home");
                    case 1:
                        return RedirectToAction("DevHome", "Developer");
                    case 2:
                        return RedirectToAction("AdminHome", "Admin");
                    default:
                        return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("ModelOnly", "Username/Password combination was incorrect.");
                return View("Index", login);
            }
        }

        public IActionResult CreatePlayer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePlayer(PlayerModel player)
        {
            if(ModelState.IsValid)
            {
                if(DataLibrary.DataAccess.SQLDataAccess.GetPlayerInfo(player.Username).Username == player.Username)
                {
                    ModelState.AddModelError("Username", 
                        "The entered username already exists. Please enter a different username.");
                }
                else
                {
                    DataLibrary.Models.PlayerModel playerData = new DataLibrary.Models.PlayerModel();
                    playerData.DisplayName = player.DisplayName;
                    playerData.Email = player.Email;
                    playerData.Password = player.Password;
                    playerData.Username = player.Username;

                    DataLibrary.DataAccess.SQLDataAccess.CreatePlayer(playerData);
                    HttpContext.Session.SetString("DisplayName", player.DisplayName);
                    HttpContext.Session.SetString("Username", player.Username);
                    HttpContext.Session.SetInt32("Permission", 0);

                    return RedirectToAction("PlayerHome", "Home");
                }
            }

            return View(player);
        }

        public IActionResult EditPlayer()
        {
            PlayerModel player = new PlayerModel();
            DataLibrary.Models.PlayerModel playerData = DataLibrary.DataAccess.SQLDataAccess.GetPlayerInfo(
                HttpContext.Session.GetString("Username"));
            player.Username = playerData.Username;
            player.DisplayName = playerData.DisplayName;
            player.Password = playerData.Password;
            player.Email = playerData.Email;
            return View(player);
        }

        [HttpPost]
        public IActionResult EditPlayer(PlayerModel player)
        {
            DataLibrary.Models.PlayerModel playerData = new DataLibrary.Models.PlayerModel();
            playerData.Username = HttpContext.Session.GetString("Username");
            playerData.DisplayName = player.DisplayName;
            HttpContext.Session.SetString("DisplayName", player.DisplayName);
            playerData.Password = player.Password;
            playerData.Email = player.Email;
            DataLibrary.DataAccess.SQLDataAccess.EditPlayer(playerData);
            return RedirectToAction("PlayerHome","Home");
        }

        public IActionResult RemovePlayer()
        {
            DataLibrary.DataAccess.SQLDataAccess.DeletePlayer(HttpContext.Session.GetString("Username"));

            if (HttpContext.Session.GetInt32("Permission") == 2)
                return RedirectToAction("Players", "Marketplace");
            else
                return Logout();
        }

        public IActionResult CreateDev()
        {
            return View();

        }
        [HttpPost]
        public IActionResult CreateDev(DeveloperModel developer)
        {
            
            if (ModelState.IsValid)
            {
                if (DataLibrary.DataAccess.SQLDataAccess.GetPlayerInfo(developer.Username).Username == developer.Username)
                {
                    ModelState.AddModelError("Username",
                        "The entered username already exists. Please enter a different username.");
                }
                else
                {
                    DataLibrary.Models.DeveloperModel DeveloperData = new DataLibrary.Models.DeveloperModel();
                    DataLibrary.Models.PlayerModel playerData = new DataLibrary.Models.PlayerModel();
                    DeveloperData.About = developer.About;
                    DeveloperData.Account = developer.BankAccountNumber.ToString();
                    DeveloperData.Email = developer.Email;
                    DeveloperData.Link = developer.WebLink;
                    DeveloperData.Name = developer.CompanyName;
                    DeveloperData.Phone = developer.Phone;
                    DeveloperData.Routing = developer.BankRoutingNumber.ToString();
                    DeveloperData.Username = developer.Username;


                    DataLibrary.DataAccess.SQLDataAccess.CreateDeveloper(DeveloperData, developer.Password);
                    return RedirectToAction("AdminHome", "Admin");
                }
            }
            return View(developer);
        }

        public IActionResult RemoveDev()
        {
            if (HttpContext.Session.GetInt32("Permission") == 2)
                return RedirectToAction("Developers", "Marketplace");
            else
                return Logout();
        }

        public IActionResult CreateAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAdmin(PlayerModel player)
        {
            if (ModelState.IsValid)
            {
                if (DataLibrary.DataAccess.SQLDataAccess.GetPlayerInfo(player.Username).Username == player.Username)
                {
                    ModelState.AddModelError("Username",
                        "The entered username already exists. Please enter a different username.");
                }
                else
                {
                    DataLibrary.Models.PlayerModel playerData = new DataLibrary.Models.PlayerModel();
                    playerData.DisplayName = player.DisplayName;
                    playerData.Email = player.Email;
                    playerData.Password = player.Password;
                    playerData.Username = player.Username;
                    DataLibrary.DataAccess.SQLDataAccess.CreateAdmin(playerData);

                    return RedirectToAction("AdminHome", "Home");
                }
            }

            return View(player);
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