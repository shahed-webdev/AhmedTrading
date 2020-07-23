using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
        public void AddCustom(ProductViewModel model)
        {
            var product = new Product
            {
                ProductName = model.ProductName,
                ProductBrandId = model.ProductBrandId,
                UnitType = model.UnitType
            };

            Add(product);
        }

        public Task<bool> IsExistAsync(string name, int updateId = 0)
        {
            var product = Context.Product.Where(p => p.ProductName == name);

            if (updateId != 0) product.Where(p => p.ProductId != updateId);
            return product.AnyAsync();
        }

        public Task<List<ProductViewModel>> FindByBrandAsync(int brandId = 0)
        {
            var products = Context.Product.Select(p =>
                new ProductViewModel
                {
                    ProductId = p.ProductId,
                    ProductBrandId = p.ProductBrandId,
                    ProductName = p.ProductName,
                    BrandName = p.ProductName,
                    SellingUnitPrice = p.SellingUnitPrice,
                    UnitType = p.UnitType,
                    Stock = p.Stock
                });

            return brandId != 0 ? products.Where(p => p.ProductBrandId == brandId).ToListAsync() : products.Take(20).ToListAsync();
        }

        public DataResult<ProductViewModel> FindByBrandDataTable(DataRequest request)
        {
            var products = Context.Product.Select(p =>
                new ProductViewModel
                {
                    ProductId = p.ProductId,
                    ProductBrandId = p.ProductBrandId,
                    ProductName = p.ProductName,
                    BrandName = p.ProductName,
                    SellingUnitPrice = p.SellingUnitPrice,
                    UnitType = p.UnitType,
                    Stock = p.Stock
                });
            return products.ToDataResult(request);
        }

        public bool RemoveCustom(int id)
        {
            if (Context.PurchaseList.Any(e => e.ProductId == id)) return false;
            Remove(Find(id));
            return true;
        }

        public Task<ProductViewModel> FindByIdAsync(int ProductId)
        {
            var product = Context.Product.Where(p => p.ProductId == ProductId).Select(p =>
                 new ProductViewModel
                 {
                     ProductId = p.ProductId,
                     ProductBrandId = p.ProductBrandId,
                     ProductName = p.ProductName,
                     BrandName = p.ProductName,
                     SellingUnitPrice = p.SellingUnitPrice,
                     UnitType = p.UnitType,
                     Stock = p.Stock
                 });

            return product.FirstOrDefaultAsync();
        }

        public Task<List<ProductViewModel>> FindByNameAsync(string name)
        {
            var product = Context.Product.Where(p => p.ProductName.Contains(name)).Select(p =>
                 new ProductViewModel
                 {
                     ProductId = p.ProductId,
                     ProductBrandId = p.ProductBrandId,
                     ProductName = p.ProductName,
                     BrandName = p.ProductBrand.BrandName,
                     SellingUnitPrice = p.SellingUnitPrice,
                     UnitType = p.UnitType,
                     Stock = p.Stock
                 }).Take(5);

            return product.ToListAsync();
        }

        public void CustomUpdate(ProductUpdateModel model)
        {
            var product = Context.Product.Find(model.ProductId);
            product.ProductBrandId = model.ProductBrandId;
            product.ProductName = model.ProductName;
            product.UnitType = model.UnitType;
            product.SellingUnitPrice = model.SellingUnitPrice;
            Context.Product.Update(product);
        }
    }
}