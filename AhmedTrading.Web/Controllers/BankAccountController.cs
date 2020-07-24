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
    }
}
