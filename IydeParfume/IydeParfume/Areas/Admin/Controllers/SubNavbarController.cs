using IydeParfume.Areas.Admin.ViewModels.SubNavbar;
using IydeParfume.Database;
using IydeParfume.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AddViewModel = IydeParfume.Areas.Admin.ViewModels.SubNavbar.AddViewModel;

namespace IydeParfume.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/subnavbar")]
    [Authorize(Roles = "admin")]

    public class SubNavbarController : Controller
    {
        private readonly DataContext _dataContext;

        public SubNavbarController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region List'

        [HttpGet("list", Name = "admin-subnavbar-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.SubNavbars
                .Select
                (s => new ListItemViewModel
                (s.Id, s.Name, s.URL, s.Order, s.Navbar.Name, s.CreatedAt, s.UpdatedAt))
                .ToListAsync();
            return View(model);
        }
        #endregion


        #region Add'

        [HttpGet("add", Name = "admin-subnavbar-add")]
        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel
            {
                Navbar = await _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Name)).ToListAsync()
            };
            return View(model);
        }

        [HttpPost("add", Name = "admin-subnavbar-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!await _dataContext.Navbars.AnyAsync(a => a.Id == model.NavbarId))
            {
                ModelState.AddModelError(String.Empty, "Navbar Not Found");
                return View(model);
            }
            if (await _dataContext.SubNavbars.AnyAsync(a => a.Order == model.Order))
            {
                var navModel = new AddViewModel
                {
                    Navbar = await _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Name)).ToListAsync()
                };
                ModelState.AddModelError(String.Empty, "Order cant be the same");
                return View(navModel);
            }

            var subNavbar = new SubNavbar
            {
                Name = model.Name,
                URL = model.URL,
                NavbarId = model.NavbarId,
                Order = model.Order,

            };
            await _dataContext.SubNavbars.AddAsync(subNavbar);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-subnavbar-list");
        }
        #endregion


        #region Update'

        [HttpGet("update/{id}", Name = "admin-subnavbar-update")]
        public async Task<IActionResult> Update([FromRoute] int id)
        {
            var subNavbar = await _dataContext.SubNavbars.Include(s => s.Navbar).FirstOrDefaultAsync(s => s.Id == id);
            if (subNavbar == null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                Id = subNavbar.Id,
                Name = subNavbar.Name,
                URL = subNavbar.URL,
                Order = subNavbar.Order,
                NavbarId = subNavbar.NavbarId,
                Navbar = _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Name)).ToList()
            };
            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-subnavbar-update")]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            var subNavbar = await _dataContext.SubNavbars.Include(s => s.Navbar).FirstOrDefaultAsync(s => s.Id == model.Id);
            if (subNavbar is null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!_dataContext.Navbars.Any(a => a.Id == model.NavbarId))
            {
                ModelState.AddModelError(String.Empty, "Navbar Not Found");
                return View(model);
            }
            if (_dataContext.SubNavbars.Any(a => a.Order == model.Order))
            {
                var navbarModel = new UpdateViewModel
                {
                    Navbar = await _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Name)).ToListAsync()
                };
                ModelState.AddModelError(String.Empty, "Order cant be the same");
                return View(navbarModel);
            }

            subNavbar.Name = model.Name;
            subNavbar.Order = model.Order;
            subNavbar.NavbarId = model.NavbarId;
            subNavbar.URL = model.URL;

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-subnavbar-list");
        }
        #endregion


        #region Delete'

        [HttpPost("delete/{id}", Name = "admin-subnavbar-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var subNavbar = await _dataContext.SubNavbars.Include(s => s.Navbar).FirstOrDefaultAsync(s => s.Id == id);
            if (subNavbar == null)
            {
                return NotFound();
            }

            _dataContext.SubNavbars.Remove(subNavbar);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-subnavbar-list");
        }
        #endregion'
    }
}
