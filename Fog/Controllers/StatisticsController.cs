using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Fog.Controllers
{
    public class StatisticsController : Controller
    {
        public IActionResult Statistics()
        {

            List<DataLibrary.Models.StreamModel> gameStats = new List<DataLibrary.Models.StreamModel>();
            List<Models.GenreStats> genreStats = new List<Models.GenreStats>();
            List<Models.DevStats> devStats = new List<Models.DevStats>();

            return View();
        }
    }
}