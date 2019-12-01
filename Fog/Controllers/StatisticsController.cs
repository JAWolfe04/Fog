using System.Collections.Generic;
using Fog.Models;
using Microsoft.AspNetCore.Mvc;
using DataLibrary.DataAccess;


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

        public IActionResult AdminStatistics()
        {

                StatisticViewModel vm = new StatisticViewModel();
                vm.Genre = SQLDataAccess.GetGenreStatistics();
                vm.Game = SQLDataAccess.GetGameStatistics();
                vm.Dev = SQLDataAccess.GetDevStatistics();

            return View(vm);
        }
    }
}