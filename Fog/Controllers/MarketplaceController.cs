using System;
using Microsoft.AspNetCore.Mvc;
using Fog.Models;
using Microsoft.AspNetCore.Http;

namespace DataLibrary.Controllers
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


            PurchaseModel purchase = new PurchaseModel();
            purchase.Username = HttpContext.Session.GetString("Username");
            DataLibrary.Models.SaleModel sale = DataLibrary.DataAccess.SQLDataAccess.GetSale(GameID);
            DataLibrary.Models.GameModel game = DataLibrary.DataAccess.SQLDataAccess.GetGameInfo(GameID);
            purchase.GameID = GameID;
            purchase.GameTitle = game.Title;
            purchase.Price = decimal.Round((decimal)game.price, 2);
            if(sale.SaleDate.Date != DateTime.Today)
                sale.SalePercent = 0;
            purchase.Discount = purchase.Price * (sale.SalePercent / 100);
            purchase.SubTotal = purchase.Price - purchase.Discount;
            purchase.Tax = purchase.SubTotal * (decimal)0.1;
            purchase.Total = purchase.SubTotal + purchase.Tax;

            return View(purchase);
        }

        [HttpPost]
        public IActionResult PurchaseGame(PurchaseModel purchase)
        {
            if (HttpContext.Session.Get("Username") == null)
                return RedirectToAction("Index", "Login");

            purchase.Username = HttpContext.Session.GetString("Username");
            DataLibrary.Models.SaleModel sale = DataLibrary.DataAccess.SQLDataAccess.GetSale(purchase.GameID);
            DataLibrary.Models.GameModel game = DataLibrary.DataAccess.SQLDataAccess.GetGameInfo(purchase.GameID);
            purchase.GameTitle = game.Title;
            purchase.Price = decimal.Round((decimal)game.price, 2);
            if (sale.SaleDate.Date != DateTime.Today)
                sale.SalePercent = 0;
            purchase.Discount = purchase.Price * (sale.SalePercent / 100);
            purchase.SubTotal = purchase.Price - purchase.Discount;
            purchase.Tax = purchase.SubTotal * (decimal)0.1;
            purchase.Total = purchase.SubTotal + purchase.Tax;

            if (ModelState.IsValid)
            {
                DataLibrary.Models.PurchaseModel purchaseData = new DataLibrary.Models.PurchaseModel();
                purchaseData.PurchaseDate = DateTime.Today;
                purchaseData.Price = purchase.Total;
                purchaseData.CardNumber = purchase.CardNumber;
                purchaseData.CardName = purchase.CardName;
                purchaseData.CardExp = purchase.CardExp;
                purchaseData.CardSecurity = purchase.CardSecurity;
                purchaseData.Username = purchase.Username;
                purchaseData.GameID = purchase.GameID;
                purchaseData.SaleID = sale.SaleID;
                DataLibrary.DataAccess.SQLDataAccess.CreatePurchase(purchaseData);
                if (DataLibrary.DataAccess.SQLDataAccess.IsGameWishListed(purchase.Username, purchase.GameID))
                    DataLibrary.DataAccess.SQLDataAccess.RemoveWishlist(purchase.Username, purchase.GameID);
                return RedirectToAction("GameInfo", "Marketplace", new { GameID = purchase.GameID });
            }

            return View(purchase);
        }
    }
}