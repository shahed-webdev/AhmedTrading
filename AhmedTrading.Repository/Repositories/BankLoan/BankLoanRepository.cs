using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using System;
using System.Linq;

namespace AhmedTrading.Repository
{
    public class BankLoanRepository : Repository<BankLoan>, IBankLoanRepository
    {
        public BankLoanRepository(ApplicationDbContext context) : base(context)
        {
        }

        public DbResponse AddLoan(BankLoanModel model)
        {
            try
            {
                if (IsLoanNameExist(model.LoanName)) return new DbResponse(false, "Loan name already exist");

                var bankLoan = new BankLoan
                {
                    BankAccountId = model.BankAccountId,
                    LoanName = model.LoanName,
                    LoanAmount = model.LoanAmount,
                    LoanDate = model.LoanDate,
                    InterestPercentage = model.InterestPercentage,
                    ReturnPeriod = model.ReturnPeriod,
                    LoanDetails = model.LoanDetails
                };

                Context.BankLoan.Add(bankLoan);
                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public bool IsLoanNameExist(string name)
        {
            return Context.BankLoan.Any(l => l.LoanName == name);
        }

        public bool IsLoanNameExist(string name, int updateLoanId)
        {
            return Context.BankLoan.Any(l => l.LoanName == name && l.BankLoanId != updateLoanId);
        }

        public DbResponse<BankLoanViewModel> FindLoan(int id)
        {
            try
            {
                var loan = Context.BankLoan.Find(id);
                if (loan == null) return new DbResponse<BankLoanViewModel>(false, "No Data Found");

                var bankLoan = new BankLoanViewModel
                {
                    BankLoanId = loan.BankLoanId,
                    BankAccountId = loan.BankAccountId,
                    LoanName = loan.LoanName,
                    LoanAmount = loan.LoanAmount,
                    LoanDate = loan.LoanDate,
                    InterestPercentage = loan.InterestPercentage,
                    ReturnPeriod = loan.ReturnPeriod,
                    LoanDetails = loan.LoanDetails,
                    RemainingAmount = loan.RemainingAmount,
                    ReturnAmount = loan.ReturnAmount
                };
                return new DbResponse<BankLoanViewModel>(true, "Success") { Data = bankLoan };
            }
            catch (Exception e)
            {
                return new DbResponse<BankLoanViewModel>(false, e.Message);
            }
        }

        public DataResult<BankLoanViewModel> RecordsDataTable(DataRequest request)
        {
            var loans = Context.BankLoan.Select(l => new BankLoanViewModel
            {
                BankLoanId = l.BankLoanId,
                BankAccountId = l.BankAccountId,
                LoanName = l.LoanName,
                LoanAmount = l.LoanAmount,
                LoanDate = l.LoanDate,
                ReturnAmount = l.ReturnAmount,
                RemainingAmount = l.RemainingAmount,
                InterestPercentage = l.InterestPercentage,
                ReturnPeriod = l.ReturnPeriod,
                LoanDetails = l.LoanDetails
            });
            return loans.ToDataResult(request);
        }

        public DataResult<BankLoanReturnViewModel> ReturnRecordsDataTable(DataRequest request)
        {
            var returns = Context.BankLoanReturn.Select(l => new BankLoanReturnViewModel
            {
                BankLoanReturnId = l.BankLoanReturnId,
                BankLoanId = l.BankLoanId,
                ReturnAmount = l.ReturnAmount,
                ReturnDate = l.ReturnDate
            });

            return returns.ToDataResult(request);
        }

        public DbResponse DeleteLoan(int id)
        {
            try
            {
                var loan = Context.BankLoan.Find(id);
                if (loan == null) return new DbResponse(false, "No Data Found");
                if (Context.BankLoanReturn.Any(l => l.BankLoanId == loan.BankLoanId)) return new DbResponse(false, "Loan Return records Exists");

                Context.BankLoan.Remove(loan);
                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse LoanReturn(BankLoanReturnModel model)
        {
            try
            {
                var loan = Context.BankLoan.Find(model.BankLoanId);
                if (loan == null) return new DbResponse(false, "No Loan Found");

                var loanReturn = new BankLoanReturn
                {
                    BankLoanId = model.BankLoanId,
                    ReturnAmount = model.ReturnAmount,
                    ReturnDate = model.ReturnDate
                };

                Context.BankLoanReturn.Add(loanReturn);
                loan.ReturnAmount += model.ReturnAmount;
                Context.BankLoan.Update(loan);
                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse DeleteLoanReturn(int id)
        {
            try
            {
                var loanReturn = Context.BankLoanReturn.Find(id);
                if (loanReturn == null) return new DbResponse(false, "No Loan Found");
                var loan = Context.BankLoan.Find(loanReturn.BankLoanId);

                Context.BankLoanReturn.Remove(loanReturn);
                loan.ReturnAmount -= loan.ReturnAmount;
                Context.BankLoan.Update(loan);
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