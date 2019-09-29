using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fog.Models;

namespace Fog.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        // GET: Login
        public ActionResult Index()
        {
            ModelState.Clear();
            return View();
        }

        // POST: Login/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                FogDBHandler fogDBHandle = new FogDBHandler();
                if(fogDBHandle.ValidateUser(loginModel))
                    ViewBag.Message = "Valid User";
                else
                    ViewBag.Message = "No username with password found";

                ModelState.Clear();
            }
            else
                ViewBag.Message = "Invalid State";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}