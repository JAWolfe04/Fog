using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fog.Models
{
    public class EditGameDev
    {
        public int GameID { get; set; }
        public int DevID { get; set; }
        public List<DataLibrary.Models.DeveloperModel> devs { get; set; }
    }
}
