using AhmedTrading.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace AhmedTrading.Web.Controllers
{
    public class ProductBrandController : Controller
    {
        private readonly IUnitOfWork _db;

        public ProductBrandController(IUnitOfWork db)
        {
            _db = db;
        }

        public IActionResult IndexData()
        {
            var data = _db.ProductBrands.ddl();
            return Json(data);
        }

        // GET: Create
        public IActionResult Create()
        {
            return PartialView("_Create");
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> Create(ProductBrandViewModel model)
        {
            var exist = await _db.ProductBrands.IsExistAsync(model.BrandName).ConfigureAwait(false);

            if (exist) ModelState.AddModelError("BrandName", "Brand Name already exist!");

            if (!ModelState.IsValid) return PartialView("_Create", model);

            _db.ProductBrands.AddCustom(model);

            var task = await _db.SaveChangesAsync();

            if (task != 0) return Json(model);

            ModelState.AddModelError("", "Unable to insert record!");
            return PartialView("_Create", model);
        }

        // GET: Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return BadRequest(HttpStatusCode.BadRequest);

            var model = _db.ProductBrands.Find(id.GetValueOrDefault());

            if (model == null) return NotFound();

            return PartialView("_Edit", model);
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> Edit(ProductBrandViewModel model)
        {
            var exist = await _db.ProductBrands.IsExistAsync(model.BrandName, model.ProductBrandId).ConfigureAwait(false);
            if (exist) ModelState.AddModelError("BrandName", "Brand Name already exist!");

            if (!ModelState.IsValid) return PartialView("_Edit", model);

            _db.ProductBrands.CustomUpdate(model);

            var task = await _db.SaveChangesAsync();
            if (task != 0) return Json(model);

            ModelState.AddModelError("", "Unable to update");
            Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return PartialView("_Edit", model);
        }

        // POST: Delete/5
        public int Delete(int id)
        {
            if (!_db.ProductBrands.RemoveCustom(id)) return -1;
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