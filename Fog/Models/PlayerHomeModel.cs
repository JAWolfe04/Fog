using System.Collections.Generic;
using DataLibrary.Models;

namespace Fog.Models
{
    public class PlayerHomeModel
    {
        public List<GameModel> PurchasedGames { get; set; }
        public List<PlayerModel> Friends { get; set; }
        public List<StreamModel> FollowedStreams { get; set; }
        public List<GameModel> Wishlist { get; set; }
    }
}
