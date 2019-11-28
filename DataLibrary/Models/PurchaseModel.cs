using System;

namespace DataLibrary.Models
{
    public class PurchaseModel
    {
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string CardExp { get; set; }
        public string CardSecurity { get; set; }
        public string Username { get; set; }
        public int SaleID { get; set; }
        public int GameID { get; set; }
    }
}
