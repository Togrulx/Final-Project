using IydeParfume.Areas.Admin.ViewModels.Brand;
using IydeParfume.Database;
using IydeParfume.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/brand")]
    [Authorize(Roles = "admin")]

    public class BrandController : Controller
    {
        private readonly DataContext _dataContext;

        public BrandController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region List'

        [HttpGet("list", Name = "admin-brand-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Brands.Select(g => new ListItemViewModel(g.Id, g.Name)).ToListAsync();


            return View(model);
        }
        #endregion

        #region Add'

        [HttpGet("add", Name = "admin-brand-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }


        [HttpPost("add", Name = "admin-brand-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var brand = new Brand
            {
                Name = model.Title
            };

            await _dataContext.Brands.AddAsync(brand);

            await _dataContext.SaveChangesAsync();



            return RedirectToRoute("admin-brand-list");
        }
        #endregion

        #region Delete'

        [HttpPost("delete/{id}", Name = "admin-brand-delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var brands = await _dataContext.Brands.FirstOrDefaultAsync(t => t.Id == id);

            if (brands is null) return NotFound();

            _dataContext.Brands.Remove(brands);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-brand-list");
        }
        #endregion
    }
}
