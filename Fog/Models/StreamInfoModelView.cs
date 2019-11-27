using System.Collections.Generic;

namespace Fog.Models
{
    public class StreamInfoModelView
    {
        public bool IsHost { get; set; }

        public bool IsFollower { get; set; }
        public DataLibrary.Models.StreamModel stream { get; set; }

        public DataLibrary.Models.GameModel game { get; set; }
        public List<DataLibrary.Models.PlayerModel> players { get; set; }
    }
}
