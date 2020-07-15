using AhmedTrading.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AhmedTrading.Web.Controllers
{
    public class SellingController : Controller
    {
        private readonly IUnitOfWork _db;

        public SellingController(IUnitOfWork db)
        {
            _db = db;
        }

        public IActionResult Selling()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Selling([FromBody] SellingViewModel model)
        {
            model.RegistrationId = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);

            if (!ModelState.IsValid) UnprocessableEntity(ModelState);

            var response = await _db.Selling.AddCustomAsync(model, _db).ConfigureAwait(false);

            if (response.IsSuccess)
            {
                _db.Customers.UpdatePaidDue(model.CustomerId);
                await _db.SaveChangesAsync();
                return Ok(response);
            }
            else
                return UnprocessableEntity(response);
        }

        //Selling receipt
        public async Task<IActionResult> SellingReceipt(int? id)
        {
            if (id == null) return RedirectToAction("Selling");

            var model = await _db.Selling.SellingReceiptAsync(id.GetValueOrDefault(), _db).ConfigureAwait(false);
            if (model == null) return RedirectToAction("Selling");

            return View(model);
        }

        public async Task<IActionResult> FindCustomers(string prefix)
        {
            var data = await _db.Customers.SearchAsync(prefix).ConfigureAwait(false);
            return Json(data);
        }

        //selling record
        public IActionResult SellingRecords()
        {
            return View();
        }

        //request from data-table(ajax)
        public IActionResult SellingRecordsData(DataRequest request)
        {
            var data = _db.Selling.Records(request);
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