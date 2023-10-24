﻿using IydeParfume.Areas.Admin.ViewModels.Navbar;
using IydeParfume.Database;
using IydeParfume.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/navbar")]
    [Authorize(Roles = "admin")]

    public class NavbarController : Controller
    {
        private readonly DataContext _dataContext;

        public NavbarController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region List'

        [HttpGet("list", Name = "admin-navbar-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Navbars
                .Select
                (n => new ListItemViewModel(n.Id, n.Name, n.URL, n.Order, n.CreatedAt, n.UpdatedAt))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Add'

        [HttpGet("add", Name = "admin-navbar-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }


        [HttpPost("add", Name = "admin-navbar-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            if (await _dataContext.Navbars.AnyAsync(n => n.Order == model.Order))
            {
                ModelState.AddModelError(String.Empty, "Order cant be the same");
                return View(model);
            }


            var navbar = new Navbar
            {
                Name = model.Name!,
                Order = model.Order,
                URL = model.URL!,
            };


            await _dataContext.Navbars.AddAsync(navbar);
            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("admin-navbar-list");
        }
        #endregion

        #region Update'

        [HttpGet("update/{id}", Name = "admin-nav-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var navbarItem = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == id);

            if (navbarItem is null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                Id = navbarItem.Id,
                Name = navbarItem.Name,
                URL = navbarItem.URL,
                Order = navbarItem.Order
            };

            return View(model);
        }


        [HttpPost("update/{id}", Name = "admin-navbar-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var navbarItem = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (navbarItem is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            navbarItem.Name = model.Name!;
            navbarItem.URL = model.URL!;
            navbarItem.Order = model.Order;

            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("admin-navbar-list");
        }
        #endregion

        #region Delete'

        [HttpPost("delete/{id}", Name = "admin-navbar-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var navbarItem = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == id);

            if (navbarItem is null)
            {
                return NotFound();
            }

            _dataContext.Navbars.Remove(navbarItem);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-navbar-list");
        }
        #endregion
    }
}
