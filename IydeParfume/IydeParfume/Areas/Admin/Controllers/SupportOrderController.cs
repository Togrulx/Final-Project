using IydeParfume.Areas.Admin.ViewModels.SupportOrder;
using IydeParfume.Database;
using IydeParfume.Database.Models;
using IydeParfume.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IydeParfume.Areas.Admin.Controllers
{

    [Area("admin")]
    [Route("SupportOrder")]
    [Authorize(Roles = "admin")]

    public class SupportOrderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public SupportOrderController(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        [HttpGet("list", Name = "admin-SupportOrder-list")]
        public async Task<IActionResult> List()
        {
            var model =
              await
              _dataContext.SupportOrders.Select
              (c => new SupportOrderViewModel
              (c.Id, c.Title, c.Content, c.CreatedAt))
              .ToListAsync();

            return View(model);
        }

        #region Add' 

        [HttpGet("add", Name = "admin-SupportOrder-add")]
        public async Task<IActionResult> Add()
        {

            return View();
        }

        [HttpPost("add", Name = "admin-SupportOrder-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            AddProduct();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-SupportOrder-list");


            async void AddProduct()
            {
                var product = new SupportOrder
                {
                    Title = model.Title,
                    Content = model.Content,
                    CreatedAt = DateTime.Now,
                };

                await _dataContext.SupportOrders.AddAsync(product);

            }
        }
        #endregion

        #region Delete'

        [HttpPost("delete/{id}", Name = "admin-SupportOrder-delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _dataContext.SupportOrders.FirstOrDefaultAsync(c => c.Id == id);

            if (model is null) return NotFound();


            _dataContext.SupportOrders.Remove(model);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-contact-list");
        }
        #endregion
    }
}
