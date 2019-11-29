using System.Collections.Generic;
using DataLibrary.Models;

namespace DataLibrary.Models
{
    public class PlayerHomeModel
    {
        public DataLibrary.Models.PlayerModel PlayerInfo { get; set; }
        public IEnumerable<GameModel> PurchasedGames { get; set; }
        public IEnumerable<DataLibrary.Models.PlayerModel> Friends { get; set; }
        public IEnumerable<DataLibrary.Models.StreamModel> FollowedStreams { get; set; }
        public IEnumerable<GameModel> Wishlist { get; set; }

    }
}