using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public interface IProductBrandRepository : IRepository<ProductBrand>, IAddCustom<ProductBrandViewModel>
    {
        Task<bool> IsExistAsync(string name, int updateId = 0);
        ICollection<DDL> ddl();
        Task<List<ProductBrandViewModel>> ListAsync();
        DataResult<ProductBrandViewModel> ListDataTable(DataRequest request);
        bool RemoveCustom(int id);
        void CustomUpdate(ProductBrandViewModel model);
        ProductBrandViewModel FindCustom(int id);
    }
}