using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                    if (developer.Username.ToLower() == Username.ToLower())
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
            GameModel game = new GameModel();
            DataLibrary.Models.GameModel gameData = DataLibrary.DataAccess.SQLDataAccess.GetGameInfo(GameID);
            game.Title = gameData.Title;
            game.price = gameData.price;
            game.Genre = gameData.Genre;
            game.GameID = GameID;
            game.Desc = gameData.Desc;

            return View(game);
        }

        [HttpPost]
        public IActionResult EditGame(GameModel game)
        {
            if (ModelState.IsValid)
            {
                DataLibrary.Models.GameModel gameData = new DataLibrary.Models.GameModel();
                gameData.GameID = game.GameID;
                gameData.Title = game.Title;
                gameData.Desc = game.Desc;
                gameData.Genre = game.Genre;
                gameData.price = game.price;
                DataLibrary.DataAccess.SQLDataAccess.EditGame(gameData);

                return RedirectToAction("GameInfo", new { GameID = game.GameID });
            }

            return View(game);
        }

        public IActionResult CreateGame()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateGame(GameModel gameModel)
        {
            if(ModelState.IsValid)
            {
                DataLibrary.Models.GameModel gameData = new DataLibrary.Models.GameModel();
                gameData.Title = gameModel.Title;
                gameData.Desc = gameModel.Desc;
                gameData.price = gameModel.price;
                gameData.Genre = gameModel.Genre;
                int gameID = DataLibrary.DataAccess.SQLDataAccess.CreateGame(gameData);

                string Username = HttpContext.Session.GetString("Username");
                int devID = DataLibrary.DataAccess.SQLDataAccess.GetDevInfo(Username).ID;
                DataLibrary.DataAccess.SQLDataAccess.AddGameDev(devID, gameID);

                return RedirectToAction("GameInfo", new { GameID = gameID });
            }

            return View(gameModel);
        }

        public IActionResult AddForum(int GameID)
        {
            ForumModel forum = new ForumModel();
            forum.GameID = GameID;

            return View(forum);
        }

        [HttpPost]
        public IActionResult AddForum(ForumModel forum)
        {
            if (ModelState.IsValid)
            {
                DataLibrary.Models.ForumModel forumData = new DataLibrary.Models.ForumModel();
                forumData.Name = forum.Name;
                forumData.Link = forum.Link;
                forumData.GameID = forum.GameID;
                DataLibrary.DataAccess.SQLDataAccess.AddGameForum(forumData);

                return RedirectToAction("GameInfo", new { GameID = forum.GameID });
            }

            return View(forum);
        }

        public IActionResult RemoveForum(int GameID)
        {
            RemoveForumModel removeForum = new RemoveForumModel();
            removeForum.GameID = GameID;
            removeForum.forums = DataLibrary.DataAccess.SQLDataAccess.GetGameForums(GameID);
            removeForum.forumList = new List<SelectListItem>();
            for(int i = 0; i < removeForum.forums.Count; ++i)
            {
                removeForum.forumList.Add(new SelectListItem { Value = i.ToString(), Text = removeForum.forums[i].Name });
            }

            return View(removeForum);
        }

        [HttpPost]
        public IActionResult RemoveForum(RemoveForumModel removeForum)
        {
            if(ModelState.IsValid)
            {
                removeForum.forums = DataLibrary.DataAccess.SQLDataAccess.GetGameForums(removeForum.GameID);
                DataLibrary.Models.ForumModel forum = removeForum.forums[removeForum.position];
                forum.GameID = removeForum.GameID;
                DataLibrary.DataAccess.SQLDataAccess.RemoveGameForum(forum);

                return RedirectToAction("GameInfo", new { GameID = forum.GameID });
            }

            return View(removeForum);
        }

        public IActionResult AddGameDev(int GameID)
        {
            EditGameDev editDev = new EditGameDev();
            editDev.GameID = GameID;
            editDev.devs = DataLibrary.DataAccess.SQLDataAccess.GetDevelopers();

            return View(editDev);
        }

        [HttpPost]
        public IActionResult AddGameDev(EditGameDev editDev)
        {
            DataLibrary.DataAccess.SQLDataAccess.AddGameDev(editDev.DevID, editDev.GameID);
            return RedirectToAction("GameInfo", new { GameID = editDev.GameID });
        }

        public IActionResult RemoveGameDev(int GameID)
        {
            EditGameDev editDev = new EditGameDev();
            editDev.GameID = GameID;
            editDev.devs = DataLibrary.DataAccess.SQLDataAccess.GetGameDevelopers(GameID);

            return View(editDev);
        }

        [HttpPost]
        public IActionResult RemoveGameDev(EditGameDev editDev)
        {
            DataLibrary.DataAccess.SQLDataAccess.RemoveGameDev(editDev.DevID, editDev.GameID);
            return RedirectToAction("GameInfo", new { GameID = editDev.GameID });
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