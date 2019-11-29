using System.Collections.Generic;

namespace Fog.Models
{
    public class GameInfoModel
    {
        public decimal finalPrice { get; set; }
        public bool IsDeveloper { get; set; }
        public bool IsGameOwned { get; set; }
        public bool IsGameWishlisted { get; set; }
        public DataLibrary.Models.GameModel game { get; set; }
        public DataLibrary.Models.SaleModel sale { get; set; }
        public IEnumerable<DataLibrary.Models.DeveloperModel> developers { get; set; }
        public IEnumerable<DataLibrary.Models.ForumModel> forums { get; set; }
    }
}
