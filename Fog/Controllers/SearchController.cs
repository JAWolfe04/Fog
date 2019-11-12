using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fog.Models;

namespace Fog.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            List<PlayerModel> Players = new List<PlayerModel>();
            PlayerModel player = new PlayerModel();
            player.Displayed_name = "ahmed";
            player.Email = "ahmed@mail.com";
            player.Login_attempt = 2;
            player.Password = "123456";
            player.Permission = 0;
            player.StreamID = 1;
            player.Username = "ahmed";

            Players.Add(player);

            return View(Players);
        }
    }
}