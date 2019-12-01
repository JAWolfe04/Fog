using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Fog.Models;

namespace Fog.Controllers
{
    public class CompetitionController : Controller
    {
        public IActionResult CreateCompetition()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCompetition(CompetitionModel newComp)
        {
            int gameID = DataLibrary.DataAccess.SQLDataAccess.getGameID(newComp.GameTitle);

            if (gameID < 0)
                ModelState.AddModelError("Game", "The game was not found. Please check the game and try again.");

            if(ModelState.IsValid)
            {
                DataLibrary.Models.CompetitionModel compData = new DataLibrary.Models.CompetitionModel();
                compData.Title = newComp.Title;
                compData.Date = newComp.Date.GetValueOrDefault();
                compData.Description = newComp.Description;
                compData.GameID = gameID;
                DataLibrary.DataAccess.SQLDataAccess.CreateCompetition(compData);

                return RedirectToAction("Index", "Admin");
            }
            
            return View(newComp);
        }

        public IActionResult CompetitionInfo(int CompID)
        {
            CompetitionModel comp = new CompetitionModel();
            DataLibrary.Models.CompetitionModel compData = 
                DataLibrary.DataAccess.SQLDataAccess.GetCompetition(CompID);
            comp.strDate = compData.Date.ToString("MM/dd/yyyy");
            comp.Description = compData.Description;
            comp.GameID = compData.GameID;
            comp.CompID = compData.CompID;
            comp.GameTitle = compData.GameTitle;
            comp.IsEntered = false;
            comp.Title = compData.Title;

            if (HttpContext.Session.Get("Username") != null)
            {
                string Username = HttpContext.Session.GetString("Username");
                comp.IsEntered = DataLibrary.DataAccess.SQLDataAccess.EnteredComp(CompID, Username);
            }

            comp.EnteredPlayers = DataLibrary.DataAccess.SQLDataAccess.GetEnteredPlayers(CompID);

            return View(comp);
        }

        public IActionResult EnterCompetition(int CompID)
        {
            if (HttpContext.Session.Get("Username") == null)
                return RedirectToAction("Index", "Login");

            string Username = HttpContext.Session.GetString("Username");
            DataLibrary.DataAccess.SQLDataAccess.EnterCompetition(CompID, Username);

            return RedirectToAction("CompetitionInfo", new { CompID = CompID });
        }

        public IActionResult LeaveCompetition(int CompID)
        {
            string Username = HttpContext.Session.GetString("Username");
            DataLibrary.DataAccess.SQLDataAccess.LeaveCompetition(CompID, Username);

            return RedirectToAction("CompetitionInfo", new { CompID = CompID });
        }

        public IActionResult EditCompetition(int CompID)
        {
            CompetitionModel comp = new CompetitionModel();
            DataLibrary.Models.CompetitionModel compData =
                DataLibrary.DataAccess.SQLDataAccess.GetCompetition(CompID);
            comp.Date = compData.Date;
            comp.Description = compData.Description;
            comp.GameID = compData.GameID;
            comp.CompID = compData.CompID;
            comp.GameTitle = compData.GameTitle;
            comp.Title = compData.Title;

            return View(comp);
        }

        [HttpPost]
        public IActionResult EditCompetition(CompetitionModel comp)
        {
            int GameID = DataLibrary.DataAccess.SQLDataAccess.getGameID(comp.GameTitle);
            if (GameID == 0)
                ModelState.AddModelError("GameTitle", "Game does not exist. Check name and try again.");

            if(ModelState.IsValid)
            {
                DataLibrary.Models.CompetitionModel compData = new DataLibrary.Models.CompetitionModel();
                compData.CompID = comp.CompID;
                compData.Date = comp.Date.GetValueOrDefault();
                compData.Description = comp.Description;
                compData.GameID = GameID;
                compData.Title = comp.Title;
                DataLibrary.DataAccess.SQLDataAccess.EditCompetition(compData);

                return RedirectToAction("CompetitionInfo", new { CompID = comp.CompID });
            }

            return View(comp);
        }

        [HttpPost]
        public IActionResult RemoveCompetition(int CompID)
        {
            string Username = HttpContext.Session.GetString("Username");
            DataLibrary.DataAccess.SQLDataAccess.RemoveCompetition(CompID);

            return RedirectToAction("Competitions", "Marketplace");
        }
    }
}