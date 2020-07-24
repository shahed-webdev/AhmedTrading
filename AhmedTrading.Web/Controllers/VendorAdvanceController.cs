using System.Threading.Tasks;
using AhmedTrading.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AhmedTrading.Web.Controllers
{
    [Authorize]
    public class VendorAdvanceController : Controller
    {
        private readonly IUnitOfWork _db;

        public VendorAdvanceController(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task<IActionResult> List(int? id)
        {
            if (id == null) return RedirectToAction("List", "Vendor");

            var model = await _db.VendorAdvances.VendorWiseRecords(id.GetValueOrDefault());
            ViewBag.VendorInfo = _db.Vendors.FindCustom(id);

            return View(model);
        }

        public IActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(VendorAdvanceAddViewModel model)
        {
            if (!ModelState.IsValid) return PartialView("_Create", model);

            _db.VendorAdvances.AddCustom(model);
            _db.SaveChanges();

            var result = new AjaxContent<VendorAdvanceAddViewModel> { Status = true, Data = model };
            return Json(result);
        }

        public int Delete(int id)
        {
            _db.VendorAdvances.RemoveCustom(id);
            return _db.SaveChanges();
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