﻿using AhmedTrading.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;

namespace AhmedTrading.Web.Controllers
{
    [Authorize(Roles = "admin, vendor")]
    public class VendorController : Controller
    {
        private readonly IUnitOfWork _db;

        public VendorController(IUnitOfWork db)
        {
            _db = db;
        }

        public IActionResult List()
        {
            return View();
        }

        //For Data-table
        public IActionResult VendorList(DataRequest request)
        {
            var list = _db.Vendors.ListDataTable(request);
            return Json(list);
        }

        public IActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(VendorViewModel model)
        {
            if (!ModelState.IsValid) return PartialView("_Create", model);

            var vendor = _db.Vendors.AddCustom(model);
            var task = await _db.SaveChangesAsync();

            if (task != 0)
            {
                model.VendorId = vendor.VendorId;
                var result = new AjaxContent<VendorViewModel> { Status = true, Data = model };
                return Json(result);
            }

            ModelState.AddModelError("", "Unable to insert record!");
            return PartialView("_Create", model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return BadRequest(HttpStatusCode.BadRequest);

            var model = _db.Vendors.FindCustom(id);
            if (model == null) return NotFound();

            return PartialView("_Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VendorViewModel model)
        {
            if (!ModelState.IsValid) return PartialView("_Edit", model);

            _db.Vendors.UpdateCustom(model);
            var task = await _db.SaveChangesAsync();

            if (task != 0) return Content("success");

            ModelState.AddModelError("", "Unable to update");
            Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return PartialView("_Edit", model);
        }

        public int Delete(int id)
        {
            if (!_db.Vendors.RemoveCustom(id)) return -1;
            return _db.SaveChanges();
        }

        public async Task<IActionResult> FindVendor(string prefix)
        {
            var data = await _db.Vendors.SearchAsync(prefix).ConfigureAwait(false);
            return Json(data);
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return RedirectToAction("List");

            var model = _db.Vendors.ProfileDetails(id.GetValueOrDefault(),_db);
            return View(model);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}