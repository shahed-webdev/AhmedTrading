using AhmedTrading.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AhmedTrading.Web.Controllers
{
    public class VendorCommissionController : Controller
    {
        private readonly IUnitOfWork _db;

        public VendorCommissionController(IUnitOfWork db)
        {
            _db = db;
        }

        public IActionResult Add(VendorCommissionAddModel model)
        {
            try
            {
                _db.VendorCommissions.AddCustom(model);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Commission", e.Message);
            }

            return View();
        }

        public async Task<IActionResult> List(int vendorId)
        {
            var model = await _db.VendorCommissions.ListAsync(vendorId).ConfigureAwait(false);
            return View(model);
        }
        public IActionResult Remove(int Id)
        {
            try
            {
                _db.VendorCommissions.RemoveCustom(Id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View();
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