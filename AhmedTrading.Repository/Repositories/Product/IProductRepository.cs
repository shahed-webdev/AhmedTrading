using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public interface IProductRepository : IRepository<Product>, IAddCustom<ProductViewModel>
    {
        Task<bool> IsExistAsync(string name, int updateId = 0);
        Task<List<ProductViewModel>> FindByBrandAsync(int brandId = 0);
        DataResult<ProductViewModel> FindByBrandDataTable(DataRequest request);
        bool RemoveCustom(int id);
        Task<ProductViewModel> FindByIdAsync(int ProductId);
        Task<List<ProductViewModel>> FindByNameAsync(string name);
        void CustomUpdate(ProductUpdateModel model);


    }
}