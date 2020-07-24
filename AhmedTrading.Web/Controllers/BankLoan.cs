using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AhmedTrading.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AhmedTrading.Web.Controllers
{
    [Authorize(Roles = "admin, bank-loan")]
    public class BankLoanController : Controller
    {
        private readonly IUnitOfWork _db;

        public BankLoanController(IUnitOfWork db)
        {
            _db = db;
        }

        /***BANK LOAN**/
        public IActionResult Loan()
        {
            ViewBag.bankName = new SelectList(_db.BankAccounts.Ddl(), "value", "label");
            return View();
        }

        //request from data-table(ajax)
        public IActionResult LoanData(DataRequest request)
        {
            var data = _db.BankLoans.RecordsDataTable(request);
            return Json(data);
        }

        [HttpPost]
        public IActionResult AddLoan([FromBody] BankLoanModel model)
        {
            if (!ModelState.IsValid) return UnprocessableEntity("Invalid Model State");

            var response = _db.BankLoans.AddLoan(model);
            if (response.IsSuccess) return Ok(response.IsSuccess);

            return UnprocessableEntity(response.Message);
        }

        public IActionResult DeleteLoan(int id)
        {
            var response = _db.BankLoans.DeleteLoan(id);
            return Ok(response.IsSuccess);
        }

        /***BANK LOAN RETURN**/
        public IActionResult LoanReturn(int? id)
        {
            if (id == null) return RedirectToAction("Loan");

            var model = _db.BankLoans.FindLoan(id.GetValueOrDefault()).Data;
            return View(model);
        }

        //request from data-table(ajax)
        public IActionResult LoanReturnData(DataRequest request)
        {
            var data = _db.BankLoans.ReturnRecordsDataTable(request);
            return Json(data);
        }

        [HttpPost]
        public IActionResult AddLoanReturn([FromBody] BankLoanReturnModel model)
        {
            if (!ModelState.IsValid) return UnprocessableEntity("Invalid Model State");

            var response = _db.BankLoans.LoanReturn(model);
            if (response.IsSuccess) return Ok(response.IsSuccess);

            return UnprocessableEntity(response.Message);
        }

        public IActionResult DeleteLoanReturn(int id)
        {
            var response = _db.BankLoans.DeleteLoanReturn(id);
            return Ok(response.IsSuccess);
        }
    }
}
