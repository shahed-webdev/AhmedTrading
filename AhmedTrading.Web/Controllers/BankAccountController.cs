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
    [Authorize(Roles = "admin, bank-account")]
    public class BankAccountController : Controller
    {
        private readonly IUnitOfWork _db;

        public BankAccountController(IUnitOfWork db)
        {
            _db = db;
        }

        /***BANK ACCOUNT**/
        public IActionResult AccountList()
        {
            return View();
        }

        //request from data-table(ajax)
        public IActionResult BankListData(DataRequest request)
        {
            var data = _db.BankAccounts.AccountListDataTable(request);
            return Json(data);
        }


        public IActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(BankAccountCreateModel model)
        {
            if (!ModelState.IsValid) return PartialView("_Create", model);

            var response = _db.BankAccounts.CreateAccount(model);

            if (!response.IsSuccess)
            {
                ModelState.AddModelError("AccountName", response.Message);
                return PartialView("_Create", model);
            }

            var result = new AjaxContent<BankAccountCreateModel> {Status = true, Data = model};
            return Json(result);
        }

        // GET: Vendors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return BadRequest(HttpStatusCode.BadRequest);

            var model = _db.BankAccounts.AccountDetails(id.GetValueOrDefault());
            if (model == null) return NotFound();

            return PartialView("_Edit", model);
        }

        // POST: Vendors/Edit/5
        [HttpPost]
        public IActionResult Edit(BankAccountUpdateModel model)
        {
            var viewModel= new BankAccountViewModel
            {
                BankAccountId = model.BankAccountId,
                BankName = model.BankName,
                AccountName = model.AccountName,
                AccountNumber = model.AccountNumber
            };

            if (!ModelState.IsValid) return PartialView("_Edit", viewModel);

            var response = _db.BankAccounts.UpdateAccount(model);

            if (!response.IsSuccess)
            {
                ModelState.AddModelError("AccountName", response.Message);
                return PartialView("_Edit", viewModel);
            }


            var result = new AjaxContent<BankAccountViewModel> { Status = true, Data = viewModel };
            return Json(result);
        }


        public IActionResult Delete(int id)
        {
            var response = _db.BankAccounts.DeleteAccount(id);
            return Ok(response.IsSuccess);
        }


        /***BANK DEPOSIT**/
        public IActionResult BankDeposit()
        {
            ViewBag.bankName = new SelectList(_db.BankAccounts.Ddl(), "value", "label");
            return View();
        }

        //request from data-table(ajax)
        public IActionResult DepositData(DataRequest request)
        {
            var data = _db.BankAccounts.DepositListDataTable(request);
            return Json(data);
        }

        [HttpPost]
        public IActionResult Deposit([FromBody] BankDepositModel model)
        {
            if (!ModelState.IsValid) return UnprocessableEntity("Invalid Model State");

            var response = _db.BankAccounts.Deposit(model);
            if (response.IsSuccess) return Ok(response.IsSuccess);

            return UnprocessableEntity(response.Message);
        }

        [HttpPost]
        public IActionResult DeleteDeposit(int id)
        {
            var response = _db.BankAccounts.DeleteDeposit(id);
            if (response.IsSuccess) return Ok(response.IsSuccess);

            return UnprocessableEntity(response.Message);
        }


        /***BANK WITHDRAW**/
        public IActionResult BankWithdrew()
        {
            ViewBag.bankName = new SelectList(_db.BankAccounts.Ddl(), "value", "label"); ;
            return View();
        }

        //request from data-table(ajax)
        public IActionResult WithdrewData(DataRequest request)
        {
            var data = _db.BankAccounts.WithdrewListDataTable(request);
            return Json(data);
        }

        [HttpPost]
        public IActionResult Withdrew([FromBody] BankWithdrewModel model)
        {
            if (!ModelState.IsValid) return UnprocessableEntity("Invalid Model State");

            var response = _db.BankAccounts.Withdrew(model);
            if (response.IsSuccess) return Ok(response.IsSuccess);

            return UnprocessableEntity(response.Message);
        }

        [HttpPost]
        public IActionResult DeleteWithdrew(int id)
        {
            var response = _db.BankAccounts.DeleteWithdrew(id);
            if (response.IsSuccess) return Ok(response.IsSuccess);

            return UnprocessableEntity(response.Message);
        }

        //account info(ajax)
        public IActionResult AccountDetails(int bankId)
        {
            var data = _db.BankAccounts.AccountDetails(bankId);
            return Json(data);
        }
    }
}
