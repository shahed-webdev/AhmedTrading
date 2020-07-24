using AhmedTrading.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AhmedTrading.Web.Controllers
{
    [Authorize]
    public class VendorCommissionController : Controller
    {
        private readonly IUnitOfWork _db;

        public VendorCommissionController(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task<IActionResult> List(int? id)
        {
            if (id == null) return RedirectToAction("List", "Vendor");
            ViewBag.VendorInfo = _db.Vendors.FindCustom(id);

            var model = await _db.VendorCommissions.ListAsync(id.GetValueOrDefault()).ConfigureAwait(false);
            return View(model);
        }

        public IActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(VendorCommissionAddModel model)
        {
            if (!ModelState.IsValid) return PartialView("_Create", model);

            try
            {
                _db.VendorCommissions.AddCustom(model);
                _db.SaveChanges();

                var result = new AjaxContent<VendorCommissionAddModel> { Status = true, Data = model };
                return Json(result);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Commission", e.Message);
                return PartialView("_Create", model);
            }
        }

        public int Remove(int id)
        {
            try
            {
                _db.VendorCommissions.RemoveCustom(id);
                _db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
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