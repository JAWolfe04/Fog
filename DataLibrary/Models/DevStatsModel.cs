using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class DevStatsModel
    {
        public IEnumerable<GameStatModel> gameStats { get; set; }
        public IEnumerable<GenreStatModel> genreStats { get; set; }
    }
}
