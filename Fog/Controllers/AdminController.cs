using DataLibrary.DataAccess;
using Fog.Models;
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


        public IActionResult CreateCompetition()
        {
            CompetitionModel competition = new CompetitionModel();

            return View(competition);
        }

        [HttpPost]
        public IActionResult CreateCompetition(CompetitionModel newComp)
        {
            CompetitionModel competition = new CompetitionModel();
            TempData["Succes"] = null;
            TempData["Danger"] = null;


            if (SQLDataAccess.IsGameExist(newComp.Game))
            {
                if (SQLDataAccess.CreateCompetition(newComp))
                {
                    TempData["Success"] = "Added Successfully!";
                    return View(newComp);
                }
                else
                {
                    TempData["Danger"] = "Something went wrong. Please try again!";
                    return View();

                }
            }
            else
            {
                TempData["Danger"] = "The Game was not found, Please make sure you spell it right!";
                return View();

            }

        }


        public IActionResult CreateSale()
        {
            SaleModel sale = new SaleModel();
            return View(sale);            

        }

        [HttpPost]
        public IActionResult CreateSale(SaleModel sale)
        {

            TempData["Succes"] = null;
            TempData["Danger"] = null;


            if (SQLDataAccess.IsGameExist(sale.SaleGame))
            {
                if (SQLDataAccess.CreateSale(sale))
                {
                    TempData["Success"] = "Added Successfully!";
                    return View(sale);
                }
                else
                {
                    TempData["Danger"] = "Something went wrong. Please try again!";
                    return View();

                }
            }
            else
            {
                TempData["Danger"] = "The Game was not found, Please make sure you spell it right!";
                return View();

            }
            //return View(sale);
        }


        public IActionResult getAdminStats()
        {
            return View();
        }

    }
}