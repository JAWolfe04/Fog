using Microsoft.AspNetCore.Mvc;
using DataLibrary.Models;

namespace DataLibrary.Controllers
{
    public class SaleController : Controller
    {
        public IActionResult CreateSale()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateSale(SaleModel sale)
        {
            int gameID = DataLibrary.DataAccess.SQLDataAccess.getGameID(sale.SaleGame);
            if (gameID > 0)
            {
                DataLibrary.Models.SaleModel saleData = new DataLibrary.Models.SaleModel();
                saleData.SaleDate = sale.SaleDate.GetValueOrDefault();
                saleData.SalePercent = sale.SalePercent.GetValueOrDefault();
                saleData.SaleGameID = gameID;
                DataLibrary.DataAccess.SQLDataAccess.CreateSale(saleData);
                return RedirectToAction("AdminHome", "Home");
            }
            else
            {
                ModelState.AddModelError("Game", "The game was not found. Please check the game and try again.");
                return View(sale);
            }
        }
    }
}