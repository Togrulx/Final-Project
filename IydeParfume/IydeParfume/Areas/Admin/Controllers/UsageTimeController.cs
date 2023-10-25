using IydeParfume.Areas.Admin.ViewModels.UsageTime;
using IydeParfume.Database;
using IydeParfume.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/usageTime")]
    [Authorize(Roles = "admin")]

    public class UsageTimeController : Controller
    {
        private readonly DataContext _dataContext;

        public UsageTimeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region List'

        [HttpGet("list", Name = "admin-usageTime-list")]
        public async Task<IActionResult> List()
        {
            var model =
                await _dataContext.UsageTimes.Select(t => new ListItemViewModel(t.Id, t.Title))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Add'

        [HttpGet("add", Name = "admin-usageTime-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }


        [HttpPost("add", Name = "admin-usageTime-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var usageTime = new UsageTime
            {
                Title = model.Title
            };

            await _dataContext.UsageTimes.AddAsync(usageTime);

            await _dataContext.SaveChangesAsync();



            return RedirectToRoute("admin-usageTime-list");
        }
        #endregion

        #region Delete'

        [HttpPost("delete/{id}", Name = "admin-usageTime-delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var usageTimes = await _dataContext.UsageTimes.FirstOrDefaultAsync(t => t.Id == id);

            if (usageTimes is null) return NotFound();

            _dataContext.UsageTimes.Remove(usageTimes);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-usageTime-list");
        }
        #endregion
    }
}
