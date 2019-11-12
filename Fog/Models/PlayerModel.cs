using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fog.Models
{
    public class PlayerModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Displayed_name { get; set; }
        public int Permission { get; set; }
        public int Login_attempt { get; set; }
        public int StreamID { get; set; }
    }
}
