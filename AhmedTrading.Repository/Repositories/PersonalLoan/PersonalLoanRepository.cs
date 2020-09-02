using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AhmedTrading.Repository
{
    public class PersonalLoanRepository : Repository<PersonalLoan>, IPersonalLoanRepository
    {
        public PersonalLoanRepository(ApplicationDbContext context) : base(context)
        {
        }

        public DbResponse Add(PersonalLoanAddModel model)
        {
            try
            {
                if (IsNameExist(model.LoanName, model.PersonId)) return new DbResponse(false, "Loan name already exist");

                var loan = new PersonalLoan
                {
                    PersonId = model.PersonId,
                    RegistrationId = model.RegistrationId,
                    LoanName = model.LoanName,
                    LoanAmount = model.LoanAmount,
                    LoanDate = model.LoanDate.ToLocalTime()
                };

                Context.PersonalLoan.Add(loan);
                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DataResult<PersonalLoanModel> ListDataTable(DataRequest request)
        {
            return Context.PersonalLoan.Select(p => new PersonalLoanModel
            {
                PersonalLoanId = p.PersonalLoanId,
                PersonId = p.PersonId,
                PersonName = p.Person.Name,
                LoanName = p.LoanName,
                LoanAmount = p.LoanAmount,
                ReturnAmount = p.ReturnAmount,
                RemainingAmount = p.RemainingAmount,
                LoanDate = p.LoanDate
            }).ToDataResult(request);
        }

        public bool IsNameExist(string name, int personId)
        {
            return Context.PersonalLoan.Any(a => a.LoanName == name && a.PersonId == personId);
        }

        public bool IsNameExist(string name, int personId, int updateId)
        {
            return Context.PersonalLoan.Any(a => a.LoanName == name && a.PersonId == personId && a.PersonalLoanId != updateId);
        }

        public DbResponse<PersonLoanDetailsModel> Details(int personLoanId)
        {
            try
            {
                var p = Context.PersonalLoan
                    .Include(l => l.Person)
                    .Include(l => l.PersonalLoanReturn)
                    .FirstOrDefault(l => l.PersonalLoanId == personLoanId);
                if (p == null) return new DbResponse<PersonLoanDetailsModel>(false, "No Data Found");

                var loanDetails = new PersonLoanDetailsModel
                {
                    LoanInfo = new PersonalLoanModel
                    {
                        PersonalLoanId = p.PersonalLoanId,
                        PersonId = p.PersonId,
                        PersonName = p.Person.Name,
                        LoanName = p.LoanName,
                        LoanAmount = p.LoanAmount,
                        ReturnAmount = p.ReturnAmount,
                        RemainingAmount = p.RemainingAmount,
                        LoanDate = p.LoanDate
                    },
                    Returns = p.PersonalLoanReturn.Select(l => new PersonalLoanReturnModel
                    {
                        PersonalLoanId = l.PersonalLoanId,
                        ReturnAmount = l.ReturnAmount,
                        ReturnDate = l.ReturnDate
                    }).ToList()
                };
                return new DbResponse<PersonLoanDetailsModel>(true, "Success") { Data = loanDetails };
            }
            catch (Exception e)
            {
                return new DbResponse<PersonLoanDetailsModel>(false, e.Message);
            }
        }

        public DbResponse Delete(int personLoanId)
        {
            try
            {
                var loan = Context.PersonalLoan
                    .Include(l => l.PersonalLoanReturn)
                    .FirstOrDefault(l => l.PersonalLoanId == personLoanId);
                if (loan == null) return new DbResponse(false, "No Data Found");

                Context.PersonalLoan.Remove(loan);
                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse DeleteReturn(int personalLoanReturnId)
        {
            try
            {
                var personalLoanReturn = Context.PersonalLoanReturn.Find(personalLoanReturnId);
                if (personalLoanReturn == null) return new DbResponse(false, "No Data Found");

                Context.PersonalLoanReturn.Remove(personalLoanReturn);

                // Update Loan return value
                var loan = Context.PersonalLoan.Find(personalLoanReturn.PersonalLoanId);
                loan.ReturnAmount -= personalLoanReturn.ReturnAmount;
                Context.PersonalLoan.Update(loan);
                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse ReturnAdd(PersonalLoanReturnModel model)
        {
            try
            {
                var loanReturn = new PersonalLoanReturn
                {
                    PersonalLoanId = model.PersonalLoanId,
                    ReturnAmount = model.ReturnAmount,
                    ReturnDate = model.ReturnDate.ToLocalTime()
                };
                Context.PersonalLoanReturn.Add(loanReturn);

                // Update Loan return value
                var loan = Context.PersonalLoan.Find(model.PersonalLoanId);
                loan.ReturnAmount += model.ReturnAmount;
                Context.PersonalLoan.Update(loan);

                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }
    }
}