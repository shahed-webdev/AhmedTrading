using AhmedTrading.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AhmedTrading.Web.Controllers
{
    public class VendorAdvanceController : Controller
    {
        private readonly IUnitOfWork _db;

        public VendorAdvanceController(IUnitOfWork db)
        {
            _db = db;
        }

        public IActionResult AdvanceAdd(VendorAdvanceAddViewModel model)
        {
            _db.VendorAdvances.AddCustom(model);
            _db.SaveChanges();
            return View();
        }
        public IActionResult AdvanceAdd(int vendorId)
        {
            var model = _db.VendorAdvances.VendorWiseRecords(vendorId);
            return View(model);
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