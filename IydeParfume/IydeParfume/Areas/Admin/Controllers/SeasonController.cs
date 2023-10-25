using Azure;
using IydeParfume.Areas.Admin.ViewModels.Season;
using IydeParfume.Database;
using IydeParfume.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/season")]
    [Authorize(Roles = "admin")]

    public class SeasonController : Controller
    {
        private readonly DataContext _dataContext;

        public SeasonController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region List'

        [HttpGet("list", Name = "admin-season-list")]
        public async Task<IActionResult> List()
        {
            var model =
                await _dataContext.Seasons.Select(t => new ListItemViewModel(t.Id, t.Title))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Add'

        [HttpGet("add", Name = "admin-season-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }


        [HttpPost("add", Name = "admin-season-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)  return View(model); 

            var season = new Season
            {
                Title = model.Title
            };

            await _dataContext.Seasons.AddAsync(season);

            await _dataContext.SaveChangesAsync();



            return RedirectToRoute("admin-season-list");
        }
        #endregion

        #region Delete'

        [HttpPost("delete/{id}", Name = "admin-season-delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var seasons = await _dataContext.Seasons.FirstOrDefaultAsync(t => t.Id == id);

            if (seasons is null) return NotFound();

            _dataContext.Seasons.Remove(seasons);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-season-list");
        } 
        #endregion
    }
}
