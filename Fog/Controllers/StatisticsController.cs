using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DataLibrary.Controllers
{
    public class StatisticsController : Controller
    {
        public IActionResult Statistics()
        {

            List<Models.StreamModel> gameStats = new List<Models.StreamModel>();
            List<Models.GenreStats> genreStats = new List<Models.GenreStats>();
            List<Models.DevStats> devStats = new List<Models.DevStats>();

            return View();
        }
    }
}