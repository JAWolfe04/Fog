using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Models;


namespace Fog.Models
{
    public class StatisticViewModel
    {
        public List<GenreStatModel> Genre { get; set; }
        public List<GameStatModel> Game { get; set; }
        public List<DeveloperStatisticModel> Dev { get; set; }
    }
}
