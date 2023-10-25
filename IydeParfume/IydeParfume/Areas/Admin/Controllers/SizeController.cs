using IydeParfume.Areas.Admin.ViewModels.Size;
using IydeParfume.Database;
using IydeParfume.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/size")]
    [Authorize(Roles = "admin")]

    public class SizeController : Controller
    {
        private readonly DataContext _dataContext;

        public SizeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region List'

        [HttpGet("list", Name = "admin-size-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Sizes.Select(c => new ListItemViewModel(c.Id, c.PrSize, c.PrPercent)).ToListAsync();
            return View(model);
        }
        #endregion

        #region Add'

        [HttpGet("add", Name = "admin-size-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost("add", Name = "admin-size-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            


            var size = new Size
            {
                PrSize = model.PrSize,
                PrPercent = model.PrPercent
            };
            await _dataContext.Sizes.AddAsync(size);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-size-list");
        }
        #endregion

        #region Update'

        [HttpGet("update/{id}", Name = "admin-size-update")]
        public async Task<IActionResult> Update([FromRoute] int id)
        {
            var size = await _dataContext.Sizes.FirstOrDefaultAsync(s => s.Id == id);

            if (size is null) return NotFound();
          
            var model = new UpdateViewModel
            {
                Id = size.Id,
                PrSize = size.PrSize,
                PrPercent = size.PrPercent
            };

            return View(model);
        }
        [HttpPost("update/{id}", Name = "admin-size-update")]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            var size = await _dataContext.Sizes.FirstOrDefaultAsync(s => s.Id == model.Id);
            if (size is null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            size.PrSize = model.PrSize;
            size.PrPercent = model.PrPercent;

            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-size-list");
        }
        #endregion

        #region Delete'

        [HttpPost("delete/{id}", Name = "admin-size-delete")]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            var size = await _dataContext.Sizes.FirstOrDefaultAsync(c => c.Id == id);

            if (size is null) return NotFound();
           
            _dataContext.Sizes.Remove(size);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-size-list");
        } 
        #endregion
    }
}
