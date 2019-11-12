using System.Collections.Generic;
using DataLibrary.Models;

namespace Fog.Models
{
    public class PlayerHomeModel
    {
        public IEnumerable<GameModel> PurchasedGames { get; set; }
        public IEnumerable<PlayerModel> Friends { get; set; }
        public IEnumerable<StreamModel> FollowedStreams { get; set; }
        public IEnumerable<GameModel> Wishlist { get; set; }
    }
}
