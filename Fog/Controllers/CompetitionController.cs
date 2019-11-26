using System;
using Microsoft.AspNetCore.Mvc;
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
            if (gameID > 0)
            {
                DataLibrary.Models.CompetitionModel compData = new DataLibrary.Models.CompetitionModel();
                compData.Title = newComp.Title;
                compData.Date = newComp.Date.GetValueOrDefault();
                compData.Description = newComp.Description;
                compData.GameID = gameID;
                DataLibrary.DataAccess.SQLDataAccess.CreateCompetition(compData);

                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ModelState.AddModelError("Game", "The game was not found. Please check the game and try again.");
                return View(newComp);
            }
        }
    }
}