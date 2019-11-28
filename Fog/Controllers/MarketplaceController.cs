using System;
using Microsoft.AspNetCore.Mvc;
using Fog.Models;
using Microsoft.AspNetCore.Http;

namespace Fog.Controllers
{
    public class MarketplaceController : Controller
    {
        public IActionResult Games()
        {
            return View(DataLibrary.DataAccess.SQLDataAccess.GetGames());
        }
        public IActionResult Players()
        {
            return View(DataLibrary.DataAccess.SQLDataAccess.GetPlayers());
        }
        public IActionResult Competitions()
        {
            return View(DataLibrary.DataAccess.SQLDataAccess.GetCompetitions());
        }
        public IActionResult Developers()
        {
            return View(DataLibrary.DataAccess.SQLDataAccess.GetDevelopers());
        }
        public IActionResult Streams()
        {
            return View(DataLibrary.DataAccess.SQLDataAccess.GetStreams());
        }

        public IActionResult GameInfo(int GameID)
        {
            GameInfoModel gameInfo = new GameInfoModel();
            gameInfo.game = DataLibrary.DataAccess.SQLDataAccess.GetGameInfo(GameID);
            gameInfo.sale = DataLibrary.DataAccess.SQLDataAccess.GetSale(GameID);
            gameInfo.developers = DataLibrary.DataAccess.SQLDataAccess.GetGameDevelopers(GameID);
            gameInfo.forums = DataLibrary.DataAccess.SQLDataAccess.GetGameForums(GameID);
            gameInfo.IsDeveloper = false;
            gameInfo.IsGameOwned = false;
            gameInfo.IsGameWishlisted = false;
            if(HttpContext.Session.Get("Username") != null)
            {
                string Username = HttpContext.Session.GetString("Username");

                gameInfo.IsGameOwned = DataLibrary.DataAccess.
                    SQLDataAccess.IsGameOwned(Username, GameID);

                gameInfo.IsGameWishlisted = DataLibrary.DataAccess.
                    SQLDataAccess.IsGameWishListed(Username, GameID);

                foreach (var developer in gameInfo.developers)
                {
                    if (developer.Username == Username)
                    {
                        gameInfo.IsDeveloper = true;
                        break;
                    }
                }
            }
            if (gameInfo.sale.SaleDate.Date != DateTime.Today)
                gameInfo.sale.SalePercent = 0;

            decimal percentRed = (100 - gameInfo.sale.SalePercent) / (decimal)100;
            gameInfo.finalPrice = (decimal)gameInfo.game.price * percentRed;
            gameInfo.finalPrice = decimal.Round(gameInfo.finalPrice, 2);
            return View(gameInfo);
        }

        public IActionResult EditGame(int GameID)
        {
            return RedirectToAction("GameInfo", "Marketplace", new { GameID = GameID });
        }

        public IActionResult RemoveGame(int GameID)
        {
            DataLibrary.DataAccess.SQLDataAccess.RemoveGame(GameID);
            return RedirectToAction("Games", "Marketplace");
        }

        public IActionResult AddWishlist(int GameID)
        {
            if (HttpContext.Session.Get("Username") == null)
                return RedirectToAction("Index", "Login");

            string Username = HttpContext.Session.GetString("Username");
            DataLibrary.DataAccess.SQLDataAccess.AddWishlist(Username, GameID);
            return RedirectToAction("GameInfo", "Marketplace", new { GameID = GameID });
        }

        public IActionResult RemoveWishlist(int GameID)
        {
            string Username = HttpContext.Session.GetString("Username");
            DataLibrary.DataAccess.SQLDataAccess.RemoveWishlist(Username, GameID);
            return RedirectToAction("GameInfo", "Marketplace", new { GameID = GameID });
        }

        public IActionResult PurchaseGame(int GameID)
        {
            if (HttpContext.Session.Get("Username") == null)
                return RedirectToAction("Index", "Login");

            return RedirectToAction("PurchaseGame", "Marketplace", new { GameID = GameID });
        }
    }
}