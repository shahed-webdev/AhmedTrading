using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public class ProductBrandRepository : Repository<ProductBrand>, IProductBrandRepository
    {
        public ProductBrandRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void AddCustom(ProductBrandViewModel model)
        {
            var brand = new ProductBrand
            {
                BrandName = model.BrandName
            };
            Context.ProductBrand.Add(brand);
        }

        public async Task<bool> IsExistAsync(string name, int updateId = 0)
        {
            if (updateId == 0)
                return await Context.ProductBrand.AnyAsync(b => b.BrandName == name).ConfigureAwait(false);

            return await Context.ProductBrand.AnyAsync(b => b.BrandName == name && b.ProductBrandId != updateId).ConfigureAwait(false);
        }

        public ICollection<DDL> ddl()
        {
            return Context.ProductBrand.Select(b => new DDL
            {
                value = b.ProductBrandId,
                label = b.BrandName
            }).ToList();
        }

        public Task<List<ProductBrandViewModel>> ListAsync()
        {
            return Context.ProductBrand.Select(b => new ProductBrandViewModel
            {
                ProductBrandId = b.ProductBrandId,
                BrandName = b.BrandName
            }).ToListAsync();
        }

        public DataResult<ProductBrandViewModel> ListDataTable(DataRequest request)
        {
            return Context.ProductBrand.Select(b => new ProductBrandViewModel
            {
                ProductBrandId = b.ProductBrandId,
                BrandName = b.BrandName
            }).ToDataResult(request);
        }

        public bool RemoveCustom(int id)
        {
            if (Context.Product.Any(b => b.ProductBrandId == id)) return false;
            Remove(Find(id));
            return true;
        }

        public void CustomUpdate(ProductBrandViewModel model)
        {
            var brand = Find(model.ProductBrandId);
            brand.BrandName = model.BrandName;
            Context.ProductBrand.Update(brand);
        }

        public ProductBrandViewModel FindCustom(int id)
        {
            return Context.ProductBrand.Select(p => new ProductBrandViewModel
            {
                ProductBrandId = p.ProductBrandId,
                BrandName = p.BrandName
            })
                .FirstOrDefault(p => p.ProductBrandId == id);
        }
    }
}