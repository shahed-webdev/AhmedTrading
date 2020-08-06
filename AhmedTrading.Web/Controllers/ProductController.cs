using AhmedTrading.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Threading.Tasks;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Authorization;

namespace AhmedTrading.Web.Controllers
{
    [Authorize(Roles = "admin, product")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _db;

        public ProductController(IUnitOfWork db)
        {
            _db = db;
        }

        //Add Product
        public IActionResult AddProduct()
        {
            ViewBag.ProductBrand = new SelectList(_db.ProductBrands.ddl(), "value", "label");
            return View();
        }


        //get product from ajax by categoryId
        public IActionResult GetProductByBrand(DataRequest request)
        {
            var productList = _db.Products.FindByBrandDataTable(request);
            return Json(productList);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
            ViewBag.ProductBrand = new SelectList(_db.ProductBrands.ddl(), "value", "label");

            if (!ModelState.IsValid) return View(model);

            var isExist = await _db.Products.IsExistAsync(model.ProductName).ConfigureAwait(false);

            if (isExist)
            {
                ModelState.AddModelError("ProductName", "Product Name Already Exist");
                return View(model);
            }

            _db.Products.AddCustom(model);
            await _db.SaveChangesAsync();

            return RedirectToAction("AddProduct");
        }


        //Update Product
        public async Task<IActionResult> UpdateProduct(int? id)
        {
            if (id == null) return RedirectToAction("AddProduct");

            var model = await _db.Products.FindByIdAsync(id.GetValueOrDefault()).ConfigureAwait(false);
            if (model == null) return RedirectToAction("AddProduct");

            ViewBag.ProductBrand = new SelectList(_db.ProductBrands.ddl(), "value", "label", model.ProductBrandId);
            
            var r = new ProductUpdateModel
            {
                ProductId = model.ProductId,
                ProductBrandId = model.ProductBrandId,
                ProductName = model.ProductName,
                SellingUnitPrice = model.SellingUnitPrice,
                UnitType = model.UnitType,
                Stock = model.Stock
            };

            return View(r);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductUpdateModel model)
        {
            ViewBag.ProductBrand = new SelectList(_db.ProductBrands.ddl(), "value", "label", model.ProductBrandId);

            var exist = await _db.Products.IsExistAsync(model.ProductName, model.ProductId).ConfigureAwait(false);
            if (exist) ModelState.AddModelError("ProductName", "Product Name already exist!");

            if (!ModelState.IsValid) return View(model);

            _db.Products.CustomUpdate(model);

            var task = await _db.SaveChangesAsync();
            if (task != 0) return RedirectToAction("AddProduct");

            return View(model);
        }

        //Find Product
        public IActionResult FindProduct()
        {
            return View();
        }

        //find product (ajax)
        public async Task<IActionResult> FindProductsByName(string name)
        {
            var data = await _db.Products.FindByNameAsync(name).ConfigureAwait(false);
            return Json(data);
        }

        //Delete Product
        public int DeleteProduct(int id)
        {
            if (!_db.Products.RemoveCustom(id)) return -1;
            return _db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}