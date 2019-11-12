using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Fog.Models;
using DataLibrary.DataAccess;
using Microsoft.AspNetCore.Http;

namespace Fog.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PlayerHome()
        {
            PlayerHomeModel homeModel = new PlayerHomeModel();
            string Username = HttpContext.Session.GetString("Username");
            homeModel.PurchasedGames = SQLDataAccess.GetPurchasedGames(Username);
            homeModel.Friends = SQLDataAccess.GetFriends(Username);
            homeModel.FollowedStreams = SQLDataAccess.GetFollowedStreams(Username);
            homeModel.Wishlist = SQLDataAccess.GetWishlist(Username);
            return View(homeModel);
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
