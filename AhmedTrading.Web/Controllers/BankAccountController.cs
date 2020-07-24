using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AhmedTrading.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AhmedTrading.Web.Controllers
{
    [Authorize]
    public class BankAccountController : Controller
    {
        private readonly IUnitOfWork _db;
        public BankAccountController(IUnitOfWork db)
        {
            _db = db;
        }

        /***BANK ACCOUNT**/
        public IActionResult BankList()
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

        [HttpPost]
        public IActionResult UpdateBankName([FromBody] BankAccountUpdateModel model)
        {
            var response = _db.BankAccounts.UpdateAccount(model);
            if (response.IsSuccess) return Ok(response);

            return UnprocessableEntity(response);
        }

        public IActionResult Delete(int id)
        {
           var response=  _db.BankAccounts.DeleteAccount(id);
          
           if (response.IsSuccess)
               return Ok(response.IsSuccess);

           return Ok(response.IsSuccess);
        }



        /***BANK DEPOSIT**/
        public IActionResult BankDeposit()
        {
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

           var response= _db.BankAccounts.Deposit(model);
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
    }
}
