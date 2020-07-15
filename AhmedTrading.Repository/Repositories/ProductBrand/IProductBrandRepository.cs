using AhmedTrading.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public interface IProductBrandRepository : IRepository<ProductBrand>, IAddCustom<ProductBrandViewModel>
    {
        Task<bool> IsExistAsync(string name, int updateId = 0);
        ICollection<DDL> ddl();
        bool RemoveCustom(int id);
        void CustomUpdate(ProductBrandViewModel model);
    }
}