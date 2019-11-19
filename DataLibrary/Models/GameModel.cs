using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class GameModel
    {
        public int GameID { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public double price { get; set; }
        public string Genre { get; set; }
    }
}
