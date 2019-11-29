using System;

namespace DataLibrary.Models
{
    public class CompetitionModel
    {
        public int CompID { set; get; }
        public string Title { set; get; }
        public DateTime Date { set; get; }
        public string Description { set; get; }
        public string GameTitle { set; get; }
        public int GameID { set; get; }
    }
}
