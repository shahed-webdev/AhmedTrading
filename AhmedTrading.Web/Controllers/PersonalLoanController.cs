using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AhmedTrading.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AhmedTrading.Web.Controllers
{
    [Authorize(Roles = "admin, personal-loan")]
    public class PersonalLoanController : Controller
    {
        private readonly IUnitOfWork _db;
        public PersonalLoanController(IUnitOfWork db)
        {
            _db = db;
        }

        //list of loan
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexDataTable(DataRequest request)
        {
            var data = _db.Person.ListDataTable(request);
            return Json(data);
        }
 

        //**Add person***
        public IActionResult AddPerson([FromBody] PersonModel model)
        {
            if (!ModelState.IsValid) return UnprocessableEntity("Model state invalid");

            var isPhone = _db.Person.IsPhoneExist(model.Phone);
            if (isPhone) return UnprocessableEntity("Mobile number already exists");

            var response = _db.Person.Add(model);

            return Ok(response);
        }
        
        //Get parson
        public async Task<IActionResult> GetPerson(string name)
        {
            var response = await _db.Person.SearchAsync(name);
            return Json(response);
        }

        //Delete parson
        public IActionResult DeletePerson(int id)
        {
            var response = _db.Person.Delete(id);
            return Json(response.IsSuccess);
        }

        //Details parson
        public IActionResult Details(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            var response = _db.Person.Details(id.GetValueOrDefault());
            return View(response.Data);
        }

        //loan details
        public IActionResult LoanDetailsDataTable(DataRequest request)
        {
            var data = _db.PersonalLoan.ListDataTable(request);
            return Json(data);
        }


        //***Add loan***
        public IActionResult AddLoan()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddLoan(PersonalLoanAddModel model)
        {
            model.RegistrationId = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var response = _db.PersonalLoan.Add(model);
            return Json(response);
        }

        //get loan
        public IActionResult GetLoanDataTable(DataRequest request)
        {
            var data = _db.PersonalLoan.ListDataTable(request);
            return Json(data);
        }

        //Delete loan
        public IActionResult DeleteLoan(int id)
        {
            var response = _db.PersonalLoan.Delete(id);
            return Json(response.IsSuccess);
        }


        //****Loan Return***
        public IActionResult LoanReturn(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            var response = _db.PersonalLoan.Details(id.GetValueOrDefault());
            return View(response.Data);
        } 
        
        [HttpPost]
        public IActionResult ReturnLoan(PersonalLoanReturnModel model)
        {
            var response = _db.PersonalLoan.ReturnAdd(model);
            return Json(response);
        }

        //Delete Return
        public IActionResult DeleteReturn(int id)
        {
            var response = _db.PersonalLoan.DeleteReturn(id);
            return Json(response.IsSuccess);
        }
    }
}
