using AhmedTrading.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AhmedTrading.Web.Controllers
{
    [Authorize(Roles = "admin, selling")]
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

            if (!response.IsSuccess) return UnprocessableEntity(response);
            _db.Customers.UpdatePaidDue(model.CustomerId);

            return Ok(response);
        }


        //Quick Selling
        public IActionResult QuickSelling()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> QuickSelling([FromBody] SellingViewModel model)
        {
            model.RegistrationId = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);

            if (!ModelState.IsValid) UnprocessableEntity(ModelState);

            var response = await _db.Selling.AddCustomAsync(model, _db).ConfigureAwait(false);

            if (!response.IsSuccess) return UnprocessableEntity(response);

            return Ok(response);
        }


        //Selling Receipt
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

        //Selling Record
        public IActionResult SellingRecords()
        {
            return View();
        }

        //Request from data-table(ajax)
        public IActionResult SellingRecordsData(DataRequest request)
        {
            var data = _db.Selling.Records(request);
            return Json(data);
        }

        //GET:// GetAmountByDate(ajax)
        public IActionResult GetAmountByDate(DateTime? fromDate, DateTime? toDate)
        {
            var model = _db.Selling.DateWiseSellingSummary(fromDate, toDate);
            return Json(model);
        }

        //delete receipt if not payment
        public IActionResult DeleteReceipt(int id)
        {
            var model = _db.Selling.ReceiptPaymentIsExist(id);
            return Json(model);
        }

        //delete receipt with payment
        public IActionResult ForceDeleteReceipt(int id)
        {
            var model = _db.Selling.DeleteReceipt(id, _db);
            return Json(model);
        }

        //GET: Due Collection
        public async Task<IActionResult> DueCollection(int? id)
        {
            if (id == null) return RedirectToAction("List", "Customer");

            var model = await _db.Selling.SellingReceiptAsync(id.GetValueOrDefault(), _db).ConfigureAwait(false);
            if (model == null) return RedirectToAction("List", "Customer");

            return View(model);
        }

        //customer due collection(ajax)
        [HttpPost]
        public async Task<IActionResult> DueCollection([FromBody] SellingDuePaySingleModel model)
        {
            model.RegistrationId = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var dbResponse = await _db.SellingPayments.DuePaySingleAsync(model, _db).ConfigureAwait(false);

            if (dbResponse.IsSuccess) return Ok();

            return BadRequest(dbResponse.Message);
        }


        //GET: multiple due collections
        public IActionResult DueCollectionMultiple(int? id)
        {
            if (id == null) return RedirectToAction("List", "Customer");

            var model = _db.Customers.SaleDueRecords(id.GetValueOrDefault());
            if (model == null) return RedirectToAction("List", "Customer");

            return View(model);
        }

        //POST: multiple due collections
        [HttpPost]
        public async Task<IActionResult> DueCollectionMultiple(SellingDuePayMultipleModel model)
        {
            model.RegistrationId = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var dbResponse = await _db.SellingPayments.DuePayMultipleAsync(model, _db).ConfigureAwait(false);

            if (dbResponse.IsSuccess) return Ok(dbResponse.Data);

            return BadRequest(dbResponse.Message);
        }

        //multiple receipt
        public async Task<IActionResult> SellingReceiptMultiple(int? id)
        {
            if (id == null) return RedirectToAction("List", "Customer");

            var response = await _db.SellingPayments.ReceiptAsync(id.GetValueOrDefault(),_db);

            return View(response);
        }


        //Product Selling Summary
        public IActionResult SellingSummary()
        {
            return View();
        }

        //GET:// Get product Selling Report(ajax)
        public IActionResult GetProductSellingSummary(DateTime? fromDate, DateTime? toDate)
        {
            var model = _db.Selling.DateWiseProductSellingSummary(fromDate, toDate);
            return Json(model);
        }


        //***** Selling Payment Summary *****
        public IActionResult PaymentSummary()
        {
            return View();
        }

        //GET:// Get product Selling Report(ajax)
        public IActionResult GetPaymentSummaryAmount(DateTime? fromDate, DateTime? toDate)
        {
            var model = _db.SellingPayments.DateWiseSalePayment(fromDate, toDate);
            return Json(model);
        }

        //Request from data-table(ajax)
        public IActionResult GetPaymentSummaryDataTable(DataRequest request)
        {
            var data = _db.SellingPayments.ReceiptDataTable(request);
            return Json(data);
        }

        //Delete Payment Summary
        public IActionResult DeletePaymentSummary(int id)
        {
            var response = _db.SellingPayments.DeleteReceipt(id,_db);
            return Json(response.IsSuccess);
        }
    }
}