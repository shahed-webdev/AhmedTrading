﻿using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public class ExpenseRepository : Repository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ICollection<ExpenseViewModel> ToListCustom()
        {
            var expense = Context.Expense.Include(e => e.ExpenseCategory).Select(e => new ExpenseViewModel
            {
                ExpenseId = e.ExpenseId,
                RegistrationId = e.RegistrationId,
                ExpenseCategoryId = e.ExpenseCategoryId,
                CategoryName = e.ExpenseCategory.CategoryName,
                ExpenseAmount = e.ExpenseAmount,
                ExpenseFor = e.ExpenseFor,
                ExpensePaymentMethod = e.ExpensePaymentMethod,
                ExpenseDate = e.ExpenseDate
            }).ToList();

            return expense;
        }

        public DataResult<ExpenseViewModel> ListDataTable(DataRequest request)
        {
            var expense = Context.Expense.Include(e => e.ExpenseCategory).Select(e => new ExpenseViewModel
            {
                ExpenseId = e.ExpenseId,
                RegistrationId = e.RegistrationId,
                ExpenseCategoryId = e.ExpenseCategoryId,
                CategoryName = e.ExpenseCategory.CategoryName,
                ExpenseAmount = e.ExpenseAmount,
                ExpenseFor = e.ExpenseFor,
                ExpensePaymentMethod = e.ExpensePaymentMethod,
                ExpenseDate = e.ExpenseDate
            });

            return expense.ToDataResult(request);
        }

        public Task<List<ExpenseViewModel>> ToListCustomAsync()
        {
            var expense = Context.Expense.Include(e => e.ExpenseCategory).Select(e => new ExpenseViewModel
            {
                ExpenseId = e.ExpenseId,
                RegistrationId = e.RegistrationId,
                ExpenseCategoryId = e.ExpenseCategoryId,
                CategoryName = e.ExpenseCategory.CategoryName,
                ExpenseAmount = e.ExpenseAmount,
                ExpenseFor = e.ExpenseFor,
                ExpensePaymentMethod = e.ExpensePaymentMethod,
                ExpenseDate = e.ExpenseDate
            }).ToListAsync();

            return expense;
        }

        public void AddCustom(ExpenseViewModel model)
        {
            Add(new Expense
            {
                RegistrationId = model.RegistrationId,
                ExpenseCategoryId = model.ExpenseCategoryId,
                ExpenseAmount = model.ExpenseAmount,
                ExpenseFor = model.ExpenseFor,
                ExpensePaymentMethod = model.ExpensePaymentMethod,
                ExpenseDate = model.ExpenseDate.ToLocalTime()
            });

            var eCategory = Context.ExpenseCategory.Find(model.ExpenseCategoryId);
            eCategory.TotalExpense += model.ExpenseAmount;
            Context.ExpenseCategory.Update(eCategory);
        }

        public void RemoveCustom(int id)
        {
            var expense = Find(id);
            Remove(expense);
            var eCategory = Context.ExpenseCategory.Find(expense.ExpenseCategoryId);
            eCategory.TotalExpense = eCategory.TotalExpense - expense.ExpenseAmount;
            Context.ExpenseCategory.Update(eCategory);
        }

        public ICollection<int> Years()
        {
            var years = Context.Expense
                .GroupBy(e => new
                {
                    e.ExpenseDate.Year
                })
                .Select(g => g.Key.Year)
                .OrderBy(o => o)
                .ToList();

            var currentYear = DateTime.Now.Year;

            if (!years.Contains(currentYear)) years.Add(currentYear);

            return years;
        }

        public double DailyExpenseAmount(DateTime? date)
        {
            var saleDate = date ?? DateTime.Now;
            return Context.Expense.Where(e => e.ExpenseDate.Date == saleDate.Date)?
                       .Sum(e => e.ExpenseAmount) ?? 0;
        }

        public double ExpenseYearly(int year)
        {
            return ToList().Where(s => s.ExpenseDate.Year == year).Sum(s => s.ExpenseAmount);
        }

        public double TotalExpense()
        {
            return Context.Expense?.Sum(s => s.ExpenseAmount) ?? 0;
        }

        public ICollection<MonthlyAmount> MonthlyAmounts(int year)
        {
            var months = Context.Expense.Where(e => e.ExpenseDate.Year == year)
                .GroupBy(e => new
                {
                    number = e.ExpenseDate.Month

                })
                .Select(g => new MonthlyAmount
                {
                    MonthNumber = g.Key.number,
                    Amount = g.Sum(e => e.ExpenseAmount)
                })
                .ToList();

            return months;
        }

        public double DateWiseExpense(DateTime? fromDate, DateTime? toDate)
        {
            var fD = fromDate ?? new DateTime(1000, 1, 1);
            var tD = toDate ?? new DateTime(3000, 12, 31);

            return Context.Expense
                       .Where(s => s.ExpenseDate <= tD && s.ExpenseDate >= fD)?
                       .Sum(s => s.ExpenseAmount) ?? 0;
        }

        public ICollection<ExpenseViewModel> DateToDate(DateTime? sDateTime, DateTime? eDateTime)
        {
            var sD = sDateTime ?? new DateTime(1000, 1, 1);
            var eD = eDateTime ?? new DateTime(3000, 1, 1);

            var expense = Context.Expense.Include(e => e.ExpenseCategory).Where(e => e.ExpenseDate <= eD && e.ExpenseDate >= sD).Select(e => new ExpenseViewModel
            {
                ExpenseId = e.ExpenseId,
                RegistrationId = e.RegistrationId,
                ExpenseCategoryId = e.ExpenseCategoryId,
                CategoryName = e.ExpenseCategory.CategoryName,
                ExpenseAmount = e.ExpenseAmount,
                ExpenseFor = e.ExpenseFor,
                ExpensePaymentMethod = e.ExpensePaymentMethod,
                ExpenseDate = e.ExpenseDate
            }).ToList();

            return expense;
        }
        public ICollection<ExpenseCategoryWise> CategoryWistDateToDate(DateTime? sDateTime, DateTime? eDateTime)
        {
            var sD = sDateTime ?? new DateTime(1000, 1, 1);
            var eD = eDateTime ?? new DateTime(3000, 1, 1);

            var ex = Context.Expense.Include(e => e.ExpenseCategory).Where(e => e.ExpenseDate <= eD && e.ExpenseDate >= sD)
                .GroupBy(e => new
                {
                    ExpenseCategoryID = e.ExpenseCategoryId,
                    e.ExpenseCategory.CategoryName

                })
                .Select(g => new ExpenseCategoryWise
                {
                    ExpenseCategoryId = g.Key.ExpenseCategoryID,
                    CategoryName = g.Key.CategoryName,
                    TotalExpense = g.Sum(e => e.ExpenseAmount)

                })
                .ToList();

            return ex;
        }
    }
}
