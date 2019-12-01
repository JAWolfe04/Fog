using System.Collections.Generic;

namespace DataLibrary.Models
{
    public class AdminStatsModel
    {
        public List<GenreStatModel> Genre { get; set; }
        public List<GameStatModel> Game { get; set; }
        public List<AdminDevStatsModel> Devs { get; set; }
    }
}
