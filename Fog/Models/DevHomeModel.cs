using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fog.Models
{
    public class DevHomeModel
    {
        public DataLibrary.Models.DeveloperModel DevInfo { get; set; }
        public IEnumerable<DataLibrary.Models.GameModel> games { get; set; }
    }
}
