using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLibrary.DataAccess;

namespace DataLibrary.Controllers
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
            return View();
        }
        public IActionResult Developers()
        {
            return View();
        }
        public IActionResult Streams()
        {
            return View(SQLDataAccess.GetStreams());
        }
    }
}