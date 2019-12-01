using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Fog.Models;

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
            homeModel.PurchasedGames = DataLibrary.DataAccess.SQLDataAccess.GetPurchasedGames(Username);
            homeModel.Friends = DataLibrary.DataAccess.SQLDataAccess.GetFriends(Username);
            homeModel.FollowedStreams = DataLibrary.DataAccess.SQLDataAccess.GetFollowedStreams(Username);
            homeModel.Wishlist = DataLibrary.DataAccess.SQLDataAccess.GetWishlist(Username);
            homeModel.PlayerInfo = DataLibrary.DataAccess.SQLDataAccess.GetPlayerInfo(Username);
            return View(homeModel);
        }

        public IActionResult PlayerInfo(string playerUsername)
        {
            string Username = HttpContext.Session.GetString("Username");
            PlayerInfoModel playerInfo = new PlayerInfoModel();
            playerInfo.IsFriend = DataLibrary.DataAccess.SQLDataAccess.IsFriend(Username, playerUsername);
            playerInfo.PlayerInfo = DataLibrary.DataAccess.SQLDataAccess.GetPlayerInfo(playerUsername);
            playerInfo.PurchasedGames = DataLibrary.DataAccess.SQLDataAccess.GetPurchasedGames(playerUsername);
            playerInfo.Friends = DataLibrary.DataAccess.SQLDataAccess.GetFriends(playerUsername);
            playerInfo.FollowedStreams = DataLibrary.DataAccess.SQLDataAccess.GetFollowedStreams(playerUsername);
            playerInfo.Wishlist = DataLibrary.DataAccess.SQLDataAccess.GetWishlist(playerUsername);

            return View(playerInfo);
        }

        public IActionResult FriendPlayer(string Username)
        {
            string PlayerUsername = HttpContext.Session.GetString("Username");
            DataLibrary.DataAccess.SQLDataAccess.AddFriend(PlayerUsername, Username);
            return RedirectToAction("PlayerInfo", "Home", new { playerUsername = Username });
        }

        public IActionResult UnfriendPlayer(string Username)
        {
            string PlayerUsername = HttpContext.Session.GetString("Username");
            DataLibrary.DataAccess.SQLDataAccess.RemoveFriend(PlayerUsername, Username);
            return RedirectToAction("PlayerInfo", "Home", new { playerUsername = Username });
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
