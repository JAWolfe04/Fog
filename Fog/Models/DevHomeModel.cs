using System.Collections.Generic;

namespace Fog.Models
{
    public class DevHomeModel
    {
        public DataLibrary.Models.DeveloperModel DevInfo { get; set; }
        public IEnumerable<DataLibrary.Models.GameModel> games { get; set; }
    }
}
