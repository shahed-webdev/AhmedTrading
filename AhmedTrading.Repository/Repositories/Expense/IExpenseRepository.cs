using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public interface IExpenseRepository : IRepository<Expense>, IAddCustom<ExpenseViewModel>
    {
        ICollection<ExpenseViewModel> ToListCustom();
        DataResult<ExpenseViewModel> ListDataTable(DataRequest request);
        Task<List<ExpenseViewModel>> ToListCustomAsync();
        // void AddCustom(ExpenseViewModel model);
        void RemoveCustom(int id);
        ICollection<int> Years();
        double DailyExpenseAmount(DateTime? day);
        double ExpenseYearly(int year);
        double TotalExpense();
        ICollection<MonthlyAmount> MonthlyAmounts(int year);

        double DateWiseExpense(DateTime? fromDate, DateTime? toDate);

        ICollection<ExpenseViewModel> DateToDate(DateTime? sDateTime, DateTime? eDateTime);

        ICollection<ExpenseCategoryWise> CategoryWistDateToDate(DateTime? sDateTime, DateTime? eDateTime);


    }
}
