using AhmedTrading.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AhmedTrading.Web.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IUnitOfWork _db;

        public PurchaseController(IUnitOfWork db)
        {
            _db = db;
        }

        public IActionResult Purchase()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Purchase(PurchaseViewModel model)
        {
            model.RegistrationId = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);

            if (!ModelState.IsValid) UnprocessableEntity(ModelState);

            var response = await _db.Purchases.AddCustomAsync(model, _db).ConfigureAwait(false);

            if (response.IsSuccess)
            {
                _db.Vendors.UpdatePaidDue(model.VendorId);
                await _db.SaveChangesAsync();
                return Ok(response);
            }
            else
                return UnprocessableEntity(response);
        }


        public async Task<IActionResult> PurchaseReceipt(int? id)
        {
            if (id == null) return RedirectToAction("Purchase");

            var model = await _db.Purchases.PurchaseReceiptAsync(id.GetValueOrDefault(), _db).ConfigureAwait(false);

            if (model == null) return RedirectToAction("Purchase");
            return View(model);
        }

        //purchase Records
        public IActionResult PurchaseRecords()
        {
            return View();
        }

        //request from datatable(ajax)
        public IActionResult PurchaseRecordsData(DataRequest request)
        {
            var data = _db.Purchases.Records(request);
            return Json(data);
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