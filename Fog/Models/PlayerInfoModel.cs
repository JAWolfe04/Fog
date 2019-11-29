using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fog.Models
{
    public class PlayerInfoModel
    {
        public bool IsFriend { get; set; }
        public DataLibrary.Models.PlayerModel PlayerInfo { get; set; }
        public IEnumerable<DataLibrary.Models.GameModel> PurchasedGames { get; set; }
        public IEnumerable<DataLibrary.Models.PlayerModel> Friends { get; set; }
        public IEnumerable<DataLibrary.Models.StreamModel> FollowedStreams { get; set; }
        public IEnumerable<DataLibrary.Models.GameModel> Wishlist { get; set; }
    }
}
