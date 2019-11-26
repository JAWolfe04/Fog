using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLibrary.DataAccess;

namespace Fog.Controllers
{
    public class MarketplaceController : Controller
    {
        public IActionResult Games()
        {
            return View(SQLDataAccess.GetGames());
        }
        public IActionResult Players()
        {
            return View(SQLDataAccess.GetPlayers());
        }
        public IActionResult Competitions()
        {
            return View(SQLDataAccess.GetCompetitions());
        }
        public IActionResult Developers()
        {
            return View(SQLDataAccess.GetDevelopers());
        }
        public IActionResult Streams()
        {
            return View(SQLDataAccess.GetStreams());
        }
    }
}