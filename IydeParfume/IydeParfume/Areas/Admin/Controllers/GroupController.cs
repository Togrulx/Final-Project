using IydeParfume.Areas.Admin.ViewModels.Group;
using IydeParfume.Database;
using IydeParfume.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/group")]
    [Authorize(Roles = "admin")]

    public class GroupController : Controller
    {
        private readonly DataContext _dataContext;

        public GroupController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region List'

        [HttpGet("list", Name = "admin-group-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Groups.Select(g => new ListViewModel(g.Id, g.Title)).ToListAsync();
                

            return View(model);
        }
        #endregion

        #region Add'

        [HttpGet("add", Name = "admin-group-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }


        [HttpPost("add", Name = "admin-group-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var group = new Group
            {
                Title = model.Title
            };

            await _dataContext.Groups.AddAsync(group);

            await _dataContext.SaveChangesAsync();



            return RedirectToRoute("admin-group-list");
        }
        #endregion

        #region Delete'

        [HttpPost("delete/{id}", Name = "admin-group-delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var groups = await _dataContext.Groups.FirstOrDefaultAsync(t => t.Id == id);

            if (groups is null) return NotFound();

            _dataContext.Groups.Remove(groups);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-group-list");
        }
        #endregion
    }
}
