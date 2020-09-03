using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AhmedTrading.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AhmedTrading.Web.Controllers
{
    [Authorize(Roles = "admin, trade-sharing")]
    public class TradeSharingController : Controller
    {
        private readonly IUnitOfWork _db;
        public TradeSharingController(IUnitOfWork db)
        {
            _db = db;
        }

        //list of Trader
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexDataTable(DataRequest request)
        {
            var data = _db.Trader.ListDataTable(request);
            return Json(data);
        }


        //**Add Trader***
        public IActionResult AddTrader([FromBody] TraderModel model)
        {
            if (!ModelState.IsValid) return UnprocessableEntity("Model state invalid");

            var isPhone = _db.Trader.IsPhoneExist(model.Phone);
            if (isPhone) return UnprocessableEntity("Mobile number already exists");

            var response = _db.Trader.Add(model);

            return Ok(response);
        }

        //Get Trader
        public async Task<IActionResult> GetTrader(string name)
        {
            var response = await _db.Trader.SearchAsync(name);
            return Json(response);
        }

        //Delete parson
        public IActionResult DeleteTrader(int id)
        {
            var response = _db.Trader.Delete(id);
            return Json(response.IsSuccess);
        }

        //Details Trader
        public IActionResult Details(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            var response = _db.Trader.Details(id.GetValueOrDefault());
            return View(response.Data);
        }

        //Share Product
        public IActionResult ShareProduct(TraderSharingAddModel model)
        {
            if (!ModelState.IsValid) return UnprocessableEntity("Model state invalid");
            var response = _db.TraderSharing.Add(model);

            return Ok(response);
        }

        //Give Data Table
        public IActionResult GiveDataTable(DataRequest request)
        {
            var data = _db.TraderSharing.GivenDataTable(request);
            return Json(data);
        }
    }
}
