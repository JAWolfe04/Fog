using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fog.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            string Username = HttpContext.Session.GetString("Username");

            return View();
        }



    }
}