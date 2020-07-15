using AhmedTrading.Data;
using System.Collections.Generic;

namespace AhmedTrading.Repository
{
    public interface IExpenseCategoryRepository : IRepository<ExpenseCategory>
    {
        ICollection<DDL> ddl();
        bool RemoveCustom(int id);
    }
}
