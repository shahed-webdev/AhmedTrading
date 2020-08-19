using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AhmedTrading.Repository
{
    public class BankAccountRepository : Repository<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(ApplicationDbContext context) : base(context)
        {

        }

        public DbResponse CreateAccount(BankAccountCreateModel model)
        {
            try
            {
                if (IsExistAccount(model.AccountName, model.AccountNumber)) return new DbResponse(false, "Account name already exist");

                var bankAccount = new BankAccount
                {
                    AccountName = model.AccountName,
                    BankName = model.BankName,
                    AccountNumber = model.AccountNumber
                };

                Context.BankAccount.Add(bankAccount);
                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }

        }

        public DbResponse DeleteAccount(int id)
        {
            try
            {
                var bankAccount = Context.BankAccount.Find(id);

                if (bankAccount == null) return new DbResponse(false, "No Data Found");

                if (Context.BankDeposit.Any(d => d.BankAccountId == bankAccount.BankAccountId)) return new DbResponse(false, "Deposited Record Exists");

                if (Context.BankLoan.Any(d => d.BankAccountId == bankAccount.BankAccountId)) return new DbResponse(false, "Load Record Exists");

                Context.BankAccount.Remove(bankAccount);
                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse UpdateAccount(BankAccountUpdateModel model)
        {
            try
            {
                var bankAccount = Context.BankAccount.Find(model.BankAccountId);

                if (bankAccount == null) return new DbResponse(false, "No Data Found");

                if (IsExistAccount(model.AccountName, model.AccountNumber, model.BankAccountId)) return new DbResponse(false, "Account already exist");


                bankAccount.AccountName = model.AccountName;
                bankAccount.AccountNumber = model.AccountNumber;
                bankAccount.BankName = model.BankName;


                Context.BankAccount.Update(bankAccount);
                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public BankAccountViewModel AccountDetails(int id)
        {
            var bankAccount = Context.BankAccount.Select(b => new BankAccountViewModel
            {
                BankAccountId = b.BankAccountId,
                BankName = b.BankName,
                AccountName = b.AccountName,
                AccountNumber = b.AccountNumber,
                Balance = b.Balance
            });
            return bankAccount.FirstOrDefault(b => b.BankAccountId == id);
        }

        public DataResult<BankAccountViewModel> AccountListDataTable(DataRequest request)
        {
            var bankAccount = Context.BankAccount
                .OrderBy(b => b.BankName)
                .ThenBy(b => b.AccountName)
                .ThenBy(b => b.AccountNumber).Select(b => new BankAccountViewModel
                {
                    BankAccountId = b.BankAccountId,
                    AccountName = b.AccountName,
                    AccountNumber = b.AccountNumber,
                    BankName = b.BankName,
                    Balance = b.Balance
                });

            return bankAccount.ToDataResult(request);
        }

        public ICollection<DDL> Ddl()
        {
            return Context.BankAccount
                .OrderBy(b => b.BankName)
                .ThenBy(b => b.AccountName)
                .ThenBy(b => b.AccountNumber)
                .Select(b => new DDL
                {
                    value = b.BankAccountId,
                    label = $"{b.AccountName} ({b.AccountNumber})"
                }).ToList();
        }

        public bool IsExistAccount(string name, string number)
        {
            return Context.BankAccount.Any(b => b.AccountName == name || b.AccountNumber == number);
        }

        public bool IsExistAccount(string name, string number, int updateAccountId)
        {
            return Context.BankAccount.Any(b => (b.AccountName == name && b.BankAccountId != updateAccountId) || (b.AccountNumber == number && b.BankAccountId != updateAccountId));
        }

        public DbResponse Deposit(BankDepositModel model)
        {
            try
            {
                var bankAccount = Context.BankAccount.Find(model.BankAccountId);
                if (bankAccount == null) return new DbResponse(false, "Bank Account not Found");

                var bankDeposit = new BankDeposit
                {
                    BankAccountId = model.BankAccountId,
                    Amount = model.Amount,
                    Details = model.Details,
                    ActivityDate = model.ActivityDate.ToLocalTime()
                };

                Context.BankDeposit.Add(bankDeposit);

                //Update Bank Balance 
                bankAccount.Balance += model.Amount;
                Context.BankAccount.Update(bankAccount);

                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse DeleteDeposit(int id)
        {
            try
            {
                var deposit = Context.BankDeposit.Find(id);
                if (deposit == null) return new DbResponse(false, "Data not Found");
                var bankAccount = Context.BankAccount.Find(deposit.BankAccountId);

                if (bankAccount.Balance < deposit.Amount) return new DbResponse(false, $"No available balance in {bankAccount.AccountName}, Current Balance is {bankAccount.Balance}");

                Context.BankDeposit.Remove(deposit);

                //Update Bank Balance 
                bankAccount.Balance -= deposit.Amount;
                Context.BankAccount.Update(bankAccount);

                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DataResult<BankDepositViewModel> DepositListDataTable(DataRequest request)
        {
            var withdraws = Context.BankDeposit.Select(w => new BankDepositViewModel
            {
                BankDepositId = w.BankDepositId,
                BankAccountId = w.BankAccountId,
                Amount = w.Amount,
                Details = w.Details,
                ActivityDate = w.ActivityDate
            });
            return withdraws.ToDataResult(request);
        }

        public DbResponse Withdrew(BankWithdrewModel model)
        {
            try
            {
                var bankAccount = Context.BankAccount.Find(model.BankAccountId);
                if (bankAccount == null) return new DbResponse(false, "Bank Account not Found");

                if (bankAccount.Balance < model.Amount) return new DbResponse(false, $"No available balance in {bankAccount.AccountName}, Current Balance is {bankAccount.Balance}");

                var bankWithdrew = new BankWithdrew
                {
                    BankAccountId = model.BankAccountId,
                    Amount = model.Amount,
                    Details = model.Details,
                    ActivityDate = model.ActivityDate.ToLocalTime()
                };

                Context.BankWithdrew.Add(bankWithdrew);

                //Update Bank Balance 
                bankAccount.Balance -= model.Amount;
                Context.BankAccount.Update(bankAccount);

                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }


        public DbResponse DeleteWithdrew(int id)
        {
            try
            {
                var withdrew = Context.BankWithdrew.Find(id);
                if (withdrew == null) return new DbResponse(false, "Data not Found");
                var bankAccount = Context.BankAccount.Find(withdrew.BankAccountId);

                Context.BankWithdrew.Remove(withdrew);

                //Update Bank Balance 
                bankAccount.Balance += withdrew.Amount;
                Context.BankAccount.Update(bankAccount);

                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DataResult<BankWithdrewViewModel> WithdrewListDataTable(DataRequest request)
        {
            var withdraws = Context.BankWithdrew.Select(w => new BankWithdrewViewModel
            {
                BankWithdrewId = w.BankWithdrewId,
                BankAccountId = w.BankAccountId,
                Amount = w.Amount,
                Details = w.Details,
                ActivityDate = w.ActivityDate
            });
            return withdraws.ToDataResult(request);
        }

        public double TotalDeposit()
        {
            return Context.BankDeposit?.Sum(s => s.Amount) ?? 0;
        }

        public double TotalWithdrew()
        {
            return Context.BankWithdrew?.Sum(s => s.Amount) ?? 0;
        }

        public double DateWiseWithdrew(DateTime? fromDate, DateTime? toDate)
        {
            var fD = fromDate ?? new DateTime(1000, 1, 1);
            var tD = toDate ?? new DateTime(3000, 12, 31);

            return Context.BankWithdrew
                       .Where(s => s.ActivityDate <= tD && s.ActivityDate >= fD)?
                       .Sum(s => s.Amount) ?? 0;
        }

        public double DateWiseDeposit(DateTime? fromDate, DateTime? toDate)
        {
            var fD = fromDate ?? new DateTime(1000, 1, 1);
            var tD = toDate ?? new DateTime(3000, 12, 31);

            return Context.BankDeposit
                       .Where(s => s.ActivityDate <= tD && s.ActivityDate >= fD)?
                       .Sum(s => s.Amount) ?? 0;
        }
    }
}