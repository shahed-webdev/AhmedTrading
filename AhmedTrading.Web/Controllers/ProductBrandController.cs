using AhmedTrading.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AhmedTrading.Web.Controllers
{
    [Authorize(Roles = "admin, product-brand")]
    public class ProductBrandController : Controller
    {
        private readonly IUnitOfWork _db;

        public ProductBrandController(IUnitOfWork db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: data (ajax)
        public async Task<IActionResult> GetData()
        {
            var data = await _db.ProductBrands.ListAsync().ConfigureAwait(false);
            return Json(data);
        }

        // POST: Create Brand (ajax)
        [HttpPost]
        public async Task<IActionResult> CreateBrand(ProductBrandViewModel model)
        {
            var exist = await _db.ProductBrands.IsExistAsync(model.BrandName).ConfigureAwait(false);

            if (exist) return UnprocessableEntity("Brand Name already exist!");

            _db.ProductBrands.AddCustom(model);
            var task = await _db.SaveChangesAsync();

            if (task != 0) return Ok();

            return UnprocessableEntity("Unable to insert record!");
        }

        // GET: Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            var model = _db.ProductBrands.FindCustom(id.GetValueOrDefault());
            if (model == null) return RedirectToAction("Index");

            return View(model);
        }

        // POST: Edit
        [HttpPost]
        public async Task<IActionResult> Edit(ProductBrandViewModel model)
        {
            var exist = await _db.ProductBrands.IsExistAsync(model.BrandName, model.ProductBrandId).ConfigureAwait(false);
            if (exist) ModelState.AddModelError("BrandName", "Brand Name already exist!");

            if (!ModelState.IsValid) return View(model);

            _db.ProductBrands.CustomUpdate(model);

            var task = await _db.SaveChangesAsync();
            if (task != 0) return RedirectToAction("Index");

            ModelState.AddModelError("", "Unable to update");
            return View(model);
        }

        // POST: Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (!_db.ProductBrands.RemoveCustom(id)) return UnprocessableEntity("Brand Name already in Used!");

            var task = await _db.SaveChangesAsync();

            if (task != 0) return Ok();

            return UnprocessableEntity("Unable to perform action!");
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